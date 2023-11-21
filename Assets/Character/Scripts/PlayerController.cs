using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        #region ComponentVariable

        private Rigidbody2D rb;
        private UnityEngine.InputSystem.PlayerInput playerInput;

        private Vector2 vec2Position => transform.position;

        #endregion

        #region ObjectVariable

        [Header("Other Object")]
        public Camera mainCamera;

        #endregion

        #region MoveVariable

        [Header("Move")]
        public float moveSpeed;
        public float moveEnergyConsumption;
        private Vector2 mousePos;

        private Vector2 moveDir => (mousePos - vec2Position).normalized;

        #endregion

        #region AttributionVariable

        private float energy;
        public float initEnergy;
        public float energyLossPerFrame;

        public float Energy => energy;

        #endregion

        #region TriggerVariable

        [Header("Trigger Related")]
        public float meetTrickEnergyLoss;
        public float meetTrickBackwardsDistance;
        public GameObject redGhostEffect;

        #endregion

        #region UIVariable

        [Header("UI")]
        public GameObject arrowUI;
        private bool isArrowUIShowing;
        public System.Action energyBarEffectAction;

        #endregion

        #region OtherVariable

        #endregion

        #region layerMask

        #endregion

        #region UnityEventFunction

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        }

        private void Start()
        {
            //arrowUI.transform.rotation = Quaternion.FromToRotation(arrowUI.transform.right, moveDir);
            energy = initEnergy;
        }

        private void Update()
        {
            if (isArrowUIShowing) ArrowUIUpdate();
            DEBUGUpdate();
        }

        private void FixedUpdate()
        {
            //Move(mousePos - vec2Position);
            AttributionUpdate();

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag(Common.ConstString.BRICK_TAG))
            {
                OnMeetBrick();
            }

            if(other.CompareTag(Common.ConstString.MIRROR_TAG))
            {
                OnMeetMirror(-other.transform.up);
            }
        }

        #endregion

        #region InputEventFunction

        public void OnGetMousePosInput(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            if (mainCamera != null)
                mousePos = mainCamera.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
            else
                Debug.LogWarning("Camera is unassigned!");
        }

        public void OnMouseClick(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            switch (ctx.phase)
            {
                case UnityEngine.InputSystem.InputActionPhase.Started:
                    //Debug.Log("mouseClickPressed");
                    OnMousePressed();
                    break;
                case UnityEngine.InputSystem.InputActionPhase.Canceled:
                    //Debug.Log("mouseClickRelesed");
                    OnMouseRelesed();
                    break;
            }
        }

        #endregion

        #region Detection

        #endregion

        #region SpriteFunction

        private void SetArrowUIActivity(bool value)
        {
            if (arrowUI == null)
            {
                Debug.LogWarning("ArrowUI not assigned.");
                return;
            }

            arrowUI.SetActive(value);
            isArrowUIShowing = value;
        }

        private void ArrowUIUpdate()
        {
            if (!isArrowUIShowing)
                return;

            //position
            float distanceScale = 1f;
            arrowUI.transform.position = vec2Position + moveDir * distanceScale;
            //rotation
            arrowUI.transform.rotation *= Quaternion.FromToRotation(arrowUI.transform.right, moveDir);
        }

        #endregion

        #region MoveFunction

        private void OnMousePressed()
        {
            //show arrow UI
            SetArrowUIActivity(true);
            //enter speedbreaker
            Time.timeScale = 0.0f;
        }

        private void OnMouseRelesed()
        {
            //exit speedbreaker
            Time.timeScale = 1f;
            //close arrow UI
            SetArrowUIActivity(false);
            //move
            Move(moveDir);
        }

        private void Move(Vector2 dir)
        {
            //energy Consumed.
            energy -= moveEnergyConsumption;
            energyBarEffectAction();

            rb.velocity = dir.normalized * moveSpeed;
        }

        #endregion

        #region AttributionFunction

        private void AttributionUpdate()
        {
            energy -= energyLossPerFrame;
            if (energy < -0.1f) OnGameComplete();
        }

        #endregion

        #region Trigger

        private void OnMeetBrick()
        {
            //后退
            StartCoroutine(MeetBrickBackwards());
            //物理计算
            rb.velocity = Vector2.zero;
            energy -= meetTrickEnergyLoss;
            //残影
            Instantiate(redGhostEffect, transform.position, Quaternion.identity);
            //UI Effect
            energyBarEffectAction();
        }

        private IEnumerator MeetBrickBackwards()
        {
            Vector2 targetPos = vec2Position - rb.velocity.normalized * meetTrickBackwardsDistance;  //要移动这么多vec2向量
            rb.velocity = Vector2.zero;
            float backwardsSpeed = 0.1f;

            while(Vector2.Distance(targetPos, vec2Position) > 0.1f)
            {
                //移动
                transform.position = Vector2.Lerp(transform.position, targetPos, backwardsSpeed);
                yield return null;
            }
        }

        //receive the normal direction of the mirror
        private void OnMeetMirror(Vector2 normalDir)
        {
            Vector2 newDir = rb.velocity.normalized;
            newDir *= -1;
            Quaternion rot = Quaternion.FromToRotation(newDir, normalDir);
            newDir = rot * rot * newDir;
            rb.velocity = rb.velocity.magnitude * newDir;
        }

        #endregion

        #region GameManager

        public void OnGameComplete()
        {
            playerInput.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
        }

        #endregion

        #region Debug

        [Header("Debug")]
        public float energyDEBUG;

        private void DEBUGUpdate()
        {
            //var keyboard = UnityEngine.InputSystem.Keyboard.current;
            //if (keyboard.vKey.wasPressedThisFrame)
            //{

            //}
            energyDEBUG = energy;

        }

        #endregion

    }
}
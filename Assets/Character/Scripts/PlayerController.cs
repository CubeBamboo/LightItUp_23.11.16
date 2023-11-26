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

        public BoxCollider2D movingCollider;
        private Vector2 currMoveDir;    //maintain it with the movedir.
        private Vector2 DirToMove => (mousePos - vec2Position).normalized;

        #endregion

        #region AttributionVariable

        private float energy;
        public float initEnergy => Core.GameManager.Instance.levelData.playerInitEnergy;
        public float energyLossPerFrame;

        public float Energy => energy;

        #endregion

        #region EffectInterface

        public System.Action ResetTrail;

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
            //if(!movingBounds.Contains(vec2Position)) MoveBoundClamp();
            AttributionUpdate();

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag(Common.Constant.BRICK_TAG))
            {
                OnMeetBrick();
            }

            
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.CompareTag(Common.Constant.MOVING_BOUNDS_TAG))
            {
                MoveBoundSwitch();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Common.Constant.MIRROR_TAG))
            {
                OnMeetMirror(other.GetContact(0).normal);
            }
        }

        private void OnDrawGizmosSelected()
        {
            //draw wirecube
            //Gizmos.color = Color.yellow;
            //Gizmos.DrawWireCube((Vector2)movingCollider.transform.position + movingCollider.offset, movingCollider.size);
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
                    OnMousePressed();
                    break;
                case UnityEngine.InputSystem.InputActionPhase.Canceled:
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
            arrowUI.transform.position = vec2Position + DirToMove * distanceScale;
            //rotation
            arrowUI.transform.rotation *= Quaternion.FromToRotation(arrowUI.transform.right, DirToMove);
        }

        #endregion

        #region MoveFunction

        private void SetVelocity(Vector2 value)
        {
            rb.velocity = value;
            currMoveDir = value.normalized;
        }

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
            Move(DirToMove);
        }

        private void Move(Vector2 dir)
        {
            //energy Consumed.
            energy -= moveEnergyConsumption;
            energyBarEffectAction();

            SetVelocity(dir.normalized * moveSpeed);
        }

        private void MoveBoundSwitch()
        {
            Debug.Log("MoveBoundClamp()");
            Vector2 newPos = vec2Position;
            Vector2 movingColliderPos = (Vector2)movingCollider.transform.position + movingCollider.offset;
            Vector2 movingColliderMax = movingColliderPos + movingCollider.size / 2;
            Vector2 movingColliderMin = movingColliderPos - movingCollider.size / 2;

            if(vec2Position.x > movingColliderMax.x) {
                newPos.x = movingColliderMin.x;
            } else if(vec2Position.x < movingColliderMin.x) {
                newPos.x = movingColliderMax.x;
            }
            if(vec2Position.y > movingColliderMax.y) {
                newPos.y = movingColliderMin.y;
            } else if(vec2Position.y < movingColliderMin.y) {
                newPos.y = movingColliderMax.y;
            }

            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
            ResetTrail?.Invoke();
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
            energy = -0.1f;
            //ghost effect
            Instantiate(redGhostEffect, transform.position, Quaternion.identity);
        }

        //receive the normal direction of the mirror
        private void OnMeetMirror(Vector2 normalDir)
        {
            //Debug.Log("MeetMirror, normalDir=" + normalDir + ", ReflectDir=" + Vector3.Reflect(currMoveDir * moveSpeed, normalDir));
            SetVelocity(Vector3.Reflect(currMoveDir * moveSpeed, normalDir));
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
        public Vector2 currMoveDirDEBUG;

        private void DEBUGUpdate()
        {
            //var keyboard = UnityEngine.InputSystem.Keyboard.current;
            //if (keyboard.vKey.wasPressedThisFrame)
            //{
            //    ResetTrail?.Invoke();
            //}
            energyDEBUG = energy;
            currMoveDirDEBUG = currMoveDir;

        }

        #endregion

    }
}
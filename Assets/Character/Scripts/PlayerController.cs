using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        #region ComponentVariable

        private Rigidbody2D rb;

        private Vector2 vec2Position => transform.position;

        #endregion

        #region ObjectVariable

        [Header("Other Object")]
        public Camera mainCamera;

        #endregion

        #region MoveVariable

        [Header("Move")]
        public float speed;
        //public bool canMove = true;
        private Vector2 mousePos;
        //private bool readyMove;

        private Vector2 moveDir => (mousePos - vec2Position).normalized;

        #endregion

        #region AttributionVariable

        private float energy;
        public float initEnergy;
        public float energyLossPerFrame;

        public float Energy => energy;

        #endregion

        #region UIVariable

        [Header("UI")]
        public GameObject arrowUI;
        private bool isArrowUIShowing;

        #endregion

        #region OtherVariable

        #endregion

        #region layerMask

        #endregion

        #region UnityEventFunction

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
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
                    Debug.Log("mouseClickPressed");
                    OnMousePressed();
                    break;
                case UnityEngine.InputSystem.InputActionPhase.Canceled:
                    Debug.Log("mouseClickRelesed");
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
            //dont move if it's close enought to the cursor.
            if (Vector2.Distance(mousePos, vec2Position) < 0.1f)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            rb.velocity = dir.normalized * speed;
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

        #endregion

        #region GameManager

        public void OnGameComplete()
        {
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
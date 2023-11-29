using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        #region ComponentVariable

        private Rigidbody2D rb;
        private UnityEngine.InputSystem.PlayerInput playerInput;
        private SpriteRenderer spriteRenderer;

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
        private Vector2 lastSetMoveDir; //To resolve the speedDir compute, get the moveDir after the moveDirection changed.
        private Vector2 dirToMove;

        #endregion

        #region AttributionVariable

        private float energy;
        public float initEnergy => Core.GameManager.Instance ? Core.GameManager.Instance.levelData.playerInitEnergy : 50f; //custom or defalult Value
        public float energyLossPerFrame;

        public float Energy => energy;

        #endregion

        #region EffectInterface

        public System.Action ResetTrailAction;
        public System.Action soundEffectAction;

        #endregion

        #region TriggerVariable

        [Header("Trigger Related")]
        //public float meetTrickEnergyLoss;
        //public float meetTrickBackwardsDistance;
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
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Start()
        {
            //arrowUI.transform.rotation = Quaternion.FromToRotation(arrowUI.transform.right, moveDir);
            energy = initEnergy;
        }

        private void Update()
        {
            if (isArrowUIShowing) ArrowUIUpdate();
#if UNITY_EDITOR
            DEBUGUpdate();
#endif
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

        #endregion

        #region InputEventFunction

        public void OnMoveDirInput(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            switch(playerInput.currentControlScheme)
            {
                case Common.Constant.MOUSE_KEYBOARD_SCHEMES:
                    if (mainCamera != null) mousePos = mainCamera.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
                    else Debug.Log("Camera is unassigned!");
                    
                    dirToMove = mousePos - vec2Position;
                    dirToMove = dirToMove.normalized;
                    break;

                case Common.Constant.GAMEPAD_SCHEMES:
                    dirToMove = ctx.ReadValue<Vector2>();
                    break;
            }
            
        }

        public void OnMoveButton(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            switch (ctx.phase)
            {
                case UnityEngine.InputSystem.InputActionPhase.Started:
                    OnMoveButtonPressed();
                    break;
                case UnityEngine.InputSystem.InputActionPhase.Canceled:
                    OnMoveButtonRelesed();
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
            float distanceScale = 0.6f;
            arrowUI.transform.position = vec2Position + dirToMove.normalized * distanceScale;
            //rotation
            arrowUI.transform.rotation *= Quaternion.FromToRotation(arrowUI.transform.right, dirToMove);
        }

        #endregion

        #region MoveFunction

        private void SetVelocity(Vector2 value)
        {
            rb.velocity = value;
            lastSetMoveDir = value.normalized;
        }

        private void OnMoveButtonPressed()
        {
            //show arrow UI
            SetArrowUIActivity(true);
            //enter speedbreaker
            rb.velocity = Vector2.zero;
        }

        private void OnMoveButtonRelesed()
        {
            //close arrow UI
            SetArrowUIActivity(false);
            
            //move
            Move(dirToMove);
        }

        private void Move(Vector2 dir)
        {
            if (dir == Vector2.zero)
            {
                rb.velocity = lastSetMoveDir * moveSpeed;
                return;
            }

            //energy Consumed.
            energy -= moveEnergyConsumption;
            energyBarEffectAction?.Invoke();

            soundEffectAction?.Invoke();
            SetVelocity(dir.normalized * moveSpeed);
        }

        private void MoveBoundSwitch()
        {
            //Debug.Log("MoveBoundClamp()");
            Vector2 newPos = vec2Position;
            Vector2 movingColliderPos = (Vector2)movingCollider.transform.position + movingCollider.offset;
            Vector2 movingColliderMax = movingColliderPos + movingCollider.size / 2;
            Vector2 movingColliderMin = movingColliderPos - movingCollider.size / 2;

            //TODO: elegant?
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
            ResetTrailAction?.Invoke();
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
            OnPlayerDie();
        }

        //receive the normal direction of the mirror
        private void OnMeetMirror(Vector2 normalDir)
        {
            //Debug.Log($"MeetMirror, currentVec = {lastSetMoveDir*moveSpeed},  normalDir={normalDir}, ReflectDir={Vector3.Reflect(lastSetMoveDir * moveSpeed, normalDir)}");
            SetVelocity(Vector3.Reflect(lastSetMoveDir * moveSpeed, normalDir));
        }

        #endregion

        #region GameManager

        public void OnGameComplete()
        {
            playerInput.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            spriteRenderer.enabled = false;
        }

        public void OnPlayerDie()
        {
            //ghost effect
            Instantiate(redGhostEffect, transform.position, Quaternion.identity);
            //camera shake
            CustomCamera.CameraCollisionImpulseSource.Impulse(); //TODO: waht... i dont know what i am coding.
        }

        #endregion

        #region Debug

        [Header("Debug")]
        public float energyDEBUG;
        //public Vector2 currMoveDirDEBUG;
        //public Vector2 mousePosDEBUG;
        //public string currentControlSchemeDebug;

        private void DEBUGUpdate()
        {
            //var keyboard = UnityEngine.InputSystem.Keyboard.current;
            ////if (keyboard.vKey.wasPressedThisFrame || keyboard.bKey.wasPressedThisFrame || keyboard.cKey.wasPressedThisFrame)
            //if (keyboard.vKey.wasPressedThisFrame)
            //{

            //}
            //mousePos = mousePosDEBUG;
            //currentControlSchemeDebug = playerInput.currentControlScheme;
            //currMoveDirDEBUG = CurrMoveDir;
            energyDEBUG = energy;

        }

        #endregion

    }
}
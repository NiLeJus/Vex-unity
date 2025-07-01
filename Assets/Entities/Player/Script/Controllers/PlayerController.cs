using Lean.Gui;
using UnityEngine;

namespace KinematicCharacterController.Examples
{
    public class PlayerController : MonoBehaviour
    {

        //#region Singleton //
        //public void SingletonImplementation()
        //{
        //    if (I == null)
        //    {
        //        I = this;
        //        DontDestroyOnLoad(gameObject);
        //    }
        //    else
        //    {
        //        Destroy(gameObject);
        //    }
        //}
        //#endregion

        private void Awake()
        {

        }

        [Header("Mobile Controls")]
        [SerializeField] private LeanJoystick _moveJoystick;
        [SerializeField] private LeanJoystick _cameraJoystick;
        [SerializeField] private LeanButton _sprintButton;
        [SerializeField] private LeanButton _interactButton;
        private bool _sprintTriggered = false;


        [SerializeField] private LeanButton _jumpButton;
        private bool _jumpTriggered = false;


        [Header("Kynematic Controller")]
        public CharacterController Character;
        public CharacterCamera CharacterCamera;
        public Animator characterAnimator;

        private bool CursorLockState = true;

        private const string MouseXInput = "Mouse X";
        private const string MouseYInput = "Mouse Y";
        private const string MouseScrollInput = "Mouse ScrollWheel";
        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";
        public bool Sprint;
        public bool Jump;
        public bool Interact;
        private void Start()
        {
            transform.position = GameManager.I.playerSpawnPosition;

            // Configure les boutons mobiles
            if (_jumpButton != null)
            {
                _jumpButton.OnDown.AddListener(() => Jump = true);

            }

            if (_sprintButton != null)
            {
                _sprintButton.OnDown.AddListener(() => Sprint = true);
            }

            if (_interactButton != null)
            {
                _interactButton.OnDown.AddListener(() => Interact = true);
            }

            Cursor.lockState = CursorLockMode.None;

            // Tell camera to follow transform
            CharacterCamera.SetFollowTransform(Character.CameraFollowPoint);

            // Ignore the _baseNPC's collider(s) for camera obstruction checks
            CharacterCamera.IgnoredColliders.Clear();
            CharacterCamera.IgnoredColliders.AddRange(Character.GetComponentsInChildren<Collider>());
        }

        private void Update()
        {

            //if (Input.GetMouseButton(2))
            //{
            //    CursorLockState = !CursorLockState;
            //}

            //if (CursorLockState == true)
            //{
            //    Cursor.lockState = CursorLockMode.Locked;
            //}
            //else
            //{
            //    Cursor.lockState = CursorLockMode.None;
            //}

            if (Input.GetKeyDown(KeyCode.E) || Interact)
            {
                DetectInteractions();
            }
            if (Input.GetKeyDown(KeyCode.Space) || Jump)
            {
                JumpTrigger();
            }

            HandleCharacterInput();

            float speed = Character.Motor.Velocity.magnitude;
            if (characterAnimator != null)
                characterAnimator.SetFloat("Speed", speed);

        }

        public void JumpTrigger()
        {
            characterAnimator.SetTrigger("JumpTrigger");
        }

        private void LateUpdate()
        {
            // Handle rotating the camera along with physics movers
            if (CharacterCamera.RotateWithPhysicsMover && Character.Motor.AttachedRigidbody != null)
            {
                CharacterCamera.PlanarDirection = Character.Motor.AttachedRigidbody.GetComponent<PhysicsMover>().RotationDeltaFromInterpolation * CharacterCamera.PlanarDirection;
                CharacterCamera.PlanarDirection = Vector3.ProjectOnPlane(CharacterCamera.PlanarDirection, Character.Motor.CharacterUp).normalized;
            }

            HandleCameraInput();

            Jump = false;
        }



        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            // Combine Keyboard + Joystick
            Vector2 joystickInput = _moveJoystick != null ? _moveJoystick.ScaledValue : Vector2.zero;
            float keyboardVertical = Input.GetAxisRaw(VerticalInput);
            float keyboardHorizontal = Input.GetAxisRaw(HorizontalInput);

            // Merge Inputs (clamp to stay between -1 et 1)
            characterInputs.MoveAxisForward = Mathf.Clamp(joystickInput.y + keyboardVertical, -1f, 1f);
            characterInputs.MoveAxisRight = Mathf.Clamp(joystickInput.x + keyboardHorizontal, -1f, 1f);

            // Mouse + Joystick
            characterInputs.CameraRotation = CharacterCamera.Transform.rotation;
            characterInputs.JumpDown = Jump || Input.GetKeyDown(KeyCode.Space);
            characterInputs.CrouchDown = Input.GetKeyDown(KeyCode.C);
            characterInputs.CrouchUp = Input.GetKeyUp(KeyCode.C);
            characterInputs.Sprint = Sprint || Input.GetKey(KeyCode.LeftShift);

            Character.SetInputs(ref characterInputs);
        }

        private void HandleCameraInput()
        {
            Vector2 cameraJoystickInput = _moveJoystick != null ? _moveJoystick.ScaledValue : Vector2.zero;

            // Combine les inputs souris ET joystick caméra
            //float mouseLookAxisUp = Input.GetAxisRaw(MouseYInput) + cameraJoystickInput.y;
            //float mouseLookAxisRight = Input.GetAxisRaw(MouseXInput) + cameraJoystickInput.x;

            float mouseLookAxisUp = cameraJoystickInput.y;
            float mouseLookAxisRight = cameraJoystickInput.x;

            // Merge sources // Ajust sensibility
            Vector3 lookInputVector = new Vector3(
                mouseLookAxisRight * 20f,
                mouseLookAxisUp * 2f,
                0f
            );

            //Prevent moving the camera while the cursor isn't locked
            if (CursorLockState == true)
            {
                lookInputVector = Vector3.zero;
            }

            // Input for zooming the camera (disabled in WebGL because it can cause problems)
            float scrollInput = -Input.GetAxis(MouseScrollInput);
#if UNITY_WEBGL
        scrollInput = 0f;
#endif

            // Apply inputs to the camera
            CharacterCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

            // Handle toggling zoom level
            if (Input.GetMouseButtonDown(1))
            {
                CharacterCamera.TargetDistance = (CharacterCamera.TargetDistance == 0f) ? CharacterCamera.DefaultDistance : 0f;
            }
        }

        public void DetectInteractions()
        {
            Interact = false;
            Debug.Log("Detecting Interaction");
            float interactRange = 20f;
            Collider[] collidersArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in collidersArray)
            {
                if (collider.TryGetComponent(out NPCController npcController))
                {
                    npcController.OnInteract();
                    break;
                }
                ;
            }
        }
    }
}
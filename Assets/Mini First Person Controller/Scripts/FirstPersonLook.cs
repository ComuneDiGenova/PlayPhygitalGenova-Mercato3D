using UnityEngine;

namespace Mini_First_Person_Controller.Scripts
{
    public class FirstPersonLook : MonoBehaviour
    {
        [SerializeField]
        Transform character;
        public float sensitivity = 2;
        public float smoothing = 1.5f;
        [SerializeField] private int minAngle = 45;
        [SerializeField] private int maxAngle = 40;

        private Vector2 _velocity;
        private Vector2 _frameVelocity;

        private static FirstPersonLook Instance { get; set; }
        private static bool _isTeleport;

        private void Awake()
        {
            Instance = this;
        }

        void Reset()
        {
            // Get the character from the FirstPersonMovement in parents.
            character = GetComponentInParent<FirstPersonMovement>().transform;
        }

        void Start()
        {
            // Lock the mouse cursor to the game screen.
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            // Get smooth velocity.
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        

            if (!_isTeleport)
            {
                _frameVelocity = Vector2.Lerp(_frameVelocity, rawFrameVelocity, 1 / smoothing);
                _velocity += _frameVelocity;
                _velocity.y = Mathf.Clamp(_velocity.y, -minAngle, maxAngle);
            }

            // Rotate camera up-down and controller left-right from velocity.
            transform.localRotation = Quaternion.AngleAxis(-_velocity.y, Vector3.right);

            if (!_isTeleport)
            {
                character.localRotation = Quaternion.Euler(0, _velocity.x, 0);
            }
            else
            {
                _isTeleport = false;
            }
        }

        /// <summary>
        ///   <para>Sets the player's rotation around the y-axis.</para>
        /// </summary>
        public static void SetVelocity(float yRotation)
        {
            Instance._velocity.x = yRotation;
        }
    
        /// <summary>
        ///   <para>Toggle the teleport state to allow change to player position and rotation.</para>
        /// </summary>
        public static void SetTeleport()
        {
            _isTeleport = true;
        }
    }
}

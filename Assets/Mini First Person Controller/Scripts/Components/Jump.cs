using UnityEngine;

namespace Mini_First_Person_Controller.Scripts.Components
{
    public class Jump : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public KeyCode jumpKey = KeyCode.Space;
        public float jumpStrength = 2;
        public event System.Action Jumped;

        [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
        private GroundCheck groundCheck;


        private void Reset()
        {
            // Try to get groundCheck.
            groundCheck = GetComponentInChildren<GroundCheck>();
        }

        private void Awake()
        {
            // Get rigidbody.
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void LateUpdate()
        {
            // Jump when the Jump button is pressed and we are on the ground.
            if (!Input.GetKeyDown(jumpKey) || (groundCheck && !groundCheck.isGrounded)) return;
            _rigidbody.AddForce(Vector3.up * (100 * jumpStrength));
            Jumped?.Invoke();
        }
    }
}

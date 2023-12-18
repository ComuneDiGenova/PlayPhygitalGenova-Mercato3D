using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Mini_First_Person_Controller.Scripts
{
    public class FirstPersonMovement : MonoBehaviour
    {
        public float speed = 5;

        [Header("Running")]
        public bool canRun = true;
        public bool IsRunning { get; private set; }
        public float runSpeed = 9;
        public KeyCode runningKey = KeyCode.LeftShift;
        
        private Rigidbody _rigidbody;
        /// <summary> Functions to override movement speed. Will use the last added override. </summary>
        public readonly List<System.Func<float>> SpeedOverrides = new List<System.Func<float>>();

        private void Awake()
        {
            // Get the rigidbody on this.
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            
            
            // Update IsRunning from input.
            IsRunning = canRun && Input.GetKey(runningKey);

            // Get targetMovingSpeed.
            float targetMovingSpeed = IsRunning ? runSpeed : speed;
            if (SpeedOverrides.Count > 0)
            {
                targetMovingSpeed = SpeedOverrides[^1]();
            }

            // Get targetVelocity from input.
            Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

            // Apply movement.
            _rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.y);
            
            // move with mouseScrollWheel
            /*if (Mouse.current.scroll.up.ReadValue() > 0)
            {
                var mouseSpeed = Mouse.current.scroll.up.ReadValue() / 30 * targetMovingSpeed;
                _rigidbody.velocity = transform.rotation * new Vector3(0, _rigidbody.velocity.y, mouseSpeed+targetVelocity.y);
            } else if (Mouse.current.scroll.down.ReadValue() > 0)
            {
                var mouseSpeed = Mouse.current.scroll.down.ReadValue() / 30 * targetMovingSpeed;
                _rigidbody.velocity = transform.rotation * new Vector3(0, _rigidbody.velocity.y, -mouseSpeed+targetVelocity.y);
            }*/
        }
    }
}
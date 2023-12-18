using System.Collections;
using Mini_First_Person_Controller.Scripts;
using UnityEngine;
using static phygital.Scripts.Env;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace phygital.Scripts
{
    public class PlayerHandler : MonoBehaviour
    {
        private static PlayerHandler Instance { get; set; }
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Transform playerPosition;
        [SerializeField] private GameObject playerMovementInput;
        [SerializeField] private GameObject crosshair;
        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        ///   <para>Lock the player's input.</para>
        /// </summary>
        public static IEnumerator LockInput()
        {
            LockPlayer();
            UnlockCursor();
            yield break;
        }

        /// <summary>
        ///   <para>Lock the player's input.</para>
        /// </summary>
        public static IEnumerator UnlockInput()
        {
            UnlockPlayer();
            LockCursor();
            yield break;
        }
        
        /// <summary>
        ///   <para>Lock the player's movement.</para>
        /// </summary>
        private static void LockPlayer()
        {
            RetrieveViewInput().sensitivity = 0;
            RetrieveCrosshair().SetActive(false);
            RetrieveMovementInput().enabled = false;
        }
        
        /// <summary>
        ///   <para>Unlock the player's movement.</para>
        /// </summary>
        private static void UnlockPlayer()
        {
            RetrieveViewInput().sensitivity = CAMERA_SENSIBILITY;
            RetrieveCrosshair().SetActive(true);
            RetrieveMovementInput().enabled = true;
        }
        
        /// <summary>
        ///   <para>Unlock the player's camera.</para>
        /// </summary>
        private static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        /// <summary>
        ///   <para>Lock the player's camera.</para>
        /// </summary>
        private static void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        /// <summary>
        ///   <para>Retrieves the camera input associated with the player.</para>
        /// </summary>
        private static FirstPersonLook RetrieveViewInput()
        {
            Instance.playerCamera.TryGetComponent(out FirstPersonLook playerView);
            return playerView;
        }

        /// <summary>
        ///   <para>Retrieves the movement input associated with the player.</para>
        /// </summary>
        private static FirstPersonMovement RetrieveMovementInput()
        {
            Instance.playerMovementInput.TryGetComponent(out FirstPersonMovement playerMovement);
            return playerMovement;
        }

        /// <summary>
        ///   <para>Retrieve the crosshair used by the player to aim.</para>
        /// </summary>
        private static GameObject RetrieveCrosshair()
        {
            return Instance.crosshair;
        }

        /// <summary>
        ///   <para>Move player in front of the <paramref name="target"/> provided.</para>
        /// </summary>
        /// <param name="target"></param>
        /// <remarks>
        /// The transform associated with the target must be in its centre
        /// </remarks>
        public static void MovePlayerTo(GameObject target)
        {
            // Common local variables
            var center = new Vector3(0, 0, 0);
            var targetTransform = target.transform;
            var targetTransformPosition = targetTransform.position;
            var centerDir = center - targetTransformPosition;
            
            // Position

            var offset = 2.8f;
            const float groundHigh = -9.0361f;
            var targetFromCenter = Vector3.Distance(targetTransformPosition, center); 
            var flipIfNearCenter = targetFromCenter < 15;
            var flipIfInnerProduct = targetFromCenter is > 18 and < 22 ;

            if (flipIfNearCenter)
            {
                offset = -2.0f;
            }
            
            if (flipIfInnerProduct)
            {
                offset = -2.0f;
            }
            var inFrontPosition = targetTransformPosition + (centerDir / centerDir.magnitude) * offset ;
            Instance.playerPosition.position = new Vector3(inFrontPosition.x, groundHigh, inFrontPosition.z);
            
            // Rotation
            
            var qir = new Quaternion();
            qir.SetLookRotation(centerDir, Vector3.up);
            FirstPersonLook.SetTeleport();
            var turn = (flipIfNearCenter || flipIfInnerProduct) switch
            {
                false => -180f,
                true => 0f
            };
            FirstPersonLook.SetVelocity(qir.eulerAngles.y + turn);
        }
    }
}

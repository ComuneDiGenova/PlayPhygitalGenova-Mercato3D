using UnityEngine;

namespace phygital.Scripts
{
    public class FolksFollowCamera2 : MonoBehaviour
    {
        [SerializeField] private Transform playerCameraTransform;
        [SerializeField] private GameObject[] people;
    

        private void Start()
        {
            LookAtPlayer();
        }

        private void Update()
        {
            LookAtPlayer();
        }

        private void LookAtPlayer()
        {
            var position = playerCameraTransform.position;
            const float posY = -8.44f;
            foreach (var t in people)
            {
                
                t.transform.LookAt(new Vector3(position.x, posY, position.z));
            }
        }
    }
}
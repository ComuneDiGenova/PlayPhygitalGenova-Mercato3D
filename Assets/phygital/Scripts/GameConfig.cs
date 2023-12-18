using UnityEngine;

namespace phygital.Scripts
{
    public class GameConfig : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = -1;
        }
    }
}

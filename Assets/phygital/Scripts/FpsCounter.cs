using System.Collections;
using TMPro;
using UnityEngine;

namespace phygital.Scripts
{
    public class FpsCounter : MonoBehaviour
    {
        [SerializeField] private GameObject frameRateUI;
        [SerializeField] private TextMeshProUGUI frameRateText;
        [SerializeField] private int frameRate = -1;
        private int _avgFrameRate;
    
        public void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = frameRate;
            StartCoroutine(ComputeFPS());
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                frameRateUI.SetActive(!frameRateUI.activeSelf);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                Screen.fullScreen = Screen.fullScreen switch
                {
                    false => true,
                    true => false
                };
            }
        }

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator ComputeFPS() {
            yield return new WaitForSeconds(1);
            _avgFrameRate = (int)(1f / Time.unscaledDeltaTime);
            frameRateText.text = _avgFrameRate.ToString();
            StartCoroutine(ComputeFPS());
        } 
    }
}

using UnityEngine;
using static phygital.Scripts.PlayerHandler;
using static phygital.Scripts.Env;

namespace phygital.Scripts
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject helpPage;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                PauseResume(pauseMenu.activeSelf);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                HelpPage(helpPage.activeSelf);
            }
        }

        public void PauseResume(bool paused)
        {
            if (UIOPEN)
            {
                if (!pauseMenu.activeSelf) return;
                pauseMenu.SetActive(!paused);
                StartCoroutine(paused ? UnlockInput() : LockInput());
                UIOPEN = false;
                return;
            }
            pauseMenu.SetActive(!paused);
            UIOPEN = true;
            StartCoroutine(paused ? UnlockInput() : LockInput());
        }
        public void HelpPage(bool isHelpOpen)
        {
            if (UIOPEN)
            {
                if (!helpPage.activeSelf) return;
                helpPage.SetActive(!isHelpOpen);
                StartCoroutine(isHelpOpen ? UnlockInput() : LockInput());
                UIOPEN = false;
                return;
            }
            helpPage.SetActive(!isHelpOpen);
            UIOPEN = true;
            StartCoroutine(isHelpOpen ? UnlockInput() : LockInput());
        }
    }
}

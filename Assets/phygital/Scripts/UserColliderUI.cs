using static phygital.Scripts.UIHandler;
using static phygital.Scripts.PlayerHandler;
using static phygital.Scripts.Env;
using UnityEngine;

namespace phygital.Scripts
{
    public class UserColliderUI : MonoBehaviour, IInteractable

    {
        public void UIInteract(GameObject target)
        {
            if (!Input.GetKeyDown(KeyCode.E) && !Input.GetKeyDown(KeyCode.Mouse0)) return;
            StartCoroutine(InvokeUI(target.tag, target.layer));
            if (UIOPEN){
                StartCoroutine(LockInput());
            }
            StartCoroutine(InputToCloseUI(target, target.layer));
        }
    }
}

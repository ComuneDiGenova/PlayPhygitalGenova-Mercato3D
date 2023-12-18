using phygital.Json;
using UnityEngine;
using static phygital.Scripts.Env;
namespace phygital.Scripts
{
    public class UserRaycaster : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float rayMaxDistance = 6;
        [SerializeField] private bool useSphereCast;
        [SerializeField] private float rayThickness = 1f;
        [SerializeField] private GameObject uiManager;

        private GameObject _lastHit;
        private void Update()
        {
            var forwardRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            const int layerMask = (1 << (int)LayerIndexes.Pareti) | (1 << (int)LayerIndexes.UI) | (1 << (int)LayerIndexes.Interagibili) | (1 << (int)LayerIndexes.Botteghe) | (1 << (int)LayerIndexes.Categorie);
            bool hit;
            RaycastHit hitInfo;
            if (useSphereCast)
            {
                hit = Physics.SphereCast(forwardRay, rayThickness, out var hitInfoTemp, rayMaxDistance, layerMask);
                hitInfo = hitInfoTemp;
            }
            else
            {
                hit = Physics.Raycast(forwardRay, out var hitInfoTemp, rayMaxDistance, layerMask);
                hitInfo = hitInfoTemp;
            }
            if (!hit)
            {
                if (!_lastHit) return;
                return;
            }
            var hitObject = hitInfo.transform.gameObject;
            _lastHit = hitObject;
            uiManager.TryGetComponent(out IInteractable action);

            if (UIOPEN) return;
            switch (hitObject.layer)
            {
                case (int)LayerIndexes.Pareti: return;
                case (int)LayerIndexes.UI: return;
                case (int)LayerIndexes.Interagibili:
                    if (RetrieveData.GetProdotto(hitObject.tag) is null)
                    {
                        if (RetrieveData.GetProdotto(hitObject.tag).nome is null)
                        {
                            if (RetrieveData.GetRicetta(hitObject.tag) is null)
                            {
                                if (RetrieveData.GetRicetta(hitObject.tag).nome is null)
                                {
                                    return;
                                }
                            }
                        } 
                    }
                    action.UIInteract(_lastHit);
                    break;
                case (int)LayerIndexes.Botteghe:
                case (int)LayerIndexes.Categorie:
                    action.UIInteract(_lastHit);
                    break;
            }
        }
    }
}
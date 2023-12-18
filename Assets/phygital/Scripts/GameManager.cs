using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace phygital.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Button load;
        [SerializeField] private TextMeshProUGUI btnLabel;
        [SerializeField] private TextMeshProUGUI eta;
        [SerializeField] private Slider loadingSlider;
    
        private void Start()
        {
            StartCoroutine(Handler().DownloadFiles());
            if (!IsLoadable()) {load.interactable = false;return;}
            load.interactable = true;
            load.onClick.AddListener(LoadGame);

        }

        private void Update()
        {
            if (!IsLoadable() || load.interactable) return;
            eta.text = LoadingLocale.GetLoadingLocale().SetETABarReady();;
            eta.text = LoadingLocale.GetLoadingLocale().SetETABarReady();;
            load.interactable = true;
            btnLabel.text = LoadingLocale.GetLoadingLocale().SetButtonEnter();
            load.onClick.AddListener(LoadGame);
        }

        private void LoadGame()
        {
            StartCoroutine(Handler().LoadAssetBundleScene());
        }

        private bool IsLoadable()
        {
            return !(Math.Abs(loadingSlider.maxValue - loadingSlider.value) > 0);
        }

        private AssetBundleHandler Handler()
        {
            gameObject.TryGetComponent(out AssetBundleHandler assetDownload);
            return assetDownload;
        }
    }
}
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace phygital.Scripts
{
    public class AssetBundleHandler : MonoBehaviour
    {
        private readonly string _sceneToDownload = RetrievePaginaHelp._assetBundleBaseUrl + "/Resources/AssetBundles/phygital.1";
        [SerializeField] private TextMeshProUGUI downloadProgress;
        [SerializeField] private TextMeshProUGUI eta;
        [SerializeField] private Slider downloadBar;
        
        private AssetBundle _assetBundle;
        private string _sceneNameToLoadAb;
        private bool _isRunning;
        private UnityWebRequest _assetRequest;

        private float _downloaded;
        private float _timeElapsed;
        private int _timePassed;

        private void Update()
        {
            if (_isRunning) CheckDownload();
        }

        public IEnumerator DownloadFiles()
        {
            _isRunning = true;
            var bundleHash = new Hash128();
                bundleHash.Append(Env.HASH);
            //using (_assetRequest = UnityWebRequestAssetBundle.GetAssetBundle(sceneToDownload, new CachedAssetBundle("cachedBundle", bundleHash)) )
            using (_assetRequest = UnityWebRequestAssetBundle.GetAssetBundle(_sceneToDownload) )
            {
                yield return _assetRequest.SendWebRequest();
                if (_assetRequest.result != UnityWebRequest.Result.Success)
                {
                    print("Errore nel recupero dell'assetbundle: "+_assetRequest.result);
                }
                else
                {
                    _isRunning = false;
                    _assetBundle = DownloadHandlerAssetBundle.GetContent(_assetRequest);
                    CheckDownload();
                }
            }

            var scenes = _assetBundle.GetAllScenePaths();
            foreach (var sceneName in scenes)
            {
                _sceneNameToLoadAb = Path.GetFileNameWithoutExtension(sceneName);
            }
        }
        
        public IEnumerator LoadAssetBundleScene()
        {
            yield return null;
            
            var asyncOperation = SceneManager.LoadSceneAsync(_sceneNameToLoadAb, LoadSceneMode.Single);
            asyncOperation.allowSceneActivation = false;
            
            //Load scene in progress
            while (!asyncOperation.isDone)
            {
                //Output current progress
                downloadBar.value = asyncOperation.progress * 100;
                downloadProgress.text = ((int)downloadBar.value).ToString(CultureInfo.CurrentCulture) + "%";
                
                //Check if finished
                if (asyncOperation.progress >= 0.9f)
                {
                    //Ready
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }

        }

        private void CheckDownload()
        {
            var percentage = _assetRequest.downloadProgress * 100;
            eta.text = Eta(eta.text, percentage);
            downloadBar.value = percentage;
            downloadProgress.text = ((int) downloadBar.value).ToString(CultureInfo.CurrentCulture) + "%";
        }

        private string Eta(string currentTime, float percentageProgress)
        {
            _downloaded = (_assetRequest.downloadedBytes - _downloaded);
            var previousTimeElapsed = _timePassed;
            _timeElapsed += Time.deltaTime;
            _timePassed = (int)_timeElapsed;
            var speed = (percentageProgress) / _timeElapsed;
            var remainingTimeRaw = (100 - percentageProgress) / speed;
            int[] timeFormatted = {(int)(remainingTimeRaw / 60), (int)(remainingTimeRaw % 60)};
            if (_timePassed <= previousTimeElapsed) return currentTime;
            if (timeFormatted[0] != 0) return timeFormatted[0] + "m " + timeFormatted[1] + "s";
            if (timeFormatted[1] == 0 && Math.Abs(percentageProgress - 100) < 0)
            {
                return "Pronto";
            }
            return timeFormatted[1] + "s";
        }
    }
}
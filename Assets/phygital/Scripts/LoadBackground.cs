using System.Collections;
using phygital.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace phygital.Scripts
{
    public class LoadBackground : MonoBehaviour
    {
        private static LoadBackground Instance { get; set; }
        [SerializeField] private RawImage background;
        [SerializeField] private Animator enterBackground;
        [SerializeField] private GameObject waitLogo;

        private static readonly int Enter = Animator.StringToHash("enter");

        private void Awake()
        {
            Instance = this;
        }

        public static IEnumerator GetTexture(string textureURL) {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(textureURL);
            if (Env.ENV != "PROD")
            {
                www.certificateHandler = new AcceptAllCertificatesSignedWithASpecificPublicKey();

            }
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                print("Errore nel recupero immagine: "+www.error);
            }
            else {
                Instance.background.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Instance.enterBackground.SetTrigger(Enter);
                while (!Instance.enterBackground.GetCurrentAnimatorStateInfo(0).IsName("enterIdleBackground"))
                {
                    yield return null;
                }
                Instance.waitLogo.SetActive(false);
            }
        }
    }
}

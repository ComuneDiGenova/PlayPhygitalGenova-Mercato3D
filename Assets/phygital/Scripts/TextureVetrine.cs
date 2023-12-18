using System.Collections;
using System.Collections.Generic;
using phygital.Json;
using phygital.Json.DataClasses;
using UnityEngine;
using UnityEngine.Networking;
using static phygital.Json.RetrieveData;

namespace phygital.Scripts
{
    public class TextureVetrine : MonoBehaviour
    {
        [SerializeField] private List<GameObject> shops;
        private static TextureVetrine Instance { get; set; }
        private List<GameObject> shopTemp;

        private void Start()
        {
            Instance = this;
        }

        public static TextureVetrine GetTextureVetrineInstance()
        {
            return Instance;
        }

        public List<GameObject> GetShops()
        {
            return Instance.shops;
        }
        
        public void LoadBottega(Bottega bottega)
        {
            UnusedShop.SetUnusedShops(shops);
            var t = shops.Find(x => x.tag.Equals(bottega.ordinamento_mercato_3d));
            if (!t)
            {
                // TODO Disable unused shop
                UnusedShop.RemoveUsedShops(t);
                return;
            }

            UnusedShop.RemoveUsedShops(t);
            
            t.TryGetComponent(out Renderer tRenderer);
            if (bottega.immagine_vetrina != "")
            {
                StartCoroutine(GetTexture(bottega.immagine_vetrina,tRenderer.materials[1]));
                                    
            }
                
            if (bottega.immagine_insegna != "")
            {
                StartCoroutine(GetTexture(bottega.immagine_insegna,tRenderer.materials[2]));
                                    
            }
        }

        private IEnumerator GetTexture(string imagePath, Material material) {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(imagePath);
            if (Env.ENV != "PROD")
            {
                www.certificateHandler = new AcceptAllCertificatesSignedWithASpecificPublicKey();

            }
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                print("Errore nel recupero vetrine negozi storici"+www.responseCode+" "+www.error);
            }
            else
            {
                var texture = (DownloadHandlerTexture)www.downloadHandler;
                texture.texture.Compress(true);
                material.mainTexture = texture.texture;
            }
        }
    }
}

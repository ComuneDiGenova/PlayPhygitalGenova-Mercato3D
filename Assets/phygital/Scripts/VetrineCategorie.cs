using System.Collections;
using System.Collections.Generic;
using phygital.Json;
using phygital.Json.DataClasses;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using Utility = phygital.Json.Utility;

namespace phygital.Scripts
{
    public class VetrineCategorie : MonoBehaviour
    {
        [SerializeField] private List<GameObject> categorie;

        private static VetrineCategorie Instance { get; set; }
        
        private void Start()
        {
            Instance = this;
        }

        public static VetrineCategorie GetVetrineInstance()
        {
            return Instance;
        }

        public void LoadVetrine(List<Categoria> listaCategorie)
        {
            foreach (var t in categorie)
            {
                var index = int.Parse(t.tag);
                if (listaCategorie.Count <= index) continue;
                var categoria = listaCategorie[index];
                if (categoria.immagine_vetrina == "" || categoria.nome == "Vuoto" || Utility.IsCategoryEmpty(categoria.nome))
                {
                    UnusedShop.DisableCategoryMarker(index);
                    t.layer = 15;
                    continue;
                }
                t.TryGetComponent(out Renderer tRenderer);
                StartCoroutine(GetTexture(categoria.immagine_vetrina, tRenderer.materials[1], index));

            }
        }
        
        private static IEnumerator GetTexture(string imagePath, Material material, int index) {
            var www = UnityWebRequestTexture.GetTexture(imagePath);
            if (Env.ENV != "PROD")
            {
                www.certificateHandler = new AcceptAllCertificatesSignedWithASpecificPublicKey();
            }
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                print("Errore nel recupero vetrine categorie: "+www.responseCode+"\n"+www.error);
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

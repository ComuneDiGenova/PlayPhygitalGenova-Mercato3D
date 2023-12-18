using System.Collections;
using phygital.Json.DataClasses;
using static phygital.Json.RetrieveData;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace phygital.Scripts
{
    public class ProductPage : MonoBehaviour
    {
        private static ProductPage Instance { get; set; }
        
        [SerializeField] private GameObject productPage;
        [SerializeField] private TextMeshProUGUI pName;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Image image;

        private Prodotti _product;  

        public static ProductPage GetInstance()
        {
            return Instance;
        }
        
        public GameObject GetProductPage(string ptag)
        {
            _product = GetProdotto(ptag);
            
            pName.text = _product.nome;
            description.text = _product.descrizione;
            if (_product.immagine != "")
            { 
                StartCoroutine(GetTexture(_product.immagine));
            }
            else image.transform.gameObject.SetActive(false);
            return productPage;
        }
        
        private IEnumerator GetTexture(string imagePath) {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(imagePath);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                
            
            }
            else
            {
                var texture = (DownloadHandlerTexture)www.downloadHandler;
                image.sprite = Sprite.Create(texture.texture, new Rect(0.0f, 0.0f, image.sprite.texture.width, image.sprite.texture.height), new Vector2(0.5f, 0.5f));
            }
        }

        public void OpenPage(TextMeshProUGUI linkBox)
        {
            Application.OpenURL(linkBox.text);
        }
        public void OpenPageFromMap(TextMeshProUGUI linkBox)
        {
            Application.OpenURL(linkBox.text);
        }
    }
}

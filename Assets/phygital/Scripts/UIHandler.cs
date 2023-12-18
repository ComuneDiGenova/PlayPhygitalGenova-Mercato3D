using System;
using System.Collections;
using phygital.Json;
using phygital.Json.DataClasses;
using TMPro;
using static phygital.Json.RetrieveData;
using static phygital.Scripts.PlayerHandler;
using static phygital.Scripts.Env;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace phygital.Scripts
{
    public class UIHandler : MonoBehaviour
    {
        private static UIHandler Instance { get; set; }
        [SerializeField] private GameObject pGenoviniUI;
        [SerializeField] private GameObject sGenoviniUI;
        private static readonly int CloseShop = Animator.StringToHash("closeShop");
        private static readonly int OpenShop = Animator.StringToHash("openShop");
        private static readonly int EarnGenovini = Animator.StringToHash("earnGenovini");

        [SerializeField] private GameObject productPage;
        [SerializeField] private TextMeshProUGUI pName;
        [SerializeField] private TextMeshProUGUI pId;
        [SerializeField] private TextMeshProUGUI pRecipe;
        [SerializeField] private GameObject pRecipeBox;
        [SerializeField] private TextMeshProUGUI pDescription;
        [SerializeField] private GameObject pScrollView;
        [SerializeField] private TextMeshProUGUI pLink;
        [SerializeField] private Image pImage;

        [SerializeField] private GameObject ancientShopPage;
        [SerializeField] private TextMeshProUGUI bName;
        [SerializeField] private TextMeshProUGUI bDescription;
        [SerializeField] private GameObject bScrollView;
        [SerializeField] private TextMeshProUGUI bLink;
        [SerializeField] private Image bImage;

        [SerializeField] private GameObject categoryShopPage;
        [SerializeField] private TextMeshProUGUI cName;
        [SerializeField] private TextMeshProUGUI cDescription;
        [SerializeField] private GameObject cScrollView;
        [SerializeField] private TextMeshProUGUI cLink;
        [SerializeField] private Image cImage;

        [SerializeField] private PostData postData;


        private Prodotti _product;
        private Bottega _bottega;
        private Categoria _categoria;

        private static bool _buttonClick;

        private void Awake()
        {
            Instance = this;
        }

        private static IEnumerator CheckAndCloseUI(string tag, int layer)
        {
            var ui = RetrieveUI(tag, layer, "close");
            ui.TryGetComponent(out Animator component);
            component.SetTrigger(CloseShop);
            while (!component.GetCurrentAnimatorStateInfo(0).IsName("idleOutShop"))
            {
                yield return null;
            }
            ui.SetActive(false);
            UIOPEN = false;
        }

        public static IEnumerator InputToCloseUI(GameObject target, int layer)
        {
            while (!Input.GetKeyDown(KeyCode.Q) && !Input.GetKeyDown(KeyCode.Mouse1) && !_buttonClick)
            {
                yield return null;
            }

            UIOPEN = false;
            yield return CheckAndCloseUI(target.tag, layer);
            yield return UnlockInput();
        }

        public static void SetButtonClicked()
        {
            _buttonClick = true;
        }
        
        public static IEnumerator InvokeUI (string tag, int layer)
        {
            if (UIOPEN) yield break;
            _buttonClick = false;
            var ui = RetrieveUI(tag, layer, "open");
            if (ui.activeInHierarchy) yield break;
            ui.SetActive(true);
            ui.TryGetComponent(out Animator anim);
            anim.SetTrigger(OpenShop);

            switch (layer)
            {
                case (int)LayerIndexes.Interagibili:
                {
                    Instance.pGenoviniUI.TryGetComponent(out Animator notify);
                    if (USER_ID == "Anonimo")
                    {
                        break;
                    }
                    notify.SetTrigger(EarnGenovini);
                    break;
                }
                case (int)LayerIndexes.Botteghe:
                {
                    Instance.sGenoviniUI.TryGetComponent(out Animator notify);
                    if (USER_ID == "Anonimo")
                    {
                        break;
                    }
                    notify.SetTrigger(EarnGenovini);
                    break;
                }
            }

            UIOPEN = true;
        }

        private static GameObject RetrieveUI(string uiTag, int layer, string action)
        {
            return layer switch
            {
                (int)LayerIndexes.Botteghe => Instance.GetAncientShopPage(uiTag, action),
                (int)LayerIndexes.Categorie => Instance.GetCategoryShopPage(uiTag),
                (int)LayerIndexes.Interagibili => Instance.GetProductPage(uiTag, action),
                _ => new GameObject()
            };
        }

        private GameObject GetAncientShopPage(string ptag, string action)
        {
            _bottega = GetBottega(ptag);
            bName.text = _bottega.nome;
            bDescription.text = _bottega.descrizione;
            bLink.text = _bottega.link;
            bScrollView.SetActive(bDescription.text != "");
            if (_bottega.immagine_di_copertina != "")
            { 
                StartCoroutine(GetTexture(_bottega.immagine_vetrina, bImage));
                bImage.transform.gameObject.SetActive(true);
            } else bImage.transform.gameObject.SetActive(false);

            if (bName.text != "" && action == "open")
            {
                postData.GetPointResult("negozio", _bottega.id);
                
            }
            return ancientShopPage;
        }

        private GameObject GetCategoryShopPage(string ptag)
        {
            _categoria = GetCategoria(ptag);
            cName.text = _categoria.nome;
            cDescription.text = _categoria.descrizione;
            cLink.text = _categoria.url;
            cScrollView.SetActive(cDescription.text != "");
            if (_categoria.immagine_di_copertina != "")
            { 
                StartCoroutine(GetTexture(_categoria.immagine_di_copertina, cImage));
                cImage.transform.gameObject.SetActive(true);
            }
            else cImage.transform.gameObject.SetActive(false);
            return categoryShopPage;
        }

        private GameObject GetProductPage(string ptag, string action)
        {
            _product = GetProdotto(ptag);
            pName.text = _product.nome;
            pId.text = _product.id;
            pDescription.text = _product.descrizione;
            pLink.text = _product.link;

            if (_product.ricette.Count > 0)
            {
                string temp = null;
                foreach (var t in _product.ricette)
                {
                    temp = string.IsNullOrEmpty(temp) ? t : "," + t;
                }

                pRecipe.text = temp;
                //pRecipeBox.SetActive(true);
                // Disabled at the moment 
                pRecipeBox.SetActive(false);
            } else pRecipeBox.SetActive(false);
            pScrollView.SetActive(pDescription.text != "");
            if (_product.immagine != "")
            { 
                StartCoroutine(GetTexture(_product.immagine, pImage));
                pImage.transform.gameObject.SetActive(true);
            }
            else pImage.transform.gameObject.SetActive(false);

            if (pName.text != "" && action == "open")
            {
                postData.GetPointResult("prodotto", _product.id);
                
            }
            return productPage;
        }
        private static IEnumerator GetTexture(string imagePath, Image image) {
            var www = UnityWebRequestTexture.GetTexture(imagePath);
            if (ENV != "PROD")
            {
                www.certificateHandler = new AcceptAllCertificatesSignedWithASpecificPublicKey();
            }
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                print("Errore nel caricamento dell'immagine: "+www.responseCode+" "+www.error);
            }
            else
            {
                var temp = (DownloadHandlerTexture)www.downloadHandler;
                var texture = new Texture2D(512, 512);
                texture.LoadImage(temp.texture.EncodeToPNG());
                texture = ScaleTexture(texture, 512, 256);
                texture.Compress(true);
                image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            }
        }
        private static Texture2D ScaleTexture(Texture2D source,int targetWidth,int targetHeight) {
            var result=new Texture2D(targetWidth,targetHeight,source.format,true);
            var rPixels=result.GetPixels(0);
            var incX=(1.0f / targetWidth);
            var incY=(1.0f / targetHeight); 
            for(var px=0; px<rPixels.Length; px++) { 
                // ReSharper disable once PossibleLossOfFraction
                rPixels[px] = source.GetPixelBilinear(incX*((float)px%targetWidth), incY*Mathf.Floor(px/targetWidth)); 
            } 
            result.SetPixels(rPixels,0); 
            result.Apply(); 
            return result; 
        }
    }
}

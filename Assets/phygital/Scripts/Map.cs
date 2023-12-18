using System;
using System.Collections.Generic;
using phygital.Json;
using phygital.Json.DataClasses;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static phygital.Scripts.PlayerHandler;
using static phygital.Scripts.Env;

namespace phygital.Scripts
{
    public class Map : MonoBehaviour
    {
        
        [SerializeField] private GameObject minimap;
        [SerializeField] private GameObject map;
        [SerializeField] private List<TextMeshProUGUI> categoryLabel;
        [SerializeField] private List<Button> categoryButton;
        [SerializeField] private List<GameObject> categoryModal;
        [SerializeField] private List<TextMeshProUGUI> categoryModalLabel;
        [SerializeField] private List<TextMeshProUGUI> categoryModalB1;
        [SerializeField] private List<TextMeshProUGUI> categoryModalB2;
        [SerializeField] private List<Button> shopButton;
        [SerializeField] private List<GameObject> shopModal;
        [SerializeField] private List<TextMeshProUGUI> shopCategoryModalLabel;
        [SerializeField] private List<TextMeshProUGUI> shopModalLabel;
        [SerializeField] private List<TextMeshProUGUI> shopModalB1;
        [SerializeField] private List<TextMeshProUGUI> shopModalB2;
        [SerializeField] private List<Button> productButton;
        [SerializeField] private List<TextMeshProUGUI> productModalLabel;
        [SerializeField] private List<TextMeshProUGUI> productModalB1;

        private static Map Instance { get; set; }
        private static List<string> _nomiCategorie = new List<string>();
        private static List<string> _nomiBotteghe = new List<string>();

        private readonly List<Button> _pGroupTempPrev = new List<Button>();
        private Button _cTempPrev;
        private readonly List<Button> _sGroupTempPrev = new List<Button>();
        private GameObject _cModalTempPrev;
        private GameObject _pModalTempPrev;
        private GameObject _sModalTempPrev;

        private void Awake()
        {
            Instance = this;
        }

        // Update is called once per frame
        private void Update()
        {
            
            if (map.activeSelf && Input.GetKeyDown(KeyCode.Mouse0) && EventSystem.current.currentSelectedGameObject is null)
            {
                CleanPreviousButtons();
            }
            
            if (Input.GetKeyDown(KeyCode.M))
            {
                OpenCloseMap(map.activeSelf);
            }
            
        }

        public static Map GetInstance()
        {
            return Instance;
        }

        public void SetCategorieAndBotteghe(List<Categoria> categorie, List<Bottega> botteghe)
        {
            // Init categorie

            var empty = categorie.FindAll(x => Utility.IsCategoryEmpty(x.nome));
            
            foreach (var t in categorie){

                if (Utility.IsCategoryEmpty(t.nome)) continue;
                _nomiCategorie.Add(t.nome);
            }

            _nomiCategorie.Sort();
            Locale.SetMapCategoryModalLocale(categoryModalB1, categoryModalB2);
            for (var i = 0; i < _nomiCategorie.Count; i++)
            {
                categoryLabel[i].text = _nomiCategorie[i];
                categoryLabel[i].tag = categorie.Find(x => x.nome.Equals(_nomiCategorie[i])).ordinamento;

                if (Utility.IsCategoryEmpty(categoryLabel[i].text))
                {
                    categoryLabel[i].text = "";
                    categoryButton.Find(x => x.tag.Equals(categoryLabel[i].tag)).interactable = false;
                }
                else
                {
                    var modalText = categoryModalLabel.Find(x => x.tag.Equals(categoryLabel[i].tag));
                    modalText.text = _nomiCategorie[i];
                }


                
                if (i == _nomiCategorie.Count - 1 && _nomiCategorie.Count < categoryLabel.Count)
                {
                    for (var j = i + 1; j < categoryLabel.Count; j++)
                    {
                        categoryLabel[j].text = "";
                    }

                    foreach (var catLabel in categoryModalLabel)
                    {
                        if (catLabel.text.Equals("Nome Categoria"))
                        {
                            categoryButton.Find(x => x.tag.Equals(catLabel.tag)).interactable = false;
                        }
                    }
                }
            }
            
            // Init botteghe
            
            foreach (var t in botteghe){

                _nomiBotteghe.Add(t.nome);
            }

            var addedShops = new List<int>();
            
            for (var i = 0; i < _nomiBotteghe.Count && i < shopModalLabel.Count; i++)
            {
                var shopIndexString = botteghe.Find(x => x.nome.Equals(_nomiBotteghe[i])).ordinamento_mercato_3d;
                if (shopIndexString.Length == 0) continue;
                var shopIndex = shopModalLabel.FindIndex(x => x.name[6..].Equals(shopIndexString));
                shopModalLabel[shopIndex].text = _nomiBotteghe[i];
                addedShops.Add(shopIndex);
            }

            for (var i = 0; i < shopModalLabel.Count; i++)
            {
                bool isPresent = false;
                for (var j = 0; j < addedShops.Count; j++)
                {
                    if (i == addedShops[j]) { isPresent = true; continue; }
                    if (j == addedShops.Count - 1 && !isPresent) shopButton[i].interactable = false;
                }
            }

            for (var i = 0; i < _nomiBotteghe.Count && i < shopModalLabel.Count; i++)
            {
                var catTempID = botteghe.Find(x => x.nome.Equals(_nomiBotteghe[i])).id_categoria ?? "";
                var shopIndexString = botteghe.Find(x => x.nome.Equals(_nomiBotteghe[i])).ordinamento_mercato_3d;
                if (shopIndexString.Length == 0) continue;
                var shopIndex = shopModalLabel.FindIndex(x => x.name[6..].Equals(shopIndexString));
                if (catTempID != "" && categorie.Exists(x => x.id.Equals(catTempID)))
                {
                    var catName = categorie.Find(x => x.id.Equals(catTempID)).nome;
                    shopCategoryModalLabel[shopIndex].text = catName ?? "";
                } else shopCategoryModalLabel[shopIndex].text = "";
            }

            if (botteghe.Count == 0)
            {
                foreach (var button in shopButton)
                {
                    button.interactable = false;
                }
            }
            Locale.SetMapShopModalLocale(shopModalB1, shopModalB2);
            
            // Init prodotti
            
            for (var i = 0; i < productModalLabel.Count; i++)
            {
                Locale.SetMapProductModalLocale(productModalLabel[i], productModalB1[i]);
            }
            
        }
        
        public void BindButtons(GameObject label)
        {
            CleanPreviousButtons();
            var tempButton = categoryButton.Find(x => x.tag.Equals(label.tag));
            tempButton.OnSelect(null);
            var catName = RetrieveData.GetCategorie().Find(x => x.ordinamento.Equals(label.tag)).id;
            var relatedShops = RetrieveData.GetBotteghe().FindAll(x => x.id_categoria.Equals(catName));
            if (relatedShops.Count > 0)
            {
                foreach (var t in relatedShops)
                {
                    var shop = shopButton.Find(x => x.tag.Equals(t.ordinamento_mercato_3d));
                    if (shop is null) continue;
                    shop.OnSelect(null);
                    _sGroupTempPrev.Add(shop);
                }
            }
            _cTempPrev = tempButton;
        }

        public void BindProductButtons(GameObject label)
        {
            CleanPreviousButtons();
            var tempTrue = productButton.FindAll(x => x.tag.Equals(label.tag));
            var tempFalse = productButton.FindAll(x => !x.CompareTag(label.tag));
            foreach (var t in tempTrue)
            {
                t.OnSelect(null);
            }

            foreach (var t in tempFalse)
            {
                t.OnDeselect(null);
            }

            _pGroupTempPrev.AddRange(tempTrue);
            _pGroupTempPrev.AddRange(tempFalse);
        }

        private void CleanPreviousButtons()
        {
            if (_pGroupTempPrev is { Count: > 0 })
            {
                foreach (var t in _pGroupTempPrev)
                {
                    t.OnDeselect(null);
                }
                _pGroupTempPrev.Clear();
            }

            if (_cTempPrev is not null)
            {
                _cTempPrev.OnDeselect(null);
            }
            if (_sGroupTempPrev is { Count: > 0 })
            {
                foreach (var t in _sGroupTempPrev)
                {
                    t.OnDeselect(null);
                }
                _sGroupTempPrev.Clear();
            }

            if (_cModalTempPrev is not null)
            { 
                _cModalTempPrev.SetActive(false);
            }
            if (_pModalTempPrev is not null)
            { 
                _pModalTempPrev.SetActive(false);
            }
            if (_sModalTempPrev is not null)
            { 
                _sModalTempPrev.SetActive(false);
            }

            if (!map.activeSelf){
                foreach (var t in categoryButton)
                {
                    if (!t.interactable) continue;
                    t.interactable = false;
                    t.interactable = true;
                }
                foreach (var t in productButton)
                {
                    t.interactable = false;
                    t.interactable = true;
                }
                foreach (var t in shopButton)
                {
                    if (!t.interactable) continue;
                    t.interactable = false;
                    t.interactable = true;
                }
            }
        }

        public void ToggleModal(GameObject modal)
        {
            CleanPreviousButtons();
            modal.SetActive(!modal.activeSelf);
            _cModalTempPrev = modal;
            var tempFalse = categoryModal.FindAll(x => !x.CompareTag(modal.tag));
            foreach (var t in tempFalse)
            {
                t.SetActive(false);
            }
        }
        public void ToggleProductModal(GameObject modal)
        {
            if (_cModalTempPrev is not null) _cModalTempPrev.SetActive(false);
            if (_pModalTempPrev is not null) _pModalTempPrev.SetActive(false);
            if (_sModalTempPrev is not null) _sModalTempPrev.SetActive(false);
            modal.SetActive(!modal.activeSelf);
            _pModalTempPrev = modal;
            var tempFalse = shopModal.FindAll(x => !x.CompareTag(modal.tag));
            foreach (var t in tempFalse)
            {
                t.SetActive(false);
            }
        }
        public void ToggleShopModal(GameObject modal)
        {
            CleanPreviousButtons();
            modal.SetActive(!modal.activeSelf);
            _sModalTempPrev = modal;
            var tempFalse = shopModal.FindAll(x => !x.CompareTag(modal.tag));
            foreach (var t in tempFalse)
            {
                t.SetActive(false);
            }
        }

        public void GoTo(GameObject target)
        {
            MovePlayerTo(target);
            OpenCloseMap(true);
        }

        public void OpenCategoryWebPage(GameObject categoryBtn)
        {
            var link = RetrieveData.GetCategorie().Find(x => x.ordinamento.Equals(categoryBtn.tag)).url;
            Application.OpenURL(link);
        }
        
        private void OpenCloseMap(bool isMapOpen)
        {
            if (UIOPEN)
            {
                if (!isMapOpen) return;
                map.SetActive(false);
                CleanPreviousButtons();
                StartCoroutine(UnlockInput());
                minimap.SetActive(true);
                UIOPEN = false;
                return;
            } 
            map.SetActive(true);
            minimap.SetActive(false);
            UIOPEN = true;
            StartCoroutine(LockInput());
        }
    }
}

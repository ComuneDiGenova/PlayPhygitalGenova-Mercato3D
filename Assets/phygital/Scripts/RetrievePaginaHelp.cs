using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using phygital.Json;
using phygital.Json.DataClasses;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using static phygital.Scripts.Env;
using Utility = phygital.Json.Utility;

namespace phygital.Scripts
{
    public class RetrievePaginaHelp : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleTextHelpLoading;
        [SerializeField] private TextMeshProUGUI textHelpLoading;

        [DllImport("__Internal")]
        private static extern string GetURLFromQueryStr();
        private string _mercatoAllEndpoint = "jsonapi/api-mercato3d-content-all";
        private readonly string _userDataEndpoint = "jsonapi/user-get-info/";
        private UnityWebRequest _mercatoRequest;
        private UnityWebRequest _userRequest;
        private static List<Mercato> _mercato;
        private static List<PaginaHelp> _paginaHelp;
        private static UserData _userData;
        public static string _assetBundleBaseUrl = "https://mercato3dt.comune.genova.it";
    
    
        // Start is called before the first frame update
        void Start()
        {
            if (Application.isEditor)
            {
                ENV = "LOCAL";
                LANGUAGE = "de";
            }
            else if(Application.absoluteURL.Contains("https://mercato3dt.comune.genova.it/"))
            {
                ENV = "TEST";
                TOKEN = TOKEN_TEST;
                API_URL = API_URL_TEST;
                _assetBundleBaseUrl = "https://mercato3dt.comune.genova.it";
            }
            else
            {
                ENV = "PROD";
                TOKEN = TOKEN_PROD;
                API_URL = API_URL_PROD;
                _assetBundleBaseUrl = "https://mercato3d.comune.genova.it";
            }
            if (ENV is "PROD" or "TEST")
            {
                var url = GetURLFromQueryStr();
                var parameters = url.Split("?")[1].Split("&");
                var id_user = parameters[0].Split("=")[1];
                var lang = parameters[1].Split("=")[1];
                SetUserID(id_user);
                SetIntroLanguageByCode(lang);
            }
            if (ENV == "LOCAL")
            {
                SetUserID("Anonimo");
                SetIntroLanguageByCode(LANGUAGE);
            }

            if (USER_ID != "Anonimo")
            {
                StartCoroutine(DownloadUserData(USER_ID));
            }
            StartCoroutine(DownloadMercatoAll());
        }

        private static List<PaginaHelp> InitPagineHelp(List<Mercato> mercato)
        {
            _paginaHelp = new List<PaginaHelp>();
            for (var i = 0; i < mercato.Count; i++)
            {
                if (mercato[i].tipo_di_contenuto.Equals("Pagina Help (App)") || mercato[i].app.Equals("Mercato 3D"))
                {
                    _paginaHelp.Add(Utility.RetrievePaginaHelpByIndex(mercato, i));
                }
            }
            return _paginaHelp;
        }
    
        private IEnumerator DownloadMercatoAll()
        {
            using (_mercatoRequest = UnityWebRequest.Get(API_URL+_mercatoAllEndpoint))

            {
                if (ENV != "PROD")
                {
                    _mercatoRequest.certificateHandler = new AcceptAllCertificatesSignedWithASpecificPublicKey();

                }
                _mercatoRequest.SetRequestHeader("AUTHORIZATION", TOKEN);
                yield return _mercatoRequest.SendWebRequest();
                if (_mercatoRequest.result != UnityWebRequest.Result.Success)
                {
                    print("Errore ricezione dati Mercato 3D: "+_mercatoRequest.responseCode+" "+_mercatoRequest.error);
                }
                else
                {
                    if (!_mercatoRequest.isDone) yield break;
                    var json = DownloadHandlerBuffer.GetContent(_mercatoRequest);
                    _mercato = new List<Mercato>();
                    _mercato = JsonConvert.DeserializeObject<List<Mercato>>(json);
                    _mercato = _mercato.FindAll(x => x.lingua == GetLanguage());
                    _paginaHelp = InitPagineHelp(_mercato);
                    var introHelp = PaginaHelp.GetIntroHelp(_paginaHelp);
                    var titleIntroHelp = PaginaHelp.GetTitleIntroHelp(_paginaHelp);
                    titleTextHelpLoading.text = titleIntroHelp.testo;
                    textHelpLoading.text = introHelp.testo;
                    StartCoroutine(LoadBackground.GetTexture(introHelp.immagine_di_copertina));
                }
            }
        }
    
        private IEnumerator DownloadUserData(string userID)
        {
            using (_userRequest = UnityWebRequest.Get(API_URL+_userDataEndpoint+userID))

            {
                if (ENV != "PROD")
                {
                    _userRequest.certificateHandler = new AcceptAllCertificatesSignedWithASpecificPublicKey();

                }
                _userRequest.SetRequestHeader("AUTHORIZATION", TOKEN);
                yield return _userRequest.SendWebRequest();
                if (_userRequest.result != UnityWebRequest.Result.Success)
                {
                    print("Errore ricezione dati utente: "+_userRequest.responseCode+" "+_userRequest.error);
                }
                else
                {
                    if (!_userRequest.isDone) yield break;
                    var json = DownloadHandlerBuffer.GetContent(_userRequest);
                    _userData = JsonConvert.DeserializeObject<List<UserData>>(json)[0];
                    SetIntroLanguage(_userData.lingua);
                }
            }
        }
    }
}

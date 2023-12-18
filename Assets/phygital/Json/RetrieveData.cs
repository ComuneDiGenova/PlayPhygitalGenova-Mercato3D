using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using phygital.Json.DataClasses;
using phygital.Scripts;
using static phygital.Json.DataClasses.Prodotti;
using static phygital.Json.DataClasses.Ricette;
using static phygital.Json.DataClasses.Bottega;
using static phygital.Json.DataClasses.Categoria;
using static phygital.Scripts.VetrineCategorie;
using static phygital.Scripts.TextureVetrine;
using static phygital.Scripts.Env;
using UnityEngine;
using UnityEngine.Networking;
using UserData = phygital.Json.DataClasses.UserData;

namespace phygital.Json
{
    public class RetrieveData : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern string GetURLFromQueryStr();
        private readonly string _mercatoAllEndpoint = "jsonapi/api-mercato3d-content-all";
        private readonly string _taxonomyAllEndpoint = "jsonapi/api-mercato3d-taxonomy-all";
        private readonly string _userDataEndpoint = "jsonapi/user-get-info/";
        
        private UnityWebRequest _mercatoRequest;
        private UnityWebRequest _taxonomyRequest;
        private UnityWebRequest _userRequest;
        private static List<Mercato> _mercato;
        private static List<Prodotti> _prodotti;
        private static List<Bottega> _botteghe;
        private static List<Categoria> _categoria;
        private static List<Ricette> _ricette;
        private static List<PaginaHelp> _paginaHelp;
        private static VetrineCategorie _vetrineCategoria;
        private static UserData _userData;

        public static List<Mercato> GetMercato()
        {
            return _mercato;
        }

        public static Prodotti GetProdotto(string ptag)
        {
            return IsProdotto(_prodotti, ptag) ? GetProdottoById(_prodotti, ptag) : GetRicettaAsProdotto(ptag);
        }
        public static Ricette GetRicetta(string ptag)
        {
            return GetRicettaById(_ricette, ptag);
        }

        private static Prodotti GetRicettaAsProdotto(string ptag)
        {
            var ricetta = GetRicetta(ptag);
            return new Prodotti
            {
                id = ricetta.id,
                nome = ricetta.nome,
                descrizione = ricetta.descrizione,
                link = ricetta.link,
                immagine = ricetta.immagine,
                prodotti_tipici = ricetta.prodotti_tipici,
                tipo_di_prodotto = ricetta.tipo_di_prodotto,
                ricette = new List<string>(),
                negozi_di_appartenenza = ricetta.negozi_di_appartenenza,
                lingua = ricetta.lingua,
            };
        }
        public static Bottega GetBottega(string ptag)
        {
            return GetBottegaByName(_botteghe, ptag);
        }
        public static Categoria GetCategoria(string ptag)
        {
            return GetCategoriaByName(_categoria, ptag);
        }
        public static List<Categoria> GetCategorie()
        {
            return _categoria ?? new List<Categoria>();
        }
        
        public static List<Bottega> GetBotteghe()
        {
            return _botteghe ?? new List<Bottega>();
        }
        
        public static List<PaginaHelp> GetPaginaHelps()
        {
            return _paginaHelp;
        }
        
        public static UserData GetUserData()
        {
            return _userData;
        }

        private static List<Prodotti> InitProdotti(List<Mercato> mercato)
        {
            _prodotti = new List<Prodotti>();
            for (var i = 0; i < mercato.Count; i++)
            {
                if (mercato[i].tipo_di_contenuto.Equals("Prodotto") || mercato[i].tipo_di_contenuto.Equals("Prodotto tipico"))
                {
                    _prodotti.Add(Utility.RetrieveProdottoByIndex(mercato, i));
                    var temp = _ricette.FindAll(x => x.prodotti_tipici.Contains(_prodotti[^1].nome));
                    foreach (var t in temp)
                    {
                        _prodotti[^1].ricette.Add(t.nome);
                    }
                }
            }

            return _prodotti;
        }
        private static List<Ricette> InitRicette(List<Mercato> mercato)
        {
            _ricette = new List<Ricette>();
            for (var i = 0; i < mercato.Count; i++)
            {
                if (mercato[i].tipo_di_contenuto.Equals("Ricetta"))
                {
                    _ricette.Add(Utility.RetrieveRicettaByIndex(mercato, i));
                }
            }

            return _ricette;
        }
        private static List<Bottega> InitBottega(List<Mercato> mercato)
        {
            _botteghe = new List<Bottega>();
            for (var i = 0; i < mercato.Count; i++)
            {
                if (mercato[i].tipologia_di_negozio.Equals("Botteghe Storiche") || mercato[i].tipologia_di_negozio.Equals("Locale di tradizione") )
                {
                    _botteghe.Add(Utility.RetrieveBottegaByIndex(mercato, i));
                }
            }

            foreach (var t in _botteghe)
            {
                GetTextureVetrineInstance().LoadBottega(t);
            }
            UnusedShop.DisableUnused();
            return _botteghe;
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
        
        private void Start()
        {
            if (ENV is "PROD" or "TEST")
            {
                var url = GetURLFromQueryStr();
                var parameters = url.Split("?")[1].Split("&");
                var id_user = parameters[0].Split("=")[1];
                var lang = parameters[1].Split("=")[1];
                SetUserID(id_user);
                SetLanguageByCode(lang);
            }
            if (ENV == "LOCAL")
            {
                //SetUserID("Anonimo");
                SetLanguageByCode(LANGUAGE);
            }

            if (USER_ID != "Anonimo")
            {
                StartCoroutine(DownloadUserData(USER_ID));
            }
            StartCoroutine(DownloadMercatoAll());
            StartCoroutine(DownloadTaxonomyAll());
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
                    _ricette = InitRicette(_mercato);
                    _prodotti = InitProdotti(_mercato);
                    _botteghe = InitBottega(_mercato);
                    _paginaHelp = InitPagineHelp(_mercato);
                }
            }
        }
        private IEnumerator DownloadTaxonomyAll()
        {
            using (_taxonomyRequest = UnityWebRequest.Get(API_URL+_taxonomyAllEndpoint))

            {
                if (ENV != "PROD")
                {
                    _taxonomyRequest.certificateHandler = new AcceptAllCertificatesSignedWithASpecificPublicKey();
                }
                _taxonomyRequest.SetRequestHeader("AUTHORIZATION", TOKEN);
                yield return _taxonomyRequest.SendWebRequest();
                if (_taxonomyRequest.result != UnityWebRequest.Result.Success)
                {
                    print("Errore ricezione elenco categorie: "+_taxonomyRequest.responseCode+" "+_taxonomyRequest.error);
                }
                else
                {
                    if (!_taxonomyRequest.isDone) yield break;
                    var json = DownloadHandlerBuffer.GetContent(_taxonomyRequest);
                    _categoria = new List<Categoria>();
                    _categoria = JsonConvert.DeserializeObject<List<Categoria>>(json);
                    _categoria = _categoria.FindAll(x => x.lang == GetLanguage());
                    GetVetrineInstance().LoadVetrine(_categoria);
                    //while (_botteghe is null || _botteghe.Count == 0) yield return null;
                    while (_botteghe is null) yield return null;
                    Utility.InitMap(_categoria, _botteghe);
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
                    SetLanguage(_userData.lingua);
                }
            }
        }
    }
}
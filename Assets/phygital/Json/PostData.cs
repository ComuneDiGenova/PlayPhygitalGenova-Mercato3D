using System.Collections;
using Newtonsoft.Json;
using phygital.Json.DataClasses;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using static phygital.Scripts.Env;
using static phygital.Scripts.Locale;

namespace phygital.Json
{
    public class PostData : MonoBehaviour
    {

        private readonly string _addPointsEndpoint = API_URL+"jsonapi/user/add_points";
        private readonly string _addCategoryShopEndpoint = API_URL+"jsonapi/user/add_preferiti_categorie_negozio";

        [SerializeField] private Animator pGemovini;
        [SerializeField] private TextMeshProUGUI pGemoviniAmount;
        [SerializeField] private TextMeshProUGUI pGemoviniMessage;
        [SerializeField] private TextMeshProUGUI sGemoviniAmount;
        [SerializeField] private TextMeshProUGUI sGemoviniMessage;

        private Point _pointResult;
        
        private UnityWebRequest _postRequest;
        private UnityWebRequest _postRequest2;
        private static readonly int EarnGenovini = Animator.StringToHash("earnGenovini");

        public void GetPointResult(string contentType, string contentId)
        {
            switch (contentType)
            {
                case "negozio":
                {
                    if (USER_ID == "Anonimo")
                    {
                        sGemoviniAmount.SetText(new Point().points);
                        sGemoviniMessage.SetText(new Point().message);
                        break;
                    }
                    StartCoroutine(AddPoints(USER_ID, "visualizzazione", contentType, contentId, _pointResult, sGemoviniAmount, sGemoviniMessage));
                    break;
                }
                case "prodotto":
                {
                    if (USER_ID == "Anonimo")
                    {
                        pGemoviniAmount.SetText(new Point().points);
                        pGemoviniMessage.SetText(new Point().message);
                        break;
                    }
                    StartCoroutine(AddPoints(USER_ID, "visualizzazione", contentType, contentId, _pointResult, pGemoviniAmount, pGemoviniMessage));
                    break;
                }
            }
        }

        public void AddToPreferiti(TextMeshProUGUI id)
        {
            if (USER_ID == "Anonimo")
            {
                pGemoviniAmount.SetText(new Point().points);
                pGemoviniMessage.SetText(new Point().message);
                pGemovini.SetTrigger(EarnGenovini);
                return;
            }

            StartCoroutine(AddCategoryShop(USER_ID, id.text));
        }
        
        private IEnumerator AddPoints(string userId, string action, string contentType, string contentId, Point pointResult, TextMeshProUGUI genoviniAmount, TextMeshProUGUI genoviniMessage)
        {
            var formData = new WWWForm();
            formData.AddField("user_id", userId);
            formData.AddField("action", action);
            formData.AddField("content_type", contentType);
            formData.AddField("content_id", contentId);
            using (_postRequest = UnityWebRequest.Post(_addPointsEndpoint, formData))

            {
                if (ENV != "PROD")
                {
                    _postRequest.certificateHandler = new AcceptAllCertificatesSignedWithASpecificPublicKey();

                }
                _postRequest.SetRequestHeader("AUTHORIZATION", TOKEN);
                _postRequest.SetRequestHeader("user-agent", "unity");
                _postRequest.timeout = 30;
                yield return _postRequest.SendWebRequest();
                if (_postRequest.result != UnityWebRequest.Result.Success)
                {
                    print("Errore accredito punti: "+_postRequest.responseCode+" "+_postRequest.error);
                    var errorMessage = POINT_MESSAGE.Split(",");
                    pGemoviniAmount.SetText(errorMessage[0]);
                    pGemoviniMessage.SetText(errorMessage[1]);
                }
                else
                {
                    var json = DownloadHandlerBuffer.GetContent(_postRequest);
                    pointResult = JsonConvert.DeserializeObject<Point>(json);
                    genoviniAmount.SetText(pointResult.points);
                    genoviniMessage.SetText(pointResult.message);
                }
            }
        }
        private IEnumerator AddCategoryShop(string uid, string nid)
        {
            var formData2 = new WWWForm();
            formData2.AddField("uid", uid);
            formData2.AddField("nid", nid);
            using (_postRequest2 = UnityWebRequest.Post(_addCategoryShopEndpoint, formData2))

            {
                if (ENV != "PROD")
                {
                    _postRequest2.certificateHandler = new AcceptAllCertificatesSignedWithASpecificPublicKey();

                }
                _postRequest2.SetRequestHeader("AUTHORIZATION", TOKEN);
                _postRequest2.SetRequestHeader("user-agent", "unity");
                _postRequest2.timeout = 30;
                yield return _postRequest2.SendWebRequest();
                if (_postRequest2.result != UnityWebRequest.Result.Success)
                {
                    print("Errore aggiunta ai preferiti: "+_postRequest2.responseCode+" "+_postRequest2.error);
                }
                else
                {
                    var json = DownloadHandlerBuffer.GetContent(_postRequest2);
                    var pointResult = JsonConvert.DeserializeObject<Point>(json);
                    pGemoviniAmount.SetText("");
                    pGemoviniMessage.SetText(pointResult.message);
                    pGemovini.SetTrigger(EarnGenovini);
                }
            }
        }
    }
}
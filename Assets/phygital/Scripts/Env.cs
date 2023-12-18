using System;
using System.Net;
using UnityEngine;

namespace phygital.Scripts
{
    public class Env : MonoBehaviour
    {
        [SerializeField] private string langByEditor = "it";
        public static string ENV = "";
        public static string ABSOLUTE_URL = "";
        public static string API_URL = "";
        public static float CAMERA_SENSIBILITY = 0.8f;
        public static string LANGUAGE = "it";
        public static bool UIOPEN = false;


        // WSO2 FIXED TOKEN
        public static string TOKEN = "";


        // LOCAL
        public static string USER_ID = "";


        // TEST
        public static string TOKEN_TEST = "";
        public static string API_URL_TEST = "";
        private static string ABSOLUTE_URL_TEST = "";


        // DEMO
        public static string TOKEN_DEMO = "";
        public static string API_URL_DEMO = "";
        public static string ABSOLUTE_URL_DEMO = "";


        // PROD
        public static string TOKEN_PROD = "";
        public static string API_URL_PROD = "";
        public static string ABSOLUTE_URL_PROD = "";


        // ASSETBUNDLE HASH
        public static string HASH = "";


        private void Awake()
        {
            if (Application.isEditor)
            {
                ENV = "LOCAL";
                TOKEN = TOKEN_TEST;
                API_URL = API_URL_TEST;
                ABSOLUTE_URL = ABSOLUTE_URL_TEST;
                LANGUAGE = langByEditor;
            }
            else if(Application.absoluteURL.Contains("https://mercato3dt.comune.genova.it/"))
            {
                ENV = "TEST";
                TOKEN = TOKEN_TEST;
                API_URL = API_URL_TEST;
                ABSOLUTE_URL = ABSOLUTE_URL_TEST;
            }
            else
            {
                ENV = "PROD";
                TOKEN = TOKEN_PROD;
                API_URL = API_URL_PROD;
                ABSOLUTE_URL = ABSOLUTE_URL_PROD;
            }
        }

        public static void SetUserID(string id)
        {
            USER_ID = id;
        }
        public static void SetLanguage(string lang)
        {
            LANGUAGE = lang switch
            {
                "Italiano" => "it",
                "English" => "en",
                "Deutsch" => "de",
                "Русский" => "ru",
                "Español" => "es",
                "Français" => "fr",
                _ => LANGUAGE
            };
            Locale.GetLocale().SetLocale();
        }
        public static void SetIntroLanguage(string lang)
        {
            LANGUAGE = lang switch
            {
                "Italiano" => "it",
                "English" => "en",
                "Deutsch" => "de",
                "Русский" => "ru",
                "Español" => "es",
                "Français" => "fr",
                _ => LANGUAGE
            };
            LoadingLocale.GetLoadingLocale().SetLoadingLocale();
        }

        public static void SetLanguageByCode(string lang)
        {
            LANGUAGE = lang;
            Locale.GetLocale().SetLocale();
        }
        public static void SetIntroLanguageByCode(string lang)
        {
            LANGUAGE = lang;
            LoadingLocale.GetLoadingLocale().SetLoadingLocale();
        }

        public static string GetLanguage()
        {
            return LANGUAGE switch
            {
                "it" => "Italiano",
                "en" => "English",
                "de" => "Deutsch",
                "ru" => "Русский",
                "es" => "Español",
                "fr" => "Français",
                _ => "Italiano"
            };
        }
    }
}
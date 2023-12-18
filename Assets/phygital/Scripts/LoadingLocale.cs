using System.Collections;
using System.Collections.Generic;
using phygital.Scripts;
using TMPro;
using UnityEngine;

public class LoadingLocale : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI EARNGENOVINI;
    [SerializeField] private TextMeshProUGUI ETA;
    [SerializeField] private TextMeshProUGUI WAITINGCONNECTION;
    [SerializeField] private TextMeshProUGUI LOADINGBUTTON;
    [SerializeField] private TextMeshProUGUI VERSION;
    
    private static LoadingLocale Instance { get; set; }
    
    private void Awake()
    {
        Instance = this;
    }
    
    public static LoadingLocale GetLoadingLocale()
    {
        return Instance;
    }
    

    public void SetLoadingLocale()
    {
        /*EARNGENOVINI.text = Env.LANGUAGE switch
        {
            "it" => "Guadagna genovini",
            "en" => "Earn genovini",
            "de" => "Gewinnung von genovini",
            "ru" => "Получение Дженовезе",
            "es" => "Ganando genovini",
            "fr" => "Gagner genovini",
            _ => EARNGENOVINI.text
        };*/
        WAITINGCONNECTION.text = Env.LANGUAGE switch
        {
            "it" => "In attesa della connessione...",
            "en" => "Waiting for connection...",
            "de" => "Warten auf Verbindung...",
            "ru" => "Жду подключения...",
            "es" => "Esperando conexión...",
            "fr" => "En attente de connexion...",
            _ => WAITINGCONNECTION.text
        };
        LOADINGBUTTON.text = Env.LANGUAGE switch
        {
            "it" => "Caricamento...",
            "en" => "Loading...",
            "de" => "Laden...",
            "ru" => "Загрузка...",
            "es" => "Cargando...",
            "fr" => "Chargement...",
            _ => LOADINGBUTTON.text
        };
        VERSION.text = Application.version;
    }

    public string SetButtonEnter()
    {
        LOADINGBUTTON.text = Env.LANGUAGE switch
        {
            "it" => "Entra",
            "en" => "Enter",
            "de" => "Eingabe",
            "ru" => "Введите",
            "es" => "Entre en",
            "fr" => "Entrer",
            _ => LOADINGBUTTON.text
        };
        return LOADINGBUTTON.text;
    }
    public string SetETABarReady()
    {
        ETA.text = Env.LANGUAGE switch
        {
            "it" => "Pronto",
            "en" => "Ready",
            "de" => "Fertige",
            "ru" => "Готовая",
            "es" => "Terminado",
            "fr" => "Fini",
            _ => ETA.text
        };
        return ETA.text;
    }
}

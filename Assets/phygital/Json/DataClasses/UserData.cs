using System;
using UnityEngine.Scripting;

namespace phygital.Json.DataClasses
{
    [Serializable]
    [Preserve]
    public class UserData
    {
        public string codice_utente { get; set; }
        public string cognome { get; set; }
        public string email { get; set; }
        public string nome { get; set; }
        public string preferiti { get; set; }
        public string genovini { get; set; }
        public string preferiti_categorie_negozio { get; set; }
        public string lingua { get; set; }
    }

}
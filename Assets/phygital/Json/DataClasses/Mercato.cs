using System;
using UnityEngine.Scripting;

namespace phygital.Json.DataClasses
{
    [Serializable]
    [Preserve]
    public class Mercato
    {
        public string titolo { get; set; }
        public string testo { get; set; }
        public string id { get; set; }
        public string url { get; set; }
        public string immagine_di_copertina { get; set; }
        public string prodotti_tipici { get; set; }
        public string tipo_di_contenuto { get; set; }
        public string autore { get; set; }
        public string categorie_ricetta { get; set; }
        public string visualizzare_in_intro { get; set; }
        public string immagine_insegna { get; set; }
        public string immagine_vetrina { get; set; }
        public string tipologia_di_negozio { get; set; }
        public string id_categoria { get; set; }
        public string negozio_di_appartenenza { get; set; }
        public string lingua { get; set; }
        public string ordinamento_mercato_3d { get; set; }
        public string app { get; set; }
        public string ordinamento_pagine_help { get; set; }
        
    }
}


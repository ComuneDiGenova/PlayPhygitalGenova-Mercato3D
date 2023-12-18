using System;
using System.Collections.Generic;

namespace phygital.Json.DataClasses
{
    [Serializable]
    public class Categoria
    {
        public string id { get; set; }
        public string nome { get; set; }
        public string descrizione { get; set; }
        public string vocabolario { get; set; }
        public string ordinamento { get; set; }
        public string immagine_di_copertina { get; set; }
        public string immagine_vetrina { get; set; }
        public string url { get; set; }
        public string lang { get; set; }
        
        public static Categoria GetCategoriaByName(List<Categoria> categorie, string position)
        {
            return categorie.Find(x => x.ordinamento.Equals(position)) != null ? categorie.Find(x => x.ordinamento == position) : new Categoria();
        }
    }
}
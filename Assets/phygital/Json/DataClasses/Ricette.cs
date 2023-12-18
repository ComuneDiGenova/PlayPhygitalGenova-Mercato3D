using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace phygital.Json.DataClasses
{
    [Serializable]
    [Preserve]
    public class Ricette
    {
        public string id { get; set; }
        public string nome { get; set; }
        public string descrizione { get; set; }
        public string link { get; set; }
        public string immagine { get; set; }
        public List<string> prodotti_tipici { get; set; }
        public string tipo_di_prodotto { get; set; }
        public string categorie_ricette { get; set; }
        public List<string> negozi_di_appartenenza { get; set; }
        public string lingua { get; set; }

        public static Ricette GetRicettaByProdotto(List<Ricette> ricette, string name)
        {
            return ricette.Find(x => x.prodotti_tipici.Find(y => y == name) == name) != null ? ricette.Find(x => x.prodotti_tipici.Find(y => y == name) == name) : new Ricette();
        }
        
        public static Ricette GetRicettaById(List<Ricette> ricette, string id)
        {
            return ricette.Find(x => x.id.Equals(id)) != null ? ricette.Find(x => x.id == id) : new Ricette();
        }
    }
}
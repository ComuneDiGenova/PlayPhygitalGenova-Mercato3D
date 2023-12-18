using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace phygital.Json.DataClasses
{
    [Serializable]
    [Preserve]
    public class Prodotti
    {
        public string id { get; set; }
        public string nome { get; set; }
        public string descrizione { get; set; }
        public string link { get; set; }
        public string immagine { get; set; }
        public List<string> prodotti_tipici { get; set; }
        public string tipo_di_prodotto { get; set; }
        public List<string> ricette { get; set; }
        public List<string> negozi_di_appartenenza { get; set; }
        public string lingua { get; set; }

        public static bool IsProdotto(List<Prodotti> prodotti, string id)
        {
            return prodotti.Exists(x => x.id.Equals(id));
        }
        public static Prodotti GetProdottoById(List<Prodotti> prodotti, string id)
        {
            return prodotti.Find(x => x.id.Equals(id)) != null ? prodotti.Find(x => x.id == id) : new Prodotti();
        }
        public static Prodotti GetProdottoByName(List<Prodotti> prodotti, string name)
        {
            return prodotti.Find(x => x.nome.Equals(name)) != null ? prodotti.Find(x => x.nome == name) : new Prodotti();
        }
    }
}
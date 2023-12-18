using System;
using System.Collections.Generic;

namespace phygital.Json.DataClasses
{
    [Serializable]
    public class Bottega
    {
        public string nome { get; set; }
        public string descrizione { get; set; }
        public string id { get; set; }
        public string link { get; set; }
        public string immagine_di_copertina { get; set; }
        public string immagine_insegna { get; set; }
        public string immagine_vetrina { get; set; }
        public string lingua { get; set; }
        public string ordinamento_mercato_3d { get; set; }
        public string id_categoria { get; set; }

        private Bottega()
        {
            nome = "";
            descrizione = "";
            id = "";
            link = "";
            immagine_di_copertina = "";
            immagine_insegna = "";
            immagine_vetrina = "";
            lingua = "";
            ordinamento_mercato_3d = "";
            id_categoria = "";
        }
        public Bottega(string bNome, string bDescrizione, string bId, string bLink, string bImmagineDiCopertina, string biImmagineInsegna, string bImmagineVetrina, string bLingua, string bOrdinamentoMercato3D, string bIdCategoria)
        {
            nome = bNome;
            descrizione = bDescrizione;
            id = bId;
            link = bLink;
            immagine_di_copertina = bImmagineDiCopertina;
            immagine_insegna = biImmagineInsegna;
            immagine_vetrina = bImmagineVetrina;
            lingua = bLingua;
            ordinamento_mercato_3d = bOrdinamentoMercato3D;
            id_categoria = bIdCategoria;
        }
        
        public static Bottega GetBottegaByName(List<Bottega> botteghe, string position)
        {
            return botteghe.Find(x => x.ordinamento_mercato_3d.Equals(position)) != null ? botteghe.Find(x => x.ordinamento_mercato_3d == position) : new Bottega();
        }
    }
}
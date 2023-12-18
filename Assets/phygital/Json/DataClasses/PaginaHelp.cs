using System;
using System.Collections.Generic;

namespace phygital.Json.DataClasses
{
    [Serializable]
    public class PaginaHelp
    {
        public string titolo { get; set; }
        public string testo { get; set; }
        public string id { get; set; }
        public string immagine_di_copertina { get; set; }
        public string visualizzare_in_intro { get; set; }
        public string lingua { get; set; }
        public string app { get; set; }
        public string ordinamento_pagine_help { get; set; }

        private PaginaHelp()
        {
            titolo = "";
            testo = "";
            id = "";
            immagine_di_copertina = "";
            visualizzare_in_intro = "";
            lingua = "";
            app = "";
            ordinamento_pagine_help = "";
        }

        public PaginaHelp(string pTitolo, string pTesto, string pId, string pImmaginediCopertina, string pVisualizzareInIntro, string pLingua, string pApp, string pOrdinamentoPagineHelp)
        {
            titolo = pTitolo;
            testo = pTesto;
            id = pId;
            immagine_di_copertina = pImmaginediCopertina;
            visualizzare_in_intro = pVisualizzareInIntro;
            lingua = pLingua;
            app = pApp;
            ordinamento_pagine_help = pOrdinamentoPagineHelp;
        }

        public static PaginaHelp GetIntroHelp(List<PaginaHelp> paginaHelps)
        {
            return paginaHelps.Find(x => x.ordinamento_pagine_help.Equals("1")) != null
                ? paginaHelps.Find(x => x.ordinamento_pagine_help == "1")
                : new PaginaHelp();
        }
        public static PaginaHelp GetTitleIntroHelp(List<PaginaHelp> paginaHelps)
        {
            return paginaHelps.Find(x => x.ordinamento_pagine_help.Equals("2")) != null
                ? paginaHelps.Find(x => x.ordinamento_pagine_help == "2")
                : new PaginaHelp();
        }
        
        
        
    }
}
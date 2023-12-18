using System.Collections.Generic;
using System.Linq;
using phygital.Json.DataClasses;
using phygital.Scripts;
using UnityEngine;

namespace phygital.Json
{
    public abstract class Utility
    {
        public static Prodotti RetrieveProdottoByIndex(List<Mercato> mercato, int index)
        {
            return new Prodotti
            {
                id = mercato[index].id,
                nome = RemoveHtmlCode(mercato[index].titolo),
                descrizione = RemoveHtmlCode(mercato[index].testo),
                link = mercato[index].url,
                immagine = mercato[index].immagine_di_copertina,
                prodotti_tipici = ParseList(mercato[index].prodotti_tipici),
                tipo_di_prodotto = mercato[index].tipo_di_contenuto,
                ricette = new List<string>(),
                negozi_di_appartenenza = ParseList(mercato[index].negozio_di_appartenenza),
                lingua = mercato[index].lingua,
            };
        }
        public static Ricette RetrieveRicettaByIndex(List<Mercato> mercato, int index)
        {
            return new Ricette
            {
                id = mercato[index].id,
                nome = RemoveHtmlCode(mercato[index].titolo),
                descrizione = RemoveHtmlCode(mercato[index].testo),
                link = mercato[index].url,
                immagine = mercato[index].immagine_di_copertina,
                prodotti_tipici = ParseList(mercato[index].prodotti_tipici),
                tipo_di_prodotto = mercato[index].tipo_di_contenuto,
                negozi_di_appartenenza = ParseList(mercato[index].negozio_di_appartenenza),
                lingua = mercato[index].lingua,
            };
        }
        public static Bottega RetrieveBottegaByIndex(List<Mercato> mercato, int index)
        {
            return new Bottega(
                RemoveHtmlCode(mercato[index].titolo), 
                RemoveHtmlCode(mercato[index].testo), 
                mercato[index].id, 
                mercato[index].url,
                mercato[index].immagine_di_copertina, 
                mercato[index].immagine_insegna, 
                mercato[index].immagine_vetrina,
                mercato[index].lingua, 
                mercato[index].ordinamento_mercato_3d,
                mercato[index].id_categoria
            );
        }

        public static PaginaHelp RetrievePaginaHelpByIndex(List<Mercato> mercato, int index)
        {
            return new PaginaHelp(
                RemoveHtmlCode(mercato[index].titolo),
                RemoveHtmlCode(mercato[index].testo),
                mercato[index].id,
                mercato[index].immagine_di_copertina,
                mercato[index].visualizzare_in_intro,
                mercato[index].lingua,
                mercato[index].app,
                mercato[index].ordinamento_pagine_help
            );
        }

        public static void InitMap(List<Categoria> categorie, List<Bottega> botteghe)
        {
            Map.GetInstance().SetCategorieAndBotteghe(categorie, botteghe);
        }

        public static bool IsCategoryEmpty(string categoryName)
        {
            switch (categoryName)
            {
                case "Vuoto":
                case "Empty":
                case "Leer":
                case "Пустой":
                case "Nустой":
                case "Vacío":
                case "Vide":
                case "": return true;
            }

            return RetrieveData.GetCategorie().Find(x => x.nome.Equals(categoryName)).id.Equals("426");
        }
        public static bool IsCategoryLabelEmpty(string categoryLabel)
        {
            switch (categoryLabel)
            {
                case "Vuoto":
                case "Empty":
                case "Vakuum":
                case "Вакуум":
                case "Vacío":
                case "Le vide":
                case "": return true;
            }
            return false;
        }
        
        private static List<string> ParseList(string lista)
        {
            return RemoveHtmlCode(lista).Split(",").ToList();
        }

        private static string RemoveHtmlCode(string text)
        {
            var cleanText = text;
            if (text.Contains("&#039;"))
            {
                cleanText = cleanText.Replace("&#039;", "'");
            }
            if (text.Contains("&nbsp;"))
            {
                cleanText = cleanText.Replace("&nbsp;", " ");
            }
            if (text.Contains("&amp;"))
            {
                cleanText = cleanText.Replace("&amp;", "&");
            }
            if (text.Contains("&gt;"))
            {
                cleanText = cleanText.Replace("&gt;", ">");
            }
            return cleanText;
        }
    }
}
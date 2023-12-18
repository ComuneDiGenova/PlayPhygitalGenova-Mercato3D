using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace phygital.Scripts
{
    public class Locale : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI PAUSE;
        [SerializeField] private TextMeshProUGUI RESUME;
        [SerializeField] private TextMeshProUGUI RESUMEHELP;
        [SerializeField] private List<TextMeshProUGUI> SEE_FULL_PAGE;
        [SerializeField] private TextMeshProUGUI RECIPE;
        [SerializeField] private TextMeshProUGUI RELATED_PRODUCTS;
        [SerializeField] private TextMeshProUGUI ADD_TO_FAVOURITES;
        [SerializeField] private TextMeshProUGUI CAMERA;
        [SerializeField] private TextMeshProUGUI MOVEMENT;
        [SerializeField] private TextMeshProUGUI RUN;
        [SerializeField] private TextMeshProUGUI OPEN_INTERACTION;
        [SerializeField] private TextMeshProUGUI CLOSE_INTERACTION;
        [SerializeField] private TextMeshProUGUI MAP;
        [SerializeField] private TextMeshProUGUI HELP;
        [SerializeField] private TextMeshProUGUI PAUSEHELP;
        [SerializeField] private TextMeshProUGUI MAPHEADER;
        [SerializeField] private TextMeshProUGUI MAPCATEGORYHEADER;
        [SerializeField] private TextMeshProUGUI MAPPRODUCTHEADER;
        [SerializeField] private TextMeshProUGUI MAPHINTCATEGORY;
        [SerializeField] private TextMeshProUGUI MAPHINTPRODUCT;
        [SerializeField] private TextMeshProUGUI MAPHINTSHOP;
        [SerializeField] private TextMeshProUGUI FULLSCREEN;

        public static string LOG_IN_TO_USE;
        public static string POINT_MESSAGE;

        private static Locale Instance { get; set; }
        public static Locale GetLocale()
        {
            return Instance;
        }
        

        private void Awake()
        {
            Instance = this;
            SetLocale();
        }

        public void SetLocale()
        {
            foreach (var t in SEE_FULL_PAGE)
            {
                t.text = Env.LANGUAGE switch
                {
                    "it" => "Vai alla scheda completa",
                    "en" => "Go to full tab",
                    "de" => "vollständiges Blatt",
                    "ru" => "полный лист",
                    "es" => "Ir a la ficha completa",
                    "fr" => "Aller à l'onglet complet",
                    _ => t.text
                };
            }
            
            RECIPE.text = Env.LANGUAGE switch
            {
                "it" => "Ricette",
                "en" => "Recipes",
                "de" => "Rezepte",
                "ru" => "Рецепты",
                "es" => "Recetas",
                "fr" => "Recettes",
                _ => RECIPE.text
            };
            RELATED_PRODUCTS.text = Env.LANGUAGE switch
            {
                "it" => "Prodotti",
                "en" => "Products",
                "de" => "Produkte",
                "ru" => "Продукция",
                "es" => "Productos",
                "fr" => "Produits",
                _ => RELATED_PRODUCTS.text
            };
            PAUSE.text = Env.LANGUAGE switch
            {
                "it" => "Pausa",
                "en" => "Pause",
                "de" => "Pause",
                "ru" => "Перерыв",
                "es" => "Pausa",
                "fr" => "Pause",
                _ => PAUSE.text
            };
            RESUME.text = Env.LANGUAGE switch
            {
                "it" => "Riprendi",
                "en" => "Resume",
                "de" => "Fortsetzung",
                "ru" => "Продолжение",
                "es" => "Continúa",
                "fr" => "Reprendre",
                _ => RESUME.text
            };
            LOG_IN_TO_USE = Env.LANGUAGE switch
            {
                "it" => "Accedi per usare questa funzione",
                "en" => "Log in to use this function",
                "de" => "Einloggen und benutzen",
                "ru" => "Войдите в систему, чтобы использовать",
                "es" => "Inicie sesión para utilizar esta función",
                "fr" => "S'identifier pour utiliser cette fonction",
                _ => LOG_IN_TO_USE
            };
            ADD_TO_FAVOURITES.text = Env.LANGUAGE switch
            {
                "it" => "Aggiungi ai preferiti",
                "en" => "Add to favourites",
                "de" => "Zu Favoriten hinzufügen",
                "ru" => "Добавить в избранное",
                "es" => "Añadir a favoritos",
                "fr" => "Ajouter aux favoris",
                _ => ADD_TO_FAVOURITES.text
            };
            POINT_MESSAGE = Env.LANGUAGE switch
            {
                "it" => "Errore accredito punti, riprovare tra 30 secondi",
                "en" => "Points credit error, please try again in 30 seconds",
                "de" => "Fehler beim Punktekonto, bitte versuchen Sie es in 30 Sekunden erneut",
                "ru" => "Ошибка зачисления баллов, повторите попытку через 30 секунд",
                "es" => "Error en el crédito de puntos, inténtelo de nuevo en 30 segundos",
                "fr" => "Erreur de crédit de points, veuillez réessayer dans 30 secondes",
                _ => POINT_MESSAGE
            };
            PAUSEHELP.text = Env.LANGUAGE switch
            {
                "it" => "Pausa",
                "en" => "Pause",
                "de" => "Pause",
                "ru" => "Перерыв",
                "es" => "Pausa",
                "fr" => "Pause",
                _ => PAUSEHELP.text
            };
            RESUMEHELP.text = Env.LANGUAGE switch
            {
                "it" => "Riprendi",
                "en" => "Resume",
                "de" => "Fortsetzung",
                "ru" => "Продолжение",
                "es" => "Continúa",
                "fr" => "Reprendre",
                _ => RESUMEHELP.text
            };
            CAMERA.text = Env.LANGUAGE switch
            {
                "it" => "Camera",
                "en" => "Camera",
                "de" => "Telekamera",
                "ru" => "Телекамера",
                "es" => "Telecámara",
                "fr" => "Caméra",
                _ => CAMERA.text
            };
            MOVEMENT.text = Env.LANGUAGE switch
            {
                "it" => "Movimento",
                "en" => "Movement",
                "de" => "Bewegung",
                "ru" => "Движение",
                "es" => "Movimiento",
                "fr" => "Mouvement",
                _ => MOVEMENT.text
            };
            RUN.text = Env.LANGUAGE switch
            {
                "it" => "Corri",
                "en" => "Run",
                "de" => "Laufen lassen",
                "ru" => "Выполнить",
                "es" => "Ejecutar",
                "fr" => "Exécuter",
                _ => RUN.text
            };
            OPEN_INTERACTION.text = Env.LANGUAGE switch
            {
                "it" => "Apri interazione",
                "en" => "Open interaction",
                "de" => "Offene Interaktion",
                "ru" => "Открытое взаимодействие",
                "es" => "Interacción abierta",
                "fr" => "Interaction ouverte",
                _ => OPEN_INTERACTION.text
            };
            CLOSE_INTERACTION.text = Env.LANGUAGE switch
            {
                "it" => "Chiudi interazione",
                "en" => "Close interaction",
                "de" => "Enge Interaktion",
                "ru" => "Тесное взаимодействие",
                "es" => "Interacción estrecha",
                "fr" => "Interaction étroite",
                _ => CLOSE_INTERACTION.text
            };
            MAP.text = Env.LANGUAGE switch
            {
                "it" => "Mappa",
                "en" => "Maps",
                "de" => "Karte",
                "ru" => "Карта",
                "es" => "Mapa",
                "fr" => "Carte",
                _ => MAP.text
            };
            HELP.text = Env.LANGUAGE switch
            {
                "it" => "Aiuto = H",
                "en" => "Help = H",
                "de" => "Hilfe = H",
                "ru" => "помощь = H",
                "es" => "Ayuda = H",
                "fr" => "Aide = H",
                _ => HELP.text
            };
            MAPHEADER.text = Env.LANGUAGE switch
            {
                "it" => "Mappa del mercato",
                "en" => "Market map",
                "de" => "Marktkarte",
                "ru" => "Карта рынка",
                "es" => "Mapa del mercado",
                "fr" => "Carte du marché",
                _ => MAPHEADER.text
            };
            MAPCATEGORYHEADER.text = Env.LANGUAGE switch
            {
                "it" => "Categorie negozi",
                "en" => "Shop categories",
                "de" => "Kategorien einkaufen",
                "ru" => "Категории магазинов",
                "es" => "Categorías de tiendas",
                "fr" => "Catégories de magasins",
                _ => MAPCATEGORYHEADER.text
            };
            MAPPRODUCTHEADER.text = Env.LANGUAGE switch
            {
                "it" => "Banchi alimentari",
                "en" => "Food banks",
                "de" => "Lebensmittelbanken",
                "ru" => "Банки продовольствия",
                "es" => "Bancos de alimentos",
                "fr" => "Banques alimentaires",
                _ => MAPPRODUCTHEADER.text
            };
            MAPHINTCATEGORY.text = Env.LANGUAGE switch
            {
                "it" => "Categorie negozi",
                "en" => "Shop categories",
                "de" => "Kategorien einkaufen",
                "ru" => "Категории магазинов",
                "es" => "Categorías de tiendas",
                "fr" => "Catégories de magasins",
                _ => MAPHINTCATEGORY.text
            };
            MAPHINTPRODUCT.text = Env.LANGUAGE switch
            {
                "it" => "Prodotti alimentari",
                "en" => "Food products",
                "de" => "Lebensmittel",
                "ru" => "Продукты питания",
                "es" => "Productos alimentarios",
                "fr" => "Produits alimentaires",
                _ => MAPHINTPRODUCT.text
            };
            MAPHINTSHOP.text = Env.LANGUAGE switch
            {
                "it" => "Botteghe storiche",
                "en" => "Historical workshops",
                "de" => "Historische Werkstätten",
                "ru" => "Исторические семинары",
                "es" => "Talleres históricos",
                "fr" => "Ateliers historiques",
                _ => MAPHINTSHOP.text
            };
            FULLSCREEN.text = Env.LANGUAGE switch
            {
                "it" => "Schermo Intero",
                "en" => "Full screen",
                "de" => "Vollbild",
                "ru" => "Полный экран",
                "es" => "Pantalla completa",
                "fr" => "Plein écran",
                _ => FULLSCREEN.text
            };
        }

        public static void SetMapCategoryModalLocale(List<TextMeshProUGUI> b1Group, List<TextMeshProUGUI> b2Group)
        {
            foreach (var b1 in b1Group)
            {
                b1.text = Env.LANGUAGE switch
                {
                    "it" => "Vai",
                    "en" => "Go",
                    "de" => "Weiter",
                    "ru" => "Перейти",
                    "es" => "Vaya a",
                    "fr" => "Aller",
                    _ => b1.text
                };
            }

            foreach (var b2 in b2Group)
            {
                b2.text = Env.LANGUAGE switch
                {
                    "it" => "Scheda",
                    "en" => "Card",
                    "de" => "Broschüre",
                    "ru" => "Фиш",
                    "es" => "Página",
                    "fr" => "Dossier",
                    _ => b2.text
                };
            }
        }
        public static void SetMapShopModalLocale(List<TextMeshProUGUI> b1Group, List<TextMeshProUGUI> b2Group)
        {
            foreach (var b1 in b1Group)
            {
                b1.text = Env.LANGUAGE switch
                {
                    "it" => "Vai",
                    "en" => "Go",
                    "de" => "Weiter",
                    "ru" => "Перейти",
                    "es" => "Vaya a",
                    "fr" => "Aller",
                    _ => b1.text
                };
            }

            foreach (var b2 in b2Group)
            {
                b2.text = Env.LANGUAGE switch
                {
                    "it" => "Preferiti",
                    "en" => "Favourites",
                    "de" => "Favoriten",
                    "ru" => "Избранное",
                    "es" => "Favoritos",
                    "fr" => "Favoris",
                    _ => b2.text
                };
            }
        }
        public static void SetMapProductModalLocale(TextMeshProUGUI label, TextMeshProUGUI b1)
        {
            label.text = label.tag switch
            {
                "icona carota" => Env.LANGUAGE switch
                {
                    "it" => "Frutta e verdura",
                    "en" => "Fruit and vegetables",
                    "de" => "Obst und Gemüse",
                    "ru" => "Фрукты и овощи",
                    "es" => "Frutas y hortalizas",
                    "fr" => "Fruits et légumes",
                    _ => label.text
                },
                "icona formaggio" => Env.LANGUAGE switch
                {
                    "it" => "Salumi e formaggi",
                    "en" => "Cold meats and cheeses",
                    "de" => "Wurst- und Käsesorten",
                    "ru" => "Холодные мясные блюда и сыры",
                    "es" => "Embutidos y quesos",
                    "fr" => "Charcuterie et fromages",
                    _ => label.text
                },
                "icona pesce" => Env.LANGUAGE switch
                {
                    "it" => "Pescheria",
                    "en" => "Fish Market",
                    "de" => "Fischmarkt",
                    "ru" => "Рыбный рынок",
                    "es" => "Pescadería",
                    "fr" => "Marché aux poissons",
                    _ => label.text
                },
                "icona pane" => Env.LANGUAGE switch
                {
                    "it" => "Pane e focaccia",
                    "en" => "Bread and focaccia",
                    "de" => "Brot und Focaccia",
                    "ru" => "Хлеб и фокачча",
                    "es" => "Pan y focaccia",
                    "fr" => "Pain et focaccia",
                    _ => label.text
                },
                "icona dolce" => Env.LANGUAGE switch
                {
                    "it" => "Pasticceria",
                    "en" => "Pastry shop",
                    "de" => "Konditorei",
                    "ru" => "Кондитерский цех",
                    "es" => "Pastelería",
                    "fr" => "Pâtisserie",
                    _ => label.text
                },
                "icona barattolo" => Env.LANGUAGE switch
                {
                    "it" => "Salse e Sughi",
                    "en" => "Sauces",
                    "de" => "Saucen",
                    "ru" => "Соусы",
                    "es" => "Salsas",
                    "fr" => "Sauces",
                    _ => label.text
                },
                "icona carne" => Env.LANGUAGE switch
                {
                    "it" => "Macelleria",
                    "en" => "Butcher Shop",
                    "de" => "Metzgerei",
                    "ru" => "Мясная лавка",
                    "es" => "Carnicería",
                    "fr" => "Boucherie",
                    _ => label.text
                },
                "icona vino" => Env.LANGUAGE switch
                {
                    "it" => "Vini e liquori",
                    "en" => "Wines and spirits",
                    "de" => "Weine und Spirituosen",
                    "ru" => "Вина и спиртные напитки",
                    "es" => "Vinos y licores",
                    "fr" => "Vins et spiritueux",
                    _ => label.text
                },
                "icona preferiti" => Env.LANGUAGE switch
                {
                    "it" => "Prodotti tipici",
                    "en" => "Typical products",
                    "de" => "Typische Produkte",
                    "ru" => "Типовые изделия",
                    "es" => "Productos típicos",
                    "fr" => "Produits typiques",
                    _ => label.text
                },
                _ => label.text
            };
            b1.text = Env.LANGUAGE switch
            {
                "it" => "Vai",
                "en" => "Go",
                "de" => "Weiter",
                "ru" => "Перейти",
                "es" => "Vaya a",
                "fr" => "Aller",
                _ => b1.text
            };
        }
        
    }
}
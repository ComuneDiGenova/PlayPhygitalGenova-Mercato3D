using System;
using System.Collections.Generic;
using phygital.Json;
using UnityEngine;

namespace phygital.Scripts
{
    public class UnusedShop : MonoBehaviour
    {
        private static UnusedShop Instance { get; set; }
        private List<GameObject> _unusedShops;
        [SerializeField] private List<GameObject> _unusedMarker;
        [SerializeField] public List<GameObject> _unusedCategories;

        private void Awake()
        {
            Instance = this;
        }

        public List<GameObject> GetUnusedShops()
        {
            return Instance._unusedShops;
        }

        public static void DisableCategoryMarker(int index)
        {
            Instance._unusedCategories[index].SetActive(false);
        }
        
        public static void SetUnusedShops(List<GameObject> shops)
        {
            Instance._unusedShops = shops;
        }
        public static void RemoveUsedShops(GameObject shop)
        {
            Instance._unusedShops.Remove(shop);
        }

        public static void DisableUnused()
        {
            for (var i = 0; i < Instance._unusedMarker.Count; i++)
            {
                //var uShop = Instance._unusedShops.Find(x => x.tag.Equals(i.ToString()));
                //if (!uShop) return;
                //uShop.layer = 0;
                //Instance._unusedMarker[i].SetActive(false);
                var t = RetrieveData.GetBotteghe().Find(x => x.ordinamento_mercato_3d.Equals(Instance._unusedMarker[i].tag));
                if (t is null)
                {
                    Instance._unusedMarker[i].SetActive(false);
                    TextureVetrine.GetTextureVetrineInstance().GetShops().Find(x => x.tag.Equals(Instance._unusedMarker[i].tag)).layer = (int)LayerIndexes.Pareti;
                }
            }
        }
    }
}
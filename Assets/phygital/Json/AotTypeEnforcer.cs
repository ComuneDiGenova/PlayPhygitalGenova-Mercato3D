using System.Collections.Generic;
using Newtonsoft.Json.Utilities;
using phygital.Json.DataClasses;
using UnityEngine;

namespace phygital.Json
{
    public class AotTypeEnforcer : MonoBehaviour
    {
        public void Awake()
        {
            AotHelper.EnsureList<int>();
            AotHelper.EnsureList<string>();
            AotHelper.EnsureList<List<Mercato>>();
        }
    }
}

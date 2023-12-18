using System;
using phygital.Scripts;
using UnityEngine.Scripting;

namespace phygital.Json.DataClasses
{
    [Serializable]
    [Preserve]
    public class Point
    {
        public string result { get; set; }
        public string message { get; set; }
        public string points { get; set; }

        public Point()
        {
            result = "false";
            message = Locale.LOG_IN_TO_USE;
            points = "";
        }
    }
}
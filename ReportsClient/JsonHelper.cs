using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;


namespace ReportsClient
{
    public static class JsonHelper
    {
        public static IList<T> DeserializeToList<T>(string jsonString)
        {

            var array = JArray.Parse(jsonString);
            IList<T> objectsList = new List<T>();

            foreach (var item in array)
            {
                try
                {
                    objectsList.Add(item.ToObject<T>());
                }
                catch (Exception e)
                {
                    //do Something
                }
            }
            return objectsList;
        }
 

    }
}

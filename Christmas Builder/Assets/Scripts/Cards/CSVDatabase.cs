using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Cards
{
    [Serializable]
    public class CSVDatabase
    {
        public CSVDatabase()
        {
            Database = new Dictionary<string, CSVObject>();
        }

        [field: SerializeField] public Dictionary<string, CSVObject> Database { get; set; }

        public CSVObject GetObj(string path)
        {
            if (Database == null)
                return null;
            CSVObject obj;
            Database.TryGetValue(path, out obj);
            if (obj == null)
                Debug.LogError("CSVDATABASE: object not found by the path :" + path);
            return obj;
        }

        public object GetValue(string path, string name)
        {
            CSVObject obj = GetObj(path);
            if (obj == null)
                return null;
            return obj.GetValue(name);
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CSVManager
{
    [Serializable]
    public class CSVObject
    {
        public CSVObject(string _name, string _path, string _parent)
        {
            name = _name;
            path = _path;
            parent = _parent;
        }

        public string name;
        public string path;
        public string parent;
        public Dictionary<string, object> name_value = new Dictionary<string, object>();

        public object GetValue(string field)
        {
            if (field.Equals("name"))
                return name;
            object val;
            name_value.TryGetValue(field, out val);
            if (val == null)
                Debug.LogError("CSVObject: such field was not found by the name:" + field);
            return val;
        }

        public string GetString(string field)
        {
            return (string)GetValue(field);
        }

        public int GetInt(string field)
        {
            return (int)GetValue(field);
        }

        public float GetFloat(string field)
        {
            return (float)GetValue(field);
        }

        public bool GetBool(string field)
        {
            return (bool)GetValue(field);
        }

        public event Action OnDataUpdate = delegate { };
    }
}
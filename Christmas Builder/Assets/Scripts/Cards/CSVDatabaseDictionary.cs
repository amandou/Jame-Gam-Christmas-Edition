using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Cards
{
    [Serializable]
    public class CSVDatabaseDictionary
    {
        public CSVDatabaseDictionary()
        {
            Database = new Dictionary<string, CSVDatabase>();
        }

        [field: SerializeField] public Dictionary<string, CSVDatabase> Database { get; set; }
    }
}
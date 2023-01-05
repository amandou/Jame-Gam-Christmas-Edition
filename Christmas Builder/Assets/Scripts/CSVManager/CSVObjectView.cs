using System.Collections.Generic;

namespace Assets.Scripts.CSVManager
{
    [System.Serializable]
    public class CSVObjectView
    {
        public string Name, path, parent;
        public List<CSVPairView> Values = new List<CSVPairView>();

        [System.Serializable]
        public class CSVPairView
        {
            public string Name;
            public string Value;

            public CSVPairView(string name, string value)
            {
                Name = name;
                Value = value;
            }
        }

        public CSVObjectView(CSVObject obj)
        {
            Name = obj.name;
            path = obj.path;
            parent = obj.parent;

            foreach (var p in obj.name_value)
            {
                Values.Add(new CSVPairView(p.Key, p.Value.ToString()));
            }
        }
    }
}
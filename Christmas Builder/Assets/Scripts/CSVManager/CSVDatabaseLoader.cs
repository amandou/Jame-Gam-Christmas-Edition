using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.Scripts.Cards
{
    public class CSVDatabaseLoader : MonoBehaviour
    {
        private string rootPath;
        [field: SerializeField] private CSVDatabaseDictionary databaseDictionary;
        public List<CSVObjectView> DatabaseObjects { get; set; }

        private void Awake()
        {
#if UNITY_EDITOR
            rootPath = Application.dataPath + "/Data/CSV/";
            databaseDictionary = new CSVDatabaseDictionary();
            Initialize();

            foreach (var csvName in databaseDictionary.Database.Keys)
            {
                Debug.Log($"Key Name: {csvName}");
                switch (csvName)
                {
                    case "cards":
                        foreach (var cardData in databaseDictionary.Database[csvName].Database)
                        {
                            var card = ScriptableObject.CreateInstance<CardDataSO>();
                            card.SaveCardAsSO(cardData.Value);
                        }
                        break;

                    case "letters":
                        foreach (var letterData in databaseDictionary.Database[csvName].Database)
                        {
                            var letter = ScriptableObject.CreateInstance<LetterDataSO>();
                            letter.SaveLetterAsSO(letterData.Value);
                        }
                        break;
                }
            }
#endif
        }

        public void Initialize()
        {
            List<CSVObject> toparent = new List<CSVObject>();
            //first pass - simply parse all the files into objects
            //get file list
            string[] files = Directory.GetFiles(rootPath, "*.csv");
            foreach (var f in files)
            {
                var database = new CSVDatabase();
                var fs = new FileStream(f, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using (StreamReader sr = new StreamReader(fs))
                {
                    int linecount = -1;
                    int tableWidth = 0;
                    string tablePath = "";
                    string[] fieldnames = null;
                    string[] fieldtypes = null;
                    string[] fieldnamesplit;

                    while (!sr.EndOfStream)
                    {
                        string[] line = SplitCSV(sr.ReadLine());
                        linecount++;
                        if (linecount == 0)
                        {
                            //first line each csv file has its own "path root"
                            tablePath = line[0];
                            tableWidth = line.Length;
                            fieldnames = new string[tableWidth];
                            fieldtypes = new string[tableWidth];
                            continue;
                        }
                        else
                        if (linecount == 1)
                        {
                            //second line defines field names and field types
                            for (int i = 0; i < tableWidth; i++)
                            {
                                //first part is name, second is type
                                fieldnamesplit = line[i].Split(':');
                                fieldnames[i] = fieldnamesplit[0];
                                if (fieldnamesplit.Length > 1)
                                    fieldtypes[i] = fieldnamesplit[1];
                            }
                            continue;
                        }
                        if (string.IsNullOrEmpty(line[0]))
                        {
                            continue;
                        }
                        string objectDbName = line[0];
                        string parent = "";
                        if (!string.IsNullOrEmpty(line[1]))
                            parent = line[1].Contains("/") ? line[1] : tablePath + line[1];

                        CSVObject csvobj = new CSVObject(objectDbName, tablePath + objectDbName, parent);

                        //TODO add cutom column support

                        if (!string.IsNullOrEmpty(parent))
                        {
                            toparent.Add(csvobj);
                        }
                        //parse fields
                        for (int i = 3; i < line.Length; i++)
                        {
                            csvobj.name_value.Add(fieldnames[i], ParseTo(line[i], fieldtypes[i], csvobj));
                        }
                        database.Database.Add(csvobj.name, csvobj);
                    }
                }
                databaseDictionary.Database.Add(Path.GetFileNameWithoutExtension(f), database);
            }
        }

        private static string[] SplitCSV(string input)
        {
            swaplist.Clear();
            string curr = null;

            foreach (Match match in csvSplit.Matches(input))
            {
                curr = match.Value;
                if (0 == curr.Length)
                {
                    swaplist.Add("");
                }

                swaplist.Add(curr.TrimStart(','));
            }

            return swaplist.ToArray();
        }

        private static object ParseTo(string value, string type, CSVObject obj)
        {
            switch (value)
            {
                case "$name":
                    return obj.name;

                case "$path":
                    return obj.path;

                default:
                    break;
            }

            object result = null;

            switch (type)
            {
                case "int":
                    {
                        int val;
                        if (int.TryParse(value, out val))
                            result = val;
                        else
                            result = 0;
                    }
                    break;

                case "float":
                    {
                        float val;
                        if (float.TryParse(value, out val))
                            result = Convert.ToSingle(value);
                        else
                            result = 0f;
                    }
                    break;

                case "bool":
                    {
                        bool val;
                        if (bool.TryParse(value, out val))
                            result = Convert.ToBoolean(value);
                        else
                            result = false;
                    }
                    break;

                default:
                    result = value.Length > 0 ? value : "";
                    break;
            }
            return result == null ? value : result;
        }

        private static List<string> swaplist = new List<string>();
        private static Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.None);
    }
}
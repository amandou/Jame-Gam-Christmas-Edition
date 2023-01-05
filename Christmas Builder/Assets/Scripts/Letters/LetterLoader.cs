using Assets.Scripts.CSVManager;
using System.Collections.Generic;

namespace Assets.Scripts.Letters
{
    internal class LetterLoader
    {
        private Dictionary<string, CSVObject> database;

        public LetterLoader(Dictionary<string, CSVObject> database)
        {
            this.database = database;
        }
    }
}
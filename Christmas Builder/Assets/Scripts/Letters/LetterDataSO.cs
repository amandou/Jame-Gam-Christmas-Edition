using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Cards
{
    [CreateAssetMenu(fileName = "LetterData", menuName = "Letters/Data")]
    public class LetterDataSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Intro { get; private set; }
        [field: SerializeField] public string Deeds { get; private set; }
        [field: SerializeField] public string Wish { get; private set; }

        public void Init(string name, string intro, string deeds, string wish)
        {
            Name = name;
            Intro = intro;
            Deeds = deeds;
            Wish = wish;
        }

        public void SaveLetterAsSO(CSVObject letterData)
        {
            Name = letterData.GetValue("name") as string;
            Intro = letterData.GetValue("intro") as string;
            Deeds = letterData.GetValue("deeds") as string;
            Wish = letterData.GetValue("wish") as string;
            Init(Name, Intro, Deeds, Wish);
#if UNITY_EDITOR
            AssetDatabase.CreateAsset(this, "Assets/ScriptableObjectData/Letters/" + Name + ".asset");
#endif
        }
    }
}
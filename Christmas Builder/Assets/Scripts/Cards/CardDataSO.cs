using Assets.Scripts.CSVManager;
using MyBox;
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Cards
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Cards/Data")]
    [Serializable]
    public class CardDataSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; set; }
        [field: SerializeField] public int VideoGameWeight { get; private set; }
        [field: SerializeField] public int SportsWeight { get; private set; }
        [field: SerializeField] public int ToyWeight { get; private set; }
        [field: SerializeField] public Sprite Visual { get; private set; }
        [field: SerializeField] public Color Shade { get; private set; }
        [field: SerializeField] public Color MainColor { get; private set; }
        [field: SerializeField] public Color Light { get; private set; }
        [field: SerializeField] public CardColorsSO ColorScheme { get; private set; }

        public void Init(string name, int videoGameWeight, int sportsWeight, int toyWeight)
        {
            Name = name;
            VideoGameWeight = videoGameWeight;
            SportsWeight = sportsWeight;
            ToyWeight = toyWeight;
            SetColorsByHighestWeight();
        }

        [ButtonMethod]
        private void SetColorsByHighestWeight()
        {
            if (VideoGameWeight > SportsWeight)
            {
                if (VideoGameWeight > ToyWeight)
                {
                    SetVideoGameColorScheme();
                }
                else
                {
                    SetToyColorScheme();
                }
            }
            else
            {
                if (SportsWeight > ToyWeight)
                {
                    SetSportsColorScheme();
                }
                else
                {
                    SetToyColorScheme();
                }
            }
        }

        private void SetSportsColorScheme()
        {
            Light = ColorScheme.SportsLight;
            MainColor = ColorScheme.Sports;
            Shade = ColorScheme.SportsShade;
        }

        private void SetVideoGameColorScheme()
        {
            Light = ColorScheme.VideoGameLight;
            MainColor = ColorScheme.VideoGame;
            Shade = ColorScheme.VideoGameShade;
        }

        private void SetToyColorScheme()
        {
            Light = ColorScheme.ToyLight;
            MainColor = ColorScheme.Toy;
            Shade = ColorScheme.ToyShade;
        }

#if UNITY_EDITOR

        public void SaveCardAsSO(CSVObject cardData)
        {
            var name = cardData.GetValue("name") as string;
            var videogame = cardData.GetInt("videogame");
            var sport = cardData.GetInt("sport");
            var toy = cardData.GetInt("toy");
            Init(name, sport, videogame, toy);
            AssetDatabase.CreateAsset(this, "Assets/ScriptableObjectData/Cards/" + Name + ".asset");
        }

#endif
    }
}
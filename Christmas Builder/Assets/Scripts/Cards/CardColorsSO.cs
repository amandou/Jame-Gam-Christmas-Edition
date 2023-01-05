using System;
using UnityEngine;

namespace Assets.Scripts.Cards
{
    [CreateAssetMenu(fileName = "CardColorScheme", menuName = "Cards/Colors")]
    [Serializable]
    public class CardColorsSO : ScriptableObject
    {
        [field: SerializeField] public Color VideoGameShade { get; set; }
        [field: SerializeField] public Color VideoGame { get; set; }
        [field: SerializeField] public Color VideoGameLight { get; set; }
        [field: SerializeField] public Color ToyShade { get; set; }
        [field: SerializeField] public Color Toy { get; set; }
        [field: SerializeField] public Color ToyLight { get; set; }
        [field: SerializeField] public Color SportsShade { get; set; }
        [field: SerializeField] public Color Sports { get; set; }
        [field: SerializeField] public Color SportsLight { get; set; }
    }
}
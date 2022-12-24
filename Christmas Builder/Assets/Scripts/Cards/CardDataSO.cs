using Assets.Scripts.Cards;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class CardDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; set; }
    [field: SerializeField] public int VideoGameWeight { get; private set; }
    [field: SerializeField] public int SportsWeight { get; private set; }
    [field: SerializeField] public int ToyWeight { get; private set; }
    [field: SerializeField] public Sprite Visual { get; private set; }

    public void Init(string name, int videoGameWeight, int sportsWeight, int toyWeight)
    {
        Name = name;
        //TODO carregar das tabelas de localização
        VideoGameWeight = videoGameWeight;
        SportsWeight = sportsWeight;
        ToyWeight = toyWeight;
    }

    public void SaveCardAsSO(CSVObject cardData)
    {
        var name = cardData.GetValue("name") as string;
        var videogame = cardData.GetInt("videogame");
        var sport = cardData.GetInt("sport");
        var toy = cardData.GetInt("toy");
        Init(name, sport, videogame, toy);
        AssetDatabase.CreateAsset(this, "Assets/ScriptableObjectData/Cards/" + Name + ".asset");
    }
}
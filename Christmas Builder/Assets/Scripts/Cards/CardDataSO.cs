using Assets.Scripts.Cards;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class CardDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public int TechnologyWeight { get; private set; }
    [field: SerializeField] public int SportsWeight { get; private set; }
    [field: SerializeField] public int ClothingWeight { get; private set; }
    [field: SerializeField] public int FunWeight { get; private set; }
    [field: SerializeField] public int CreativityWeight { get; private set; }
    [field: SerializeField] public int FoodWeight { get; private set; }

    public void Init(string name, string description, int cost, int technologyWeight, int sportsWeight, int clothingWeight, int funWeight, int creativityWeight, int foodWeight)
    {
        Name = name;
        Description = description;
        Cost = cost;
        TechnologyWeight = technologyWeight;
        SportsWeight = sportsWeight;
        ClothingWeight = clothingWeight;
        FunWeight = funWeight;
        CreativityWeight = creativityWeight;
        FoodWeight = foodWeight;
    }

    public void SaveCardAsSO(CSVObject cardData)
    {
        var name = cardData.GetValue("name") as string;
        var description = cardData.GetValue("description") as string;
        var technology = cardData.GetInt("technology");
        var sport = cardData.GetInt("sport");
        var clothing = cardData.GetInt("clothing");
        var fun = cardData.GetInt("fun");
        var creativity = cardData.GetInt("creativity");
        var food = cardData.GetInt("food");
        var cost = cardData.GetInt("cost");
        Init(name, description, cost, technology, sport, clothing, fun, creativity, food);
        AssetDatabase.CreateAsset(this, "Assets/ScriptableObjectData/Cards/" + Name + ".asset");
    }
}
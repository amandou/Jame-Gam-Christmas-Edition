using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;
    public TextMeshProUGUI deckSizeText;


    public void Start()
    {
        DrawCardsOnTheTable();
    }

    public void DrawCard()
    {
        if (deck.Count >= 1)
        {
            Card card = deck[Random.Range(0, deck.Count)];
            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i])
                {
                    ShowCard(card, deck, i);
                    return;
                }
            }
        }
    }

    public void DrawCardsOnTheTable()
    {
        if (deck.Count >= 1)
        {
            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i])
                {
                    Card card = deck[Random.Range(0, deck.Count)];
                    ShowCard(card, deck, i);
                }
            }
        }
    }

    private void Update()
    {
        deckSizeText.text = deck.Count.ToString();
    }

    private void ShowCard(Card card, List<Card> deck, int index)
    {
        card.gameObject.SetActive(true);
        Vector3 cardPosition = cardSlots[index].position;
        card.transform.position = cardPosition;
        availableCardSlots[index] = false;
        deck.Remove(card);
    }
}

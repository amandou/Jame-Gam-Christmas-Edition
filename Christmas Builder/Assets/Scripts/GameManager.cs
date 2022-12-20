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

    public void DrawCard ()
    { 
        if (deck.Count >= 1)
        {
            Card card = deck[Random.Range(0,deck.Count)];
            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i])
                {
                    card.gameObject.SetActive(true);
                    Vector3 cardPosition = cardSlots[i].position;
                    card.transform.position = cardPosition;
                    availableCardSlots[i] = false;
                    deck.Remove(card);
                    return;
                }
            }
        }
    }

    private void Update()
    {
        deckSizeText.text = deck.Count.ToString();
    }
}

using System;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyBox;
using DG.Tweening.Core.Easing;
using Assets.Scripts.Letters;
using Assets.Scripts.Localization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Transform[] cardSlots;
    public TextMeshProUGUI deckSizeText;
    public Button confirmButton;
    public GameObject finalInformation;

    public static event Action hideInfo;
    public static event Action showInfo;

    private const int DECK_SIZE = 10;
    private const int HAND_SIZE = 3;
    private const int LETTER_AMOUNT = 3;
    private int letterIndex;

    public int selectedCard;

    [field: SerializeField] public Card cardPrefab { get; set; }
    [field: SerializeField] public GameObject canvas { get; set; }

    [field: SerializeField] public List<Card> Deck { get; set; }
    [field: SerializeField] public Card[] Hand { get; set; }
    [field: SerializeField] public List<CardDataSO> AvailableCards { get; set; }
    [field: SerializeField] public LetterGenerator Letter { get; set; }

    private void Awake()
    {
        letterIndex = 0;
        BuildDeck();
        Hand = new Card[HAND_SIZE];
    }

    private void BuildDeck()
    {
        Deck = new();
        for (var i = 0; i < DECK_SIZE; ++i)
        {
            var cardSO = AvailableCards.GetRandom();
            var card = Instantiate(cardPrefab, canvas.transform);
            card.Init(cardSO);
            Deck.Add(card);
        }
    }

    public void Start()
    {
        DrawCardsOnTheTable();
        CreateLetter();
    }

    private void CreateLetter()
    {
        if (letterIndex < LETTER_AMOUNT)
        {
            //TODO criar criança aleatória e fazer carta receber os dados e escrever nome.
            Letter.CreateLetter();
            letterIndex++;
        }
        else
        {
            //GAME OVER
            Debug.Log("This is a end game");
            hideInfo?.Invoke();
            finalInformation.SetActive(true);
        }
    }

    public void DrawCard()
    {
        if (Deck.Count >= 1)
        {
            Card card = Deck[Random.Range(0, Deck.Count)];
            for (int i = 0; i < Hand.Length; i++)
            {
                if (Hand[i] == null)
                {
                    ShowCard(card, i);
                    return;
                }
            }
        }
    }

    public void DrawCardsOnTheTable()
    {
        if (Deck.Count >= 1)
        {
            for (int i = 0; i < Hand.Length; i++)
            {
                if (Hand[i] == null)
                {
                    Card card = Deck[Random.Range(0, Deck.Count)];
                    ShowCard(card, i);
                }
            }
        }
    }

    private void Update()
    {
        deckSizeText.text = Deck.Count.ToString();
    }

    private void ShowCard(Card card, int index)
    {
        card.gameObject.SetActive(true);
        Vector3 cardPosition = cardSlots[index].position;
        card.transform.position = cardPosition;
        card.handIndex = index;
        Deck.Remove(card);
        Hand[index] = card;
    }

    public void setSelectedCard(int handIndex)
    {
        confirmButton.interactable = true;
        selectedCard = handIndex;
        for (var i = 0; i < Hand.Length; ++i)
        {
            if (i != handIndex)
            {
                if (Hand[i] != null)
                {
                    Hand[i].DisableUnselected();
                }
            }
        }
    }

    public int getSelectedCard()
    {
        return selectedCard;
    }

    private void OnEnable()
    {
        Card.selectCard += setSelectedCard;
        ConfirmButton.useCard += UseSelectedCard;
    }

    private void UseSelectedCard()
    {
        var selectedCardIndex = getSelectedCard();
        var usedCard = Hand[selectedCardIndex];
        Destroy(usedCard.gameObject);
        Hand[selectedCardIndex] = null;
        CreateLetter();
    }

    private void OnDisable()
    {
        Card.selectCard -= setSelectedCard;
        ConfirmButton.useCard -= UseSelectedCard;
    }
}
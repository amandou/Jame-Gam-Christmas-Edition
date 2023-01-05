using Assets.Scripts.Cards;
using Assets.Scripts.Letters;
using MyBox;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField] public Transform[] CardSlots { get; private set; }
        [field: SerializeField] public TextMeshProUGUI DeckSizeText { get; private set; }
        [field: SerializeField] public Card CardPrefab { get; private set; }
        [field: SerializeField] public GameObject GameCanvas { get; private set; }
        [field: SerializeField] public List<Card> Deck { get; private set; }
        [field: SerializeField] public Card[] Hand { get; private set; }
        [field: SerializeField] public List<CardDataSO> AvailableCards { get; private set; }

        public static event Action ShowInfoHandler;

        public static event Action<float> UpdateScoreHandler;

        private const int DECK_SIZE = 10;
        private const int HAND_SIZE = 3;

        private LetterController _letterController;
        private int _selectedCard;

        public enum GiftType
        {
            Toy,
            Videogame,
            Sports
        }

        private void Awake()
        {
            BuildDeck();
            Hand = new Card[HAND_SIZE];
        }

        private void Start()
        {
            DrawCardsOnTheTable();
            _letterController = GetComponent<LetterController>();
            _letterController.CreateLetter();
        }

        private void OnEnable()
        {
            Card.CardIsSelectedEventHandler += SetSelectedCard;
            ConfirmButtonController.UseCardHandler += UseSelectedCard;
        }

        private void OnDisable()
        {
            Card.CardIsSelectedEventHandler -= SetSelectedCard;
            ConfirmButtonController.UseCardHandler -= UseSelectedCard;
        }

        private void BuildDeck()
        {
            Deck = new();
            for (var i = 0; i < DECK_SIZE; ++i)
            {
                var cardSO = AvailableCards.GetRandom();
                var card = Instantiate(CardPrefab, GameCanvas.transform);
                card.Init(cardSO);
                Deck.Add(card);
            }
        }

        public void DrawCard()
        {
            if (Deck.Count < 1) return;

            Card card = Deck[Random.Range(0, Deck.Count)];
            for (int i = 0; i < Hand.Length; i++)
            {
                if (Hand[i] != null) continue;

                ShowCard(card, i);
                return;
            }
        }

        private void DrawCardsOnTheTable()
        {
            if (Deck.Count < 1) return;

            for (int i = 0; i < Hand.Length; i++)
            {
                if (Hand[i] != null) continue;

                Card card = Deck[Random.Range(0, Deck.Count)];
                ShowCard(card, i);
            }
        }

        private void Update()
        {
            DeckSizeText.text = Deck.Count.ToString();
        }

        private void ShowCard(Card card, int index)
        {
            card.gameObject.SetActive(true);
            Vector3 cardPosition = CardSlots[index].position;
            card.transform.position = cardPosition;
            card.HandIndex = index;
            Deck.Remove(card);
            Hand[index] = card;
        }

        public void SetSelectedCard(int handIndex)
        {
            _selectedCard = handIndex;
            for (var i = 0; i < Hand.Length; ++i)
            {
                if (i == handIndex) continue;
                if (Hand[i] == null) continue;

                Hand[i].DisableUnselected();
            }
        }

        public int GetSelectedCard()
        {
            return _selectedCard;
        }

        private void UseSelectedCard()
        {
            var selectedCardIndex = GetSelectedCard();
            var usedCard = Hand[selectedCardIndex];
            UpdateSantaClausScore(usedCard);
            SetSelectedCard(-1);
            Destroy(usedCard.gameObject);
            Hand[selectedCardIndex] = null;
            _letterController.CreateLetter();
        }

        private void UpdateSantaClausScore(Card usedCard)
        {
            var giftValue = GetGiftRewardFromWantedType(usedCard, _letterController.WantedType);
            float scoreChange = _letterController.ChildAlignment * giftValue;
            UpdateScoreHandler?.Invoke(scoreChange);
        }

        private int GetGiftRewardFromWantedType(Card usedCard, GiftType wantedType)
        {
            switch (wantedType)
            {
                case GiftType.Toy:
                    return usedCard.ToyWeight;

                case GiftType.Videogame:
                    return usedCard.VideoGameWeight;

                case GiftType.Sports:
                    return usedCard.SportsWeight;

                default:
                    Debug.LogError("Wanted gift does not exist");
                    return 0;
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Assets.Scripts.Cards
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [field: SerializeField] public int HandIndex { get; set; }
        [field: SerializeField] public string Description { get; set; }
        [field: SerializeField] public int VideoGameWeight { get; private set; }
        [field: SerializeField] public int SportsWeight { get; private set; }
        [field: SerializeField] public int ToyWeight { get; private set; }
        [field: SerializeField] public TextMeshProUGUI VideoGameWeightText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI SportsWeightText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI ToyWeightText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI DescriptionText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI NameText { get; private set; }
        [field: SerializeField] public Image UIImage { get; private set; }
        [field: SerializeField] public Image ProtraitBackground { get; private set; }
        [field: SerializeField] public Image CardBackground { get; private set; }
        [field: SerializeField] public List<CardDataSO> Cards { get; private set; }

        public static event Action<int> CardIsSelectedEventHandler;

        private const float OFFSET_POSITION_WHEN_OUTLINED = 5f;

        private RectTransform _rectTransform;
        private Outline _outline;
        private string cardName;
        private bool _canBeSelected;
        private string _tableName;
        private Sprite _visual;
        private Color _backgroundColor;
        private Color _portraitColor;
        private StringTable _cardsTable;

        public void Init(CardDataSO cardSO)
        {
            _tableName = cardSO.name;
            cardName = cardSO.Name;
            Description = cardSO.Description;
            VideoGameWeight = cardSO.VideoGameWeight;
            SportsWeight = cardSO.SportsWeight;
            ToyWeight = cardSO.ToyWeight;
            _visual = cardSO.Visual;
            _portraitColor = cardSO.Light;
            _backgroundColor = cardSO.Shade;
        }

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _outline = GetComponent<Outline>();
            VideoGameWeightText.text = VideoGameWeight.ToString();
            SportsWeightText.text = SportsWeight.ToString();
            ToyWeightText.text = ToyWeight.ToString();
            NameText.text = cardName;
            UIImage.sprite = _visual;
            CardBackground.color = _backgroundColor;
            ProtraitBackground.color = _portraitColor;
        }

        private void OnEnable()
        {
            LocalizationSettings.SelectedLocaleChanged += LoadCard;
            StartCoroutine(LoadTables());
        }

        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= LoadCard;
        }

        private void LoadCard(Locale locale)
        {
            StartCoroutine(LoadTables());
        }

        private IEnumerator LoadTables()
        {
            yield return StartCoroutine(LoadTable());
            Description = _cardsTable[_tableName].GetLocalizedString();
            DescriptionText.text = Description;
        }

        private IEnumerator LoadTable()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync("Cards");
            yield return loadingOperation;
            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                _cardsTable = loadingOperation.Result;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_canBeSelected)
            {
                _outline.enabled = true;
                CardIsSelectedEventHandler?.Invoke(HandIndex);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _rectTransform.localPosition += Vector3.up * OFFSET_POSITION_WHEN_OUTLINED;
            _canBeSelected = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _rectTransform.localPosition += Vector3.up * -OFFSET_POSITION_WHEN_OUTLINED;
            _canBeSelected = false;
        }

        internal void DisableUnselected()
        {
            if (!_canBeSelected)
            {
                _outline.enabled = false;
            }
        }
    }
}
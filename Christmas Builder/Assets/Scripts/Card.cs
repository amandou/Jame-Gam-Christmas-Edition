using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public int id;
    public string cardName;
    public int multiplier;
    public float value;
    public bool canBeSelected;
    public bool isSelected;
    public int handIndex;
    public string tableName;
    public Transform cards;
    private Sprite visual;
    private Color backgroundColor;
    private Color portraitColor;

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

    public static event Action<int> selectCard;

    private RectTransform rectTransform;
    private Outline outline;
    private GameManager gameManager;

    public void Init(CardDataSO cardSO)
    {
        tableName = cardSO.name;
        cardName = cardSO.Name;
        Description = cardSO.Description;
        VideoGameWeight = cardSO.VideoGameWeight;
        SportsWeight = cardSO.SportsWeight;
        ToyWeight = cardSO.ToyWeight;
        visual = cardSO.Visual;
        portraitColor = cardSO.Light;
        backgroundColor = cardSO.Shade;
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        outline = GetComponent<Outline>();
        gameManager = FindObjectOfType<GameManager>();
        VideoGameWeightText.text = VideoGameWeight.ToString();
        SportsWeightText.text = SportsWeight.ToString();
        ToyWeightText.text = ToyWeight.ToString();
        NameText.text = cardName;
        UIImage.sprite = visual;
        CardBackground.color = backgroundColor;
        ProtraitBackground.color = portraitColor;
    }

    private StringTable _cardsTable;
    [field: SerializeField] public List<CardDataSO> Cards { get; private set; }

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
        Description = _cardsTable[tableName].GetLocalizedString();
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
        if (canBeSelected)
        {
            outline.enabled = true;
            selectCard?.Invoke(handIndex);
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        rectTransform.localPosition += Vector3.up * 5;
        canBeSelected = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        rectTransform.localPosition += Vector3.up * -5;
        canBeSelected = false;
    }

    internal void DisableUnselected()
    {
        if (!canBeSelected)
        {
            outline.enabled = false;
        }
    }
}
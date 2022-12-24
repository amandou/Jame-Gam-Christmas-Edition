using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public int id;
    public string cardName;
    public int multiplier;
    public float value;
    public bool canBeSelected;
    public bool isSelected;
    public int handIndex;
    public Transform cards;

    [field: SerializeField] public string Description { get; set; }
    [field: SerializeField] public int VideoGameWeight { get; private set; }
    [field: SerializeField] public int SportsWeight { get; private set; }
    [field: SerializeField] public int ToyWeight { get; private set; }
    [field: SerializeField] public Sprite Visual { get; private set; }

    public static event Action<int> selectCard;

    private RectTransform rectTransform;
    private Outline outline;
    private GameManager gameManager;

    public void Init(CardDataSO cardSO)
    {
        cardName = cardSO.Name;
        Description = cardSO.Description;
        VideoGameWeight = cardSO.VideoGameWeight;
        SportsWeight = cardSO.SportsWeight;
        ToyWeight = cardSO.ToyWeight;
        Visual = cardSO.Visual;
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        outline = GetComponent<Outline>();
        gameManager = FindObjectOfType<GameManager>();
        var image = GetComponent<Image>();
        image.sprite = Visual;
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
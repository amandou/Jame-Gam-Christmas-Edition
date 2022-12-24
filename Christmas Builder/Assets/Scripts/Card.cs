using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public int id;
    public string name;
    public int multiplyer;
    public float value;
    public bool canBeSelected;
    public bool isSelected;
    public int handIndex;
    public Button confirmButton;
    public Transform cards;

    public static event Action<int> selectCard;

    private RectTransform rectTransform;
    private Outline outline;
    private GameManager gameManager;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        outline = GetComponent<Outline>();
        gameManager = FindObjectOfType<GameManager>();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canBeSelected)
        {
            outline.enabled = true;
            confirmButton.interactable = true;

            int selectedCardIndex = gameManager.getSelectedCard();
            if (handIndex != selectedCardIndex)
            {
                foreach (Transform child in cards)
                {
                    int childHandIndex = child.transform.gameObject.GetComponent<Card>().handIndex;
                    if (childHandIndex == selectedCardIndex)
                    {
                        child.transform.gameObject.GetComponent<Outline>().enabled = false;
                    }
                }
            }
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
}

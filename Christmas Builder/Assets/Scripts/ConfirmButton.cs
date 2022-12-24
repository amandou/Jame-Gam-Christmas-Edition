using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    private int maxScore = 100;

    public Transform cards;
    public Image scoreBarSprite;

    public static event Action useCard;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        button = GetComponent<Button>();
    }

    public void ConfirmChoice()
    {
        useCard?.Invoke();
        UndoSelections();

        UpdateChildInfo();
        UpdateSantaClausScore();
        ActiveCardEffect();
    }

    private void UpdateSantaClausScore()
    {
        Debug.Log("Santa Claus Score");

        float childStatus = 1f;
        float giftValue = 5f;
        float wantendScale = 1f;

        float currentScore = childStatus * giftValue * wantendScale;

        scoreBarSprite.fillAmount -= currentScore / maxScore;
    }

    private void UpdateChildInfo()
    {
        Debug.Log("Child Info Updated");
    }

    private void ActiveCardEffect()
    {
        Debug.Log("Active Card Effect");
    }

    private void UndoSelections()
    {
        gameManager.setSelectedCard(-1);
        button.interactable = false;
    }
}
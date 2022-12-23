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

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        button = GetComponent<Button>();
    }

    public void ConfirmChoice()
    {
        int selectedCardIndex = gameManager.getSelectedCard();
        gameManager.availableCardSlots[selectedCardIndex] = true;

        GameObject selectedCard = gameManager.cardSlots[selectedCardIndex].transform.gameObject;

        HideConfirmedCard(selectedCardIndex); 
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

    private void HideConfirmedCard(int selectedCardIndex)
    {
        foreach (Transform child in cards)
        {
            int childHandIndex = child.transform.gameObject.GetComponent<Card>().handIndex;
            if (childHandIndex == selectedCardIndex)
            {
                child.transform.gameObject.SetActive(false);
            }
        }
    }

    private void UndoSelections()
    {
        gameManager.setSelectedCard(-1);
        button.interactable = false;
    }


}

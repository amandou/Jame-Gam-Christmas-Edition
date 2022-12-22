using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    public Transform cards;

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

            if (child.transform.gameObject.GetComponent<Card>().handIndex == selectedCardIndex)
            {
                child.transform.gameObject.SetActive(false);
            }
        }
    }

    private void UndoSelections()
    {
        gameManager.setSelectedCard(0);
        button.interactable = false;
    }


}

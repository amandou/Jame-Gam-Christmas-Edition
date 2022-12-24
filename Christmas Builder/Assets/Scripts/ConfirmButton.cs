using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    public static event Action useCard;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        button = GetComponent<Button>();
    }

    public void ConfirmChoice()
    {
        useCard?.Invoke();
        button.interactable = false;
    }
}
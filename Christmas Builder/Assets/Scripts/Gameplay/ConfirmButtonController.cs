using Assets.Scripts.Cards;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gameplay
{
    public class ConfirmButtonController : MonoBehaviour
    {
        private Button button;

        public static event Action UseCardHandler;

        private void OnEnable()
        {
            Card.CardIsSelectedEventHandler += EnableButton;
        }

        private void OnDisable()
        {
            Card.CardIsSelectedEventHandler -= EnableButton;
        }

        private void EnableButton(int index)
        {
            button.interactable = true;
        }

        private void Start()
        {
            button = GetComponent<Button>();
        }

        public void ConfirmChoice()
        {
            UseCardHandler?.Invoke();
            button.interactable = false;
        }
    }
}
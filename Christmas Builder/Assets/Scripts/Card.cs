using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Card : MonoBehaviour
{
    public int id;
    public string name;
    public int multiplyer;
    public float value;
    public bool canBeSelected;
    public bool isSelected;

    public int handIndex;

    public Button confirmButton;

    public GameObject circle;

    public static event Action<int> selectCard;

    private void OnMouseDown()
    {
        if (canBeSelected)
        {
            circle.SetActive(true);
            circle.transform.position = new Vector3(gameObject.transform.position.x, circle.transform.position.y, circle.transform.position.z);
            confirmButton.interactable = true;
            selectCard?.Invoke(handIndex);
        }
    }

    void OnMouseEnter()
    {
       transform.position += Vector3.up * 5;
       canBeSelected = true;
    }

    void OnMouseExit()
    {
        transform.position += Vector3.up * -5;
        canBeSelected = false;
    }
}

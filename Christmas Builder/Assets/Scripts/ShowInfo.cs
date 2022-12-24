using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    private void ShowInformation()
    {
        Debug.Log("show information End");
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        GameManager.showInfo += ShowInformation;
    }

    private void OnDisable()
    {
        GameManager.showInfo -= ShowInformation;
    }
}

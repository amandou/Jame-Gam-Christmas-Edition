using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideChildInfo : MonoBehaviour
{

    private void HideChildIfo()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.hideInfo += HideChildIfo;
    }

    private void OnDisable()
    {
        GameManager.hideInfo -= HideChildIfo;
    }
}

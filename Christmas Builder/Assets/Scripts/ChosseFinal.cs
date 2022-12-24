using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChosseFinal : MonoBehaviour
{
    public TextMeshProUGUI endGameText;

    public Image scoreBarSprite;

    void Update()
    {
        if (scoreBarSprite.fillAmount >= 0.8)
        {
            endGameText.text = "All the childers are happy and Santa's reputation is good";
        }
        else 
        {
            endGameText.text = "Some childers are a little unhappy and Santa's reputation is going bad";
        }

    }
}

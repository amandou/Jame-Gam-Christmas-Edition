using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingSelector : MonoBehaviour
{
    public TextMeshProUGUI endGameText;

    public Image scoreBarSprite;

    private void Update()
    {
        if (scoreBarSprite.fillAmount >= 0.8)
        {
            endGameText.text = "All the children are happy and Santa's reputation is good :)";
        }
        else
        {
            endGameText.text = "Some children are a little unhappy and Santa's reputation is going bad :(";
        }
    }
}
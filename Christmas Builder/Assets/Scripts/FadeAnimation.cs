using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeAnimation : MonoBehaviour
{
    [SerializeField]
    private float fadeTime = 1f;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;


    public void FadeIn()
    {
        canvasGroup.alpha = 0f;
        rectTransform.transform.localPosition = new Vector3(0f, 5f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0.009f, -0.15f), fadeTime, false).SetEase(Ease.OutCubic);
        canvasGroup.DOFade(1f, fadeTime);
    }

    public void FadeOut()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0, fadeTime);
        rectTransform.transform.localPosition = new Vector3(0.009f, -0.15f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -5f), fadeTime, false).SetEase(Ease.OutCubic);
    }
}

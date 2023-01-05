using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Letters
{
    public class FadeAnimation : MonoBehaviour
    {
        [SerializeField] private float fadeTime = 1f;
        [field: SerializeField] public CanvasGroup CanvasGroup { get; set; }
        [field: SerializeField] public RectTransform RectTransform { get; set; }

        public void FadeIn()
        {
            CanvasGroup.alpha = 0f;
            RectTransform.transform.localPosition = new Vector3(0f, 5f, 0f);
            RectTransform.DOAnchorPos(new Vector2(0.009f, -0.15f), fadeTime, false).SetEase(Ease.OutCubic);
            CanvasGroup.DOFade(1f, fadeTime);
        }

        public void FadeOut()
        {
            CanvasGroup.alpha = 1f;
            CanvasGroup.DOFade(0, fadeTime);
            RectTransform.transform.localPosition = new Vector3(0.009f, -0.15f, 0f);
            RectTransform.DOAnchorPos(new Vector2(0f, -5f), fadeTime, false).SetEase(Ease.OutCubic);
        }
    }
}
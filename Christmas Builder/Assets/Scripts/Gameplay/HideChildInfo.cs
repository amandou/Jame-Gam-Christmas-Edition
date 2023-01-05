using Assets.Scripts.Letters;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class HideChildInfo : MonoBehaviour
    {
        private void HideChildIfo()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            LetterController.ReachedEndingEventHandler += HideChildIfo;
        }

        private void OnDisable()
        {
            LetterController.ReachedEndingEventHandler -= HideChildIfo;
        }
    }
}
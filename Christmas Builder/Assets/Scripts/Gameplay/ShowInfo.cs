using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class ShowInfo : MonoBehaviour
    {
        private void ShowInformation()
        {
            gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            GameManager.ShowInfoHandler += ShowInformation;
        }

        private void OnDisable()
        {
            GameManager.ShowInfoHandler -= ShowInformation;
        }
    }
}
using Assets.Scripts.Letters;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class EndingSelector : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI EndGameText { get; private set; }
        [field: SerializeField] public GameObject EndGameVisual { get; private set; }
        private float _score;

        private void Awake()
        {
            _score = 0f;
        }

        private void OnEnable()
        {
            GameManager.UpdateScoreHandler += UpdateSantaClausScore;
            LetterController.ReachedEndingEventHandler += DecideEnding;
        }

        private void OnDisable()
        {
            GameManager.UpdateScoreHandler -= UpdateSantaClausScore;
            LetterController.ReachedEndingEventHandler -= DecideEnding;
        }

        private void UpdateSantaClausScore(float scoreUpdate)
        {
            _score += scoreUpdate;
        }

        private void DecideEnding()
        {
            EndGameVisual.SetActive(true);
            if (_score >= 0.8)
            {
                EndGameText.text = "All the children are happy and Santa's reputation is good :)";
            }
            else
            {
                EndGameText.text = "Some children are a little unhappy and Santa's reputation is going bad :(";
            }
        }
    }
}
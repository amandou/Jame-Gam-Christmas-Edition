using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gameplay
{
    public class ScoreController : MonoBehaviour
    {
        private const float MAX_SCORE = 10f;
        private float _score;
        [field: SerializeField] public Image ScoreBarSprite { get; private set; }

        private void OnEnable()
        {
            GameManager.UpdateScoreHandler += UpdateSantaClausScore;
        }

        private void OnDisable()
        {
            GameManager.UpdateScoreHandler -= UpdateSantaClausScore;
        }

        private void UpdateSantaClausScore(float score)
        {
            _score += score;
            ScoreBarSprite.fillAmount += _score / MAX_SCORE;
        }
    }
}
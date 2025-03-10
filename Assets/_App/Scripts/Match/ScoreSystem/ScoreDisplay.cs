using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Tirlim.Match
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        private ScoreSystem _scoreSystem;
        
        [Inject]
        public void Construct(ScoreSystem scoreSystem)
        {
            _scoreSystem = scoreSystem;
            _scoreSystem.OnUpdateScore += OnUpdateScore;
        }

        private void OnDestroy()
        {
            _scoreSystem.OnUpdateScore -= OnUpdateScore;
        }

        private void OnUpdateScore(int scoreA, int scoreB)
        {
            _scoreText.SetText(scoreA + ":" + scoreB);
        }
    }
}
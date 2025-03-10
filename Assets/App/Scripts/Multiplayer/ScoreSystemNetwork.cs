using Mirror;
using Tirlim.Match;
using Zenject;

namespace Tirlim.Multiplayer
{
    public class ScoreSystemNetwork : NetworkBehaviour
    {
        private ScoreSystem _scoreSystem;

        [Inject]
        public void Construct(ScoreSystem scoreSystem)
        {
            _scoreSystem = scoreSystem;
            _scoreSystem.OnUpdateScore += UpdateScore;
        }

        private void OnDestroy()
        {
            _scoreSystem.OnUpdateScore -= UpdateScore;
        }
        
        private void UpdateScore(int scoreA, int scoreB)
        {
            if (isServer)
                UpdateScoreRpc(scoreA, scoreB);
        }
        
        [ClientRpc]
        private void UpdateScoreRpc(int scoreA, int scoreB)
        {
            if(!isServer)
                _scoreSystem.SetScore(scoreA, scoreB);
        }
    }
}
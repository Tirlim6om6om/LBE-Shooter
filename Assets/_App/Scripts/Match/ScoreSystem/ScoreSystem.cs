using System;
using UnityEngine;
using Zenject;


namespace Tirlim.Match
{
    public class ScoreSystem : MonoBehaviour
    {
        public event Action<int, int> OnUpdateScore;
        public int ScoreA { get { return _scoreA; } }
        public int ScoreB{ get { return _scoreB; } }
        
        private int _scoreA;
        private int _scoreB;
        private TeamSystem _teamSystem;

        [Inject]
        public void Construct(TeamSystem teamSystem)
        {
            _teamSystem = teamSystem;
            _teamSystem.OnPlayerJoinTeam += OnJoinTeam;
            _teamSystem.OnPlayerOutTeam += OnOutTeam;
        }

        private void OnDestroy()
        {
            _teamSystem.OnPlayerJoinTeam -= OnJoinTeam;
            _teamSystem.OnPlayerOutTeam -= OnOutTeam;
        }

        private void OnJoinTeam(PlayerInfo player, Team team)
        {
            if(team != Team.neutral)
                player.PlayerTeam.OnPlayerDead += OnPlayerDead;
        }
        
        private void OnOutTeam(PlayerInfo player, Team team)
        {
            if(team != Team.neutral)
                player.PlayerTeam.OnPlayerDead -= OnPlayerDead;
        }

        private void OnPlayerDead(Team team)
        {
            switch (team)
            {
                case Team.teamA:
                    _scoreA++;
                    break;
                case Team.teamB:
                    _scoreB++;
                    break;
            }
            OnUpdateScore?.Invoke(_scoreA, _scoreB);
        }

        public void SetScore(int scoreA, int scoreB)
        {
            _scoreA = scoreA;
            _scoreB = scoreB;
            OnUpdateScore?.Invoke(_scoreA, _scoreB);
        }
    }
}
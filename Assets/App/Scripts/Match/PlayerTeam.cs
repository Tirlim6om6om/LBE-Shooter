using System;
using Mirror;
using Tirlim.Player;
using Zenject;

namespace Tirlim.Match
{
    public class PlayerTeam : NetworkBehaviour
    {
        public Team CurrentTeam
        {
            get { return _currentTeam; }
            set { _currentTeam = value; }
        }

        public event Action<Team> OnSwitchTeam;
        public event Action<Team> OnPlayerDead;
        
        private Team _currentTeam;
        private PlayerHealthSystem _healthSystem;

        [Inject]
        public void Construct(PlayerHealthSystem healthSystem)
        {
            _healthSystem = healthSystem;
            _healthSystem.OnSwitchState += OnDead;
        }

        private void OnDestroy()
        {
            _healthSystem.OnSwitchState -= OnDead;
        }

        private void OnDead(PlayerState state)
        {
            if(state == PlayerState.Dead)
                OnPlayerDead?.Invoke(_currentTeam);
        }

        public void SetTeam(Team team)
        {
            _currentTeam = team;
            OnSwitchTeam?.Invoke(team);
        }
    }
}

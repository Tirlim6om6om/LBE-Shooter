using System;
using Mirror;
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

        private Team _currentTeam;


        public void SetTeam(Team team)
        {
            OnSwitchTeam?.Invoke(team);
        }
    }
}

using System;
using Mirror;
using Tirlim.Match;
using Tirlim.Player;
using UnityEngine;
using Zenject;

namespace Tirlim.Multiplayer
{
    public class PlayerEventSystemNetwork : NetworkBehaviour
    {
        private PlayerHealthSystem _healthSystem;
        private PlayerTeam _playerTeam;

        [Inject]
        public void Construct(PlayerHealthSystem playerHealthSystem, PlayerTeam playerTeam)
        {
            _healthSystem = playerHealthSystem;
            _healthSystem.OnSwitchState += OnSwitchState;
            _healthSystem.OnChangeHealth += OnChangeHealth;
            
            _playerTeam = playerTeam;
            _playerTeam.OnSwitchTeam += OnSwitchTeam;
        }

        private void OnDestroy()
        {
            _healthSystem.OnSwitchState -= OnSwitchState;
            _healthSystem.OnChangeHealth -= OnChangeHealth;
            _playerTeam.OnSwitchTeam -= OnSwitchTeam;
        }

        #region SwitchState sync

        private void OnSwitchState(PlayerState state)
        {
            if(!isOwned)
                return;
            
            OnSwitchStateCmd(state);
        }

        [Command]
        private void OnSwitchStateCmd(PlayerState state)
        {
            OnSwitchStateRpc(state);
        }
        
        [ClientRpc(includeOwner = false)]
        private void OnSwitchStateRpc(PlayerState state)
        {
            switch (state)
            {
                case PlayerState.Alive:
                    _healthSystem.Regeneration();
                    break;
                case PlayerState.Dead:
                    _healthSystem.Dead();
                    break;
                case PlayerState.Immortal:
                    _healthSystem.SetImmortal(true);
                    break;
            }
        }

        #endregion

        #region Health sync

        private void OnChangeHealth(int health)
        {
            if(!isOwned)
                return;
            OnChangeHealthCmd(health);
        }
        
        [Command]
        private void OnChangeHealthCmd(int health)
        {
            OnChangeHealthRpc(health);
        }
        
        [ClientRpc(includeOwner = false)]
        private void OnChangeHealthRpc(int health)
        {
            _healthSystem.SetHealth(health);
        }

        #endregion

        #region Team Sync

        private void OnSwitchTeam(Team team)
        {
            if(!isServer)
                return;
            OnSwitchTeamRpc(team);
        }

        [ClientRpc]
        private void OnSwitchTeamRpc(Team team)
        {
            if(isServer)
                return;
            _playerTeam.SetTeam(team);
        }

        #endregion
    }

}
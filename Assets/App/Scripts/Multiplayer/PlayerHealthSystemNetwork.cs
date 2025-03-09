using System;
using Mirror;
using Tirlim.Player;
using UnityEngine;
using Zenject;

namespace Tirlim.Multiplayer
{
    public class PlayerHealthSystemNetwork : NetworkBehaviour
    {
        private PlayerHealthSystem _healthSystem;

        [Inject]
        public void Construct(PlayerHealthSystem playerHealthSystem)
        {
            _healthSystem = playerHealthSystem;
            _healthSystem.OnSwitchState += OnSwitchState;
            _healthSystem.OnChangeHealth += OnChangeHealth;
        }

        private void OnDestroy()
        {
            _healthSystem.OnSwitchState -= OnSwitchState;
            _healthSystem.OnChangeHealth -= OnChangeHealth;
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
        
    }

}
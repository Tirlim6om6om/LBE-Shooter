using UnityEngine;
using Zenject;

namespace Tirlim.Player
{
    public class DeadEffector : MonoBehaviour
    {
        private PlayerHealthSystem _playerHealthSystem;

        [Inject]
        public void Construct(PlayerHealthSystem playerHealthSystem)
        {
            _playerHealthSystem = playerHealthSystem;
            _playerHealthSystem.OnSwitchState += SwitchState;
        }

        private void SwitchState(PlayerState state)
        {
            switch (state)
            {
                case PlayerState.Dead:
                    OnDeath();
                    break;
                case PlayerState.Alive or PlayerState.Immortal:
                    OnAlive();
                    break;
            }
        }

        protected virtual void OnDeath() { }

        protected virtual void OnAlive() { }
    }
}

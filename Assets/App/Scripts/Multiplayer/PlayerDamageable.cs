using Mirror;
using Tirlim.Gun;
using Tirlim.Player;
using UnityEngine;
using Zenject;

namespace Tirlim.Multiplayer
{
    public class PlayerDamageable : NetworkBehaviour, IDamageable
    {
        private PlayerHealthSystem _healthSystem;

        [Inject]
        public void Construct(PlayerHealthSystem playerHealthSystem)
        {
            _healthSystem = playerHealthSystem;
        }

        public void SetDamage(int damage)
        {
            if (isOwned)
            {
                _healthSystem.SetDamage(damage);
            }
        }
    }
}

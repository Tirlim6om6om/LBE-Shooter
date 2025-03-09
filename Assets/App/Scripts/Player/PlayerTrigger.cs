using Tirlim.Player;
using UnityEngine;
using Zenject;

namespace Tirlim.Player
{
    public class PlayerTrigger : MonoBehaviour
    {
        public PlayerHealthSystem PlayerHealthSystem { get { return _playerHealthSystem; } }

        private PlayerHealthSystem _playerHealthSystem;

        [Inject]
        public void Construct(PlayerHealthSystem playerHealthSystem)
        {
            _playerHealthSystem = playerHealthSystem;
        }
    }

}
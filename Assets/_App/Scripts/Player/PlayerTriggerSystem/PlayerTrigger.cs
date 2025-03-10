using Tirlim.Match;
using Tirlim.Player;
using UnityEngine;
using Zenject;

namespace Tirlim.Player
{
    /// <summary>
    /// Класс, для обращения к игроку, через триггеры
    /// </summary>
    public class PlayerTrigger : MonoBehaviour
    {
        private PlayerHealthSystem _playerHealthSystem;
        private PlayerTeam _playerTeam;

        [Inject]
        public void Construct(PlayerHealthSystem playerHealthSystem, PlayerTeam playerTeam)
        {
            _playerHealthSystem = playerHealthSystem;
            _playerTeam = playerTeam;
        }

        public void SetImmortal(bool active, Team team)
        {
            if (_playerTeam.CurrentTeam == team)
            {
                _playerHealthSystem.SetImmortal(active);
            }
        }
    }
}
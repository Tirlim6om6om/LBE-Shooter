using Mirror;
using Tirlim.Player;
using UnityEngine;
using Zenject;

namespace Tirlim.Multiplayer
{
    public class NetworkPlayer : NetworkBehaviour
    {
        [SerializeField] private IKTargetsTracking ikTargetsTracking;
        
        private void Awake()
        {
            ProjectContext.Instance.Container.InjectGameObject(gameObject);
            ikTargetsTracking.enabled = false;
        }

        public override void OnStartAuthority()
        {
            ikTargetsTracking.enabled = true;
        }
    }
}

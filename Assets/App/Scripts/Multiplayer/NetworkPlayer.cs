using Mirror;
using Tirlim.Player;
using UnityEngine;
using Zenject;

namespace Tirlim.Multiplayer
{
    public class NetworkPlayer : NetworkBehaviour
    {
        [SerializeField] private IKTargetsTracking ikTargetsTracking;
        [SerializeField] private CalibrateController calibrateController;
        
        private void Awake()
        {
            ProjectContext.Instance.Container.InjectGameObject(gameObject);//TODO УБРАТЬ
            ikTargetsTracking.enabled = false;
            calibrateController.enabled = false;
        }

        public override void OnStartAuthority()
        {
            ikTargetsTracking.enabled = true;
            calibrateController.enabled = true;
        }
    }
}

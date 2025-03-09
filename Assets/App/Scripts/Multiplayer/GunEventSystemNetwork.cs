using Mirror;
using Tirlim.Gun;
using UnityEngine;
using Zenject;

namespace Tirlim.Multiplayer
{
    /// <summary>
    /// Синхронизация событий по сети
    /// </summary>
    public class GunEventSystemNetwork : NetworkBehaviour, IShootNotifier, IUnloadNotifier, IStartReloadNotifier
    {
        private GunEventSystem _gunEventSystem;
        
        [Inject]
        public void Construct(GunEventSystem gunEventSystem)
        {
            _gunEventSystem = gunEventSystem;
        }
        
        public void OnShootMessage()
        {
            if(isOwned)
                OnShootCmd();
        }

        [Command]
        private void OnShootCmd()
        {
            OnShootRpc();
        }
        
        [ClientRpc(includeOwner = false)]
        private void OnShootRpc()
        {
            _gunEventSystem.OnShoot();
        }
        
        public void OnUnloadMessage()
        {
            if(isOwned)
                OnUnloadCmd();
        }
        
        [Command]
        private void OnUnloadCmd()
        {
            OnUnloadRpc();
        }
        
        [ClientRpc(includeOwner = false)]
        private void OnUnloadRpc()
        {
            _gunEventSystem.OnUnload();
        }

        public void OnStartReloadMessage()
        {
            if(isOwned)
                OnStartReloadCmd();
        }
        
        [Command]
        private void OnStartReloadCmd()
        {
            OnStartReloadRpc();
        }
        
        [ClientRpc(includeOwner = false)]
        private void OnStartReloadRpc()
        {
            _gunEventSystem.OnStartReload();
        }

    }
}

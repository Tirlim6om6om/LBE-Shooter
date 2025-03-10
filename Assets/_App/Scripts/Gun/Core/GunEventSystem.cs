using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Tirlim.Gun
{
    public class GunEventSystem : MonoBehaviour
    {
        private IEnumerable<IShootNotifier> _shootNotifier;
        private IEnumerable<IReloadedNotifier> _reloadedNotifier;
        private IEnumerable<IStartReloadNotifier> _startReloadNotifier;
        private IEnumerable<IUnloadNotifier> _unloadNotifiers;
        
        [Inject]
        public void Construct(
            IEnumerable<IShootNotifier> shootNotifier, 
            IEnumerable<IReloadedNotifier> reloadNotifier,
            IEnumerable<IStartReloadNotifier> startReloadNotifier,
            IEnumerable<IUnloadNotifier> unloadNotifier)
        {
            _shootNotifier = shootNotifier;
            _reloadedNotifier = reloadNotifier;
            _startReloadNotifier = startReloadNotifier;
            _unloadNotifiers = unloadNotifier;
        }
        
        public void OnShoot()
        {
            foreach (var shootNotifier in _shootNotifier)
            {
                shootNotifier?.OnShootMessage();
            }
        }
        
        public void OnReloaded()
        {
            foreach (var reloadNotifier in _reloadedNotifier)
            {
                reloadNotifier?.OnReloadedMessage();
            }
        }
        
        public void OnStartReload()
        {
            foreach (var startReloadNotifier in _startReloadNotifier)
            {
                startReloadNotifier?.OnStartReloadMessage();
            }
        }
        
        public void OnUnload()
        {
            foreach (var unloadNotifier in _unloadNotifiers)
            {
                unloadNotifier?.OnUnloadMessage();
            }
        }
    }

}
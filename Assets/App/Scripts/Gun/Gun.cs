using System.Collections;
using UnityEngine;
using Zenject;

namespace Tirlim.Gun
{
    public class Gun : MonoBehaviour, IReloadedNotifier, IStartReloadNotifier
    {
        public int Ammo { get { return _ammo; } }
        public bool IsLoaded { get { return _ammo == _specificationGun.StartAmmo; } }

        private GunEventSystem _gunEventSystem;
        private SpecificationGun _specificationGun;
        private WaitForSeconds _waitForLoad;
        private bool _isLoadShot;
        private bool _isReloading;
        private int _ammo;

        [Inject]
        public void Construct(
            GunEventSystem gunEventSystem,
            SpecificationGun specificationGun)
        {
            _gunEventSystem = gunEventSystem;
            _specificationGun = specificationGun;
            _waitForLoad = new WaitForSeconds(specificationGun.ShotInterval);
            _ammo = specificationGun.StartAmmo;
        }

        public void Shoot()
        {
            if (_isLoadShot)
                return;
            
            WaitLoadShot();
            
            if (_ammo == 0 | _isReloading)
            {
                _gunEventSystem.OnUnload();
                return;
            }

            _ammo -= 1;
            _gunEventSystem.OnShoot();
        }
        
        public void OnStartReloadMessage()
        {
            _isReloading = true;
        }

        public void OnReloadedMessage()
        {
            _ammo = _specificationGun.StartAmmo;
            _isReloading = false;
        }

        public void WaitLoadShot()
        {
            if(_isLoadShot)
                return;
            StartCoroutine(WaitingLoadShot());
        }

        private IEnumerator WaitingLoadShot()
        {
            _isLoadShot = true;
            yield return _waitForLoad;
            _isLoadShot = false;
        }
    }
}
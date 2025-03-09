using System.Collections;
using UnityEngine;
using Zenject;

namespace Tirlim.Gun
{
    public class Gun : MonoBehaviour, IReloadedNotifier
    {
        public int Ammo { get { return 0; } }

        private GunEventSystem _gunEventSystem;
        private SpecificationGun _specificationGun;
        private WaitForSeconds _waitForLoad;
        private bool isLoadShot;
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
            if (isLoadShot)
                return;
            
            WaitLoadShot();
            
            if (_ammo == 0)
            {
                _gunEventSystem.OnUnload();
                return;
            }

            _ammo -= 1;
            _gunEventSystem.OnShoot();
        }

        public void OnReloadedMessage()
        {
            _ammo = _specificationGun.StartAmmo;
        }

        public void WaitLoadShot()
        {
            if(isLoadShot)
                return;
            StartCoroutine(WaitingLoadShot());
        }

        private IEnumerator WaitingLoadShot()
        {
            isLoadShot = true;
            yield return _waitForLoad;
            isLoadShot = false;
        }
    }
}
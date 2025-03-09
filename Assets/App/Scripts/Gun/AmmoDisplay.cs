using Tirlim.Gun;
using TMPro;
using UnityEngine;
using Zenject;

namespace Tirlim.Gun
{
    public class AmmoDisplay : MonoBehaviour, IShootNotifier, IReloadedNotifier
    {
        [SerializeField] private TextMeshProUGUI textOut;
    
        private Gun _gun;
        private int _startAmmo;

        [Inject]
        public void Construct(Gun gun, SpecificationGun specificationGun)
        {
            _gun = gun;
            _startAmmo = specificationGun.StartAmmo;
        }
        
        public void OnShootMessage()
        {
            SetAmmoText(_gun.Ammo);
        }

        public void OnReloadedMessage()
        {
            SetAmmoText(_gun.Ammo);
        }

        private void SetAmmoText(int ammo)
        {
            textOut.text = ammo.ToString() + "/" + _startAmmo;
        }
    }
}
using UnityEngine;
using Zenject;

namespace Tirlim.Gun
{
    [CreateAssetMenu(fileName = "GunSettingsInstaller", menuName = "Installers/GunSettingsInstaller")]
    public class GunSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private AudioGunLibrary audioGunLibrary;
        [SerializeField] private SpecificationGun specificationGun;
        
        public override void InstallBindings()
        {
            Container.BindInstance(audioGunLibrary);
            Container.BindInstance(specificationGun);
        }
    }
}
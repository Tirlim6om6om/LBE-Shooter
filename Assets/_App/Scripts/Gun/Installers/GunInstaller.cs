using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Tirlim.Gun
{
    public class GunInstaller : MonoInstaller
    {
        [SerializeField] private Gun gun;
        [SerializeField] private GunEventSystem gunEventSystem;
        
        public override void InstallBindings()
        {
            Container.Bind<IShootNotifier>().FromComponentsInHierarchy().AsTransient();
            Container.Bind<IReloadedNotifier>().FromComponentsInHierarchy().AsTransient();
            Container.Bind<IUnloadNotifier>().FromComponentsInHierarchy().AsTransient();
            Container.Bind<IStartReloadNotifier>().FromComponentsInHierarchy().AsTransient();
            Container.BindInstance(gun).AsSingle();
            Container.BindInstance(gunEventSystem).AsSingle();
            
        }
    }
}
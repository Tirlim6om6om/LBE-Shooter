using Game.Network;
using Mirror;
using UnityEngine;
using Zenject;

namespace Tirlim.Multiplayer
{
    public class NetworkInstaller : MonoInstaller
    {
        [SerializeField] private CustomNetworkManager networkManager;
        
        public override void InstallBindings()
        {
            CustomNetworkManager netManagerInstance = Container.InstantiatePrefabForComponent<CustomNetworkManager>(networkManager);
            Container.BindInstance(netManagerInstance).AsSingle().NonLazy();
            Container.BindInstance(netManagerInstance.GetComponent<CustomNetworkDiscovery>()).AsSingle().NonLazy();
        }
    }
}
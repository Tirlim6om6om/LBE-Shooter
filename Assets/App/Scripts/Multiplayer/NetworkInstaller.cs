using Game.Network;
using Mirror;
using UnityEngine;
using Zenject;

namespace Tirlim.Multiplayer
{
    public class NetworkInstaller : MonoInstaller
    {
        [SerializeField] private NetworkManager networkManager;
        
        public override void InstallBindings()
        {
            NetworkManager netManagerInstance = Container.InstantiatePrefabForComponent<NetworkManager>(networkManager);
            Container.BindInstance(netManagerInstance).AsSingle().NonLazy();
            Container.BindInstance(netManagerInstance.GetComponent<CustomNetworkDiscovery>()).AsSingle().NonLazy();
        }
    }
}
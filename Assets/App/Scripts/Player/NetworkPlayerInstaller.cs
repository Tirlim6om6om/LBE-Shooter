using Tirlim.Player;
using UnityEngine;
using Zenject;

public class NetworkPlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerHealthSystem healthSystem;
    
    public override void InstallBindings()
    {
        Container.BindInstance(healthSystem).AsSingle().NonLazy();
    }
}

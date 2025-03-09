using Tirlim.Match;
using Tirlim.Player;
using UnityEngine;
using Zenject;

public class NetworkPlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerHealthSystem healthSystem;
    [SerializeField] private PlayerTeam playerTeam;
    
    public override void InstallBindings()
    {
        Container.BindInstance(healthSystem).AsSingle().NonLazy();
        Container.BindInstance(playerTeam).AsSingle().NonLazy();
    }
}

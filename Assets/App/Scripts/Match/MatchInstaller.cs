using UnityEngine;
using Zenject;

namespace Tirlim.Match
{
    public class MatchInstaller : MonoInstaller
    {
        [SerializeField] private ScoreSystem scoreSystem;
        [SerializeField] private TeamSystem teamSystem;
        
        public override void InstallBindings()
        {
            Container.BindInstance(scoreSystem).AsSingle().NonLazy();
            Container.BindInstance(teamSystem).AsSingle().NonLazy();
        }
    }
}
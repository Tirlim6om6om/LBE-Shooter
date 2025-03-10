using UnityEngine;
using Zenject;

namespace Tirlim.Match
{
    [CreateAssetMenu(fileName = "MatchConfigsInstaller", menuName = "Installers/MatchConfigsInstaller")]
    public class MatchConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private TeamColorsConfig colorsConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(colorsConfig);
        }
    }
}

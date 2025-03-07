using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using Zenject;

namespace Tirlim.Player
{
    public class XRInstaller : MonoInstaller
    {
        [SerializeField] private GameObject xrOriginPrefab;
        [SerializeField] private GameObject xrSetup;
        [SerializeField] private bool useDeviceSimulator = false;
        [SerializeField] private XRDeviceSimulator deviceSimulator;

        public override void InstallBindings()
        {
            Container.InstantiatePrefab(xrSetup);
            XROrigin xrOriginInstance = Container.InstantiatePrefabForComponent<XROrigin>(xrOriginPrefab);

            Container.BindInstance(xrOriginInstance).AsSingle().NonLazy();

            if (useDeviceSimulator && (Application.isEditor || Application.platform != RuntimePlatform.Android))
            {
                Container.InstantiatePrefabForComponent<XRDeviceSimulator>(this.deviceSimulator);
            }
        }
    }
}

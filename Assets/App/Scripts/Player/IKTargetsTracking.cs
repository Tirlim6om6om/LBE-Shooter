using System;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using Zenject;

namespace Tirlim.Player
{
    public class IKTargetsTracking : MonoBehaviour
    {
        [SerializeField] private IKTarget[] targets;
        
        [Inject] private XROrigin xrOrigin;
        
        private void OnEnable()
        {
            foreach (var target in targets)
            {
                target.Enable();
            }
        }

        private void OnDisable()
        {
            foreach (var target in targets)
            {
                target.Disable();
            }
        }

        private void Update()
        {
            foreach (var target in targets)
            {
                target.UpdatePos(xrOrigin.CameraYOffset);
            }
        }
    }
}

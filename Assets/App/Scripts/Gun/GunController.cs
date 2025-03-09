using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Tirlim.Gun
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private InputActionReference shootActionReference = null;

        private Gun _gun;

        [Inject]
        public void Construct(Gun gun)
        {
            _gun = gun;
        }

        private void OnEnable()
        {
            if (shootActionReference != null && shootActionReference.action != null)
            {
                shootActionReference.action.Enable();
            }
        }

        private void OnDisable()
        {
            if (shootActionReference != null && shootActionReference.action != null)
            {
                shootActionReference.action.Disable();
            }
        }

        private void Update()
        {
            if (shootActionReference.action.IsPressed())
            {
                _gun.Shoot();
            }
        }
    }

}
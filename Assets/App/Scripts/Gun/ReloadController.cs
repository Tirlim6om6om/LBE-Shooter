using System.Collections;
using UnityEngine;
using Zenject;

namespace Tirlim.Gun
{
    public class ReloadController : MonoBehaviour
    {
        [SerializeField] private Transform weaponTransform;
        [SerializeField] private float targetAngle = 170f;
        [SerializeField] private float angleTolerance = 20f;

        private bool isLoading;
        private GunEventSystem _gunEventSystem;
        private Gun _gun;
        private WaitForSeconds _waitForSeconds;

        [Inject]
        public void Construct(GunEventSystem gunEventSystem, Gun gun, SpecificationGun specificationGun)
        {
            _gunEventSystem = gunEventSystem;
            _gun = gun;
            _waitForSeconds = new WaitForSeconds(specificationGun.TimeReload);
        }

        private void Update()
        {
            if(isLoading)
                return;
            
            float angle = Vector3.Angle(Vector3.up,  weaponTransform.forward);

            if (Mathf.Abs(angle - targetAngle) <= angleTolerance && !_gun.IsLoaded)
            {
                StartCoroutine(Load());
            }
        }

        private IEnumerator Load()
        {
            isLoading = true;
            _gunEventSystem.OnStartReload();
            yield return _waitForSeconds;
            _gunEventSystem.OnReloaded();
            isLoading = false;
        }
        
    }

}
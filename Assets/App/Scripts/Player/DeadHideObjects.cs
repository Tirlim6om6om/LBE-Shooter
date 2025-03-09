using UnityEngine;

namespace Tirlim.Player
{
    public class DeadHideObjects: DeadEffector
    {
        [SerializeField] private GameObject[] hideObjects;
        
        protected override void OnDeath()
        {
            foreach (var hideObject in hideObjects)
            {
                hideObject.SetActive(false);
            }
        }

        protected override void OnAlive()
        {
            foreach (var hideObject in hideObjects)
            {
                hideObject.SetActive(true);
            }
        }
    }
}
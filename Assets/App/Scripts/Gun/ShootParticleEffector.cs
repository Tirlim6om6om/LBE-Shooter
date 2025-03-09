using UnityEngine;

namespace Tirlim.Gun
{
    public class ShootParticleEffector : MonoBehaviour, IShootNotifier
    {
        [SerializeField] private ParticleSystem particleSystem;
        
        public void OnShootMessage()
        {
            particleSystem.Play();
        }
    }
}
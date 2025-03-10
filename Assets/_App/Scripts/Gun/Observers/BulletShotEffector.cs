using Unity.Mathematics;
using UnityEngine;

namespace Tirlim.Gun
{
    public class BulletShotEffector : MonoBehaviour, IShootNotifier
    {
        [SerializeField] private GameObject bulletPrefab;
        
        public void OnShootMessage()
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }
}

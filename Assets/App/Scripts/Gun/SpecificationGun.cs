using UnityEngine;

namespace Tirlim.Gun
{
    [CreateAssetMenu(fileName = "SpecificationGun", menuName = "ScriptableObjects/SpecificationGun")]
    public class SpecificationGun : ScriptableObject
    {
        public int StartAmmo { get { return startAmmo; } }
        public float TimeReload { get { return timeReload; } }
        public float ShotInterval { get { return shotInterval; } }

        [SerializeField] private int startAmmo;
        [SerializeField] private float timeReload;
        [SerializeField] private float shotInterval;
    }
}
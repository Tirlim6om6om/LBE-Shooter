using UnityEngine;

namespace Tirlim.Gun
{
    [CreateAssetMenu(fileName = "SpecificationGun", menuName = "ScriptableObjects/SpecificationGun")]
    public class SpecificationGun : ScriptableObject
    {
        public int StartAmmo { get { return startAmmo; } }
        public float TimeReload { get { return timeReload; } }
        public float ShotInterval { get { return shotInterval; } }
        public int Damage { get { return damage; } }
        public Vector3 HalfExtents { get { return halfExtents; } }

        [SerializeField] private int startAmmo;
        [SerializeField] private float timeReload;
        [SerializeField] private float shotInterval;
        [SerializeField] private int damage;
        [SerializeField] private Vector3 halfExtents;
    }
}
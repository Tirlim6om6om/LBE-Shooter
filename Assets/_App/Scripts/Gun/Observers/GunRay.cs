using System;
using RootMotion.FinalIK;
using Tirlim.Gun;
using UnityEngine;
using Zenject;

public class GunRay : MonoBehaviour, IShootNotifier
{
    [SerializeField] private LayerMask layerMask;
    private int damage;
    private Vector3 _halfExtents;
    
    [Inject]
    public void Construct(SpecificationGun specificationGun)
    {
        damage = specificationGun.Damage;
        _halfExtents = specificationGun.HalfExtents;
    }
    
    public void OnShootMessage()
    {
        if (Physics.BoxCast(transform.position, _halfExtents, transform.forward, 
                out RaycastHit hit, transform.rotation, 1000, layerMask))
        {
            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.SetDamage(damage);
            }
        }
    }

#if UNITY_EDITOR
    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.white);
        if (Physics.Raycast(transform.position, transform.forward, 
                out RaycastHit hit, 1000, layerMask))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
       
    }
#endif
}

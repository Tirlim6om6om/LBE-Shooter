using System;
using Tirlim.Gun;
using UnityEngine;
using Zenject;

public class GunRay : MonoBehaviour, IShootNotifier
{
    [SerializeField] private LayerMask layerMask;
    private int damage;

    [Inject]
    public void Construct(SpecificationGun specificationGun)
    {
        damage = specificationGun.Damage;
    }
    
    public void OnShootMessage()
    {
        if (Physics.Raycast(transform.position, transform.forward, 
                out RaycastHit hit, 1000, layerMask))
        {
            IDamageable damageable = hit.collider.GetComponentInParent<IDamageable>();
            if (damageable != null)
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

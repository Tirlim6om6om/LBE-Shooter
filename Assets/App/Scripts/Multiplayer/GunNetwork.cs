using System;
using Mirror;
using Tirlim.Gun;
using UnityEngine;

public class GunNetwork : NetworkBehaviour
{
    [SerializeField] private GunController gunController;
    
    private void Awake()
    {
        //gunController.enabled = false;
    }

    public override void OnStartAuthority()
    {
        gunController.enabled = true;
    }
}

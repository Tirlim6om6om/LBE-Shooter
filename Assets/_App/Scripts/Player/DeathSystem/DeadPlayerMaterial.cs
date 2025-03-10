using System.Collections.Generic;
using UnityEngine;

namespace Tirlim.Player
{
    public class DeadPlayerMaterial: DeadEffector
    {
        [SerializeField] private Renderer[] meshRenderers;
        [SerializeField] private Material deadMaterial;
        
        private Dictionary<Renderer, Material> startMaterials = new Dictionary<Renderer, Material>();
        
        private void Awake()
        {
            foreach (var meshRenderer in meshRenderers)
            {
                startMaterials.Add(meshRenderer, meshRenderer.material);
            }
        }
        
        protected override void OnDeath()
        {
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.material = deadMaterial;
            }
        }

        protected override void OnAlive()
        {
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.material = startMaterials[meshRenderer];
            }
        }
    }
}
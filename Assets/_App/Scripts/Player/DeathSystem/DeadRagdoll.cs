using RootMotion.FinalIK;
using UnityEngine;

namespace Tirlim.Player
{
    public class DeadRagdoll: DeadEffector
    {
        [SerializeField] private VRIK vrik;
        [SerializeField] private GameObject ragdoll;
        [SerializeField] private VRIK.References references;
        
        protected override void OnDeath()
        {
            ragdoll.transform.localScale = vrik.transform.localScale;
            Transform[] bones = vrik.references.GetTransforms();
            Transform[] targetBones = references.GetTransforms();
            
            for (int i = 0; i < targetBones.Length; i++)
            {
                if (bones[i] != null & targetBones[i] != null)
                {
                    targetBones[i].transform.localPosition = bones[i].transform.localPosition;
                    targetBones[i].transform.localRotation = bones[i].transform.localRotation;
                }
            }
            
            ragdoll.SetActive(true);
        }

        protected override void OnAlive()
        {
            ragdoll.SetActive(false);
        }
        
        [ContextMenu("Auto-detect References")]
        public void AutoDetectReferences() {
            VRIK.References.AutoDetectReferences(ragdoll.transform, out references);
        }
    }
}
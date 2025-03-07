using UnityEngine;
using UnityEngine.InputSystem;

namespace Tirlim.Player
{
    [System.Serializable]
    public class IKTarget
    {
        public TypeTargets type;
        [SerializeField] private Transform targetParent;
        [SerializeField] private InputActionReference positionReference;
        [SerializeField] private InputActionReference rotationReference;

        public void Enable()
        {
            if (positionReference != null && positionReference.action != null)
            {
                positionReference.action.Enable();
            }
            if (rotationReference != null && positionReference.action != null)
            {
                rotationReference.action.Enable();
            }
        }

        public void Disable()
        {
            if (positionReference != null && positionReference.action != null)
            {
                positionReference.action.Disable();
            }
            if (rotationReference != null && positionReference.action != null)
            {
                rotationReference.action.Disable();
            }
        }

        public void UpdatePos(float offsetY = 0)
        {
            targetParent.position = positionReference.action.ReadValue<Vector3>() + new Vector3(0, offsetY, 0);
            targetParent.rotation = rotationReference.action.ReadValue<Quaternion>();
        }
    }
}
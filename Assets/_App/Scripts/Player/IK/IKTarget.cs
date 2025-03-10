using UnityEngine;
using UnityEngine.InputSystem;

namespace Tirlim.Player
{
    [System.Serializable]
    public class IKTarget
    {
        [SerializeField] private Transform targetParent;
        [SerializeField] private InputActionReference positionReference;
        [SerializeField] private InputActionReference rotationReference;
        
        [Header("Оффсет позиции")]
        [SerializeField] private Vector3 offsetPos;
        [Header("Оффсет поворота")]
        [SerializeField] private Vector3 offsetRot;
        
        [Header("Стабилизация перемещения")]
        [SerializeField] private bool activeStabilizeMove;
        [Header("Стабилизация вращения")]
        [Tooltip("Вращается, если разница больше этого значения")]
        [SerializeField]
        private bool activeStabilizeRot;
        
        [Header("Интерполяция")]
        [Tooltip("Скорость интерполяции: 1 - выключает её")]
        [Range(0.5f, 1f)]
        [SerializeField] private float interpolationSpeed = 1;
        [Range(0f, 2f)]
        [SerializeField] private float rotSense = 0;
        
        public void Enable()
        {
            if (positionReference != null && positionReference.action != null)
            {
                positionReference.action.Enable();
            }
            if (rotationReference != null && rotationReference.action != null)
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
            targetParent.localPosition = CalculatePos(targetParent.localPosition,
                positionReference.action.ReadValue<Vector3>() + new Vector3(0, offsetY, 0));
            targetParent.localRotation = CalculateRot(targetParent.rotation,
                rotationReference.action.ReadValue<Quaternion>());
        }

        private bool CheckRotationReference()
        {
            return rotationReference != null && rotationReference.action != null;
        }
        
        private bool CheckPosReference()
        {
            return positionReference != null && positionReference.action != null;
        }
        
        private Vector3 CalculatePos(Vector3 origin, Vector3 target)
        {
            Vector3 targetOffset = target + targetParent.localRotation * offsetPos;
            if (!activeStabilizeMove)
                return targetOffset;
            
            return Vector3.Lerp(origin, targetOffset,  interpolationSpeed * 20 * Time.deltaTime);
        }

        private Quaternion CalculateRot(Quaternion origin, Quaternion target)
        {
            if (!IsValidQuaternion(origin) || !IsValidQuaternion(target))
            {
                return origin;
            }
            
            Quaternion targetOffset = target * Quaternion.Euler(offsetRot);
            
            if (!activeStabilizeRot)
                return targetOffset;
            
            (origin * Quaternion.Inverse(targetOffset)).ToAngleAxis(out float angle, out Vector3 axisV);
            float dif = 360 - angle;
            
            return Quaternion.Lerp(origin, targetOffset, interpolationSpeed * dif / rotSense);
        }
        
        private bool IsValidQuaternion(Quaternion q)
        {
            bool isNaN = float.IsNaN(q.x + q.y + q.z + q.w);
            bool isZero = q.x == 0 && q.y == 0 && q.z == 0 && q.w == 0;
            return !(isNaN || isZero);
        }
    }
}
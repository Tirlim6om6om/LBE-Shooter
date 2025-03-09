using UnityEngine;

namespace Tirlim.Player
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class PlayerTriggerMover : MonoBehaviour
    {
        [SerializeField] private Transform head;

        private CapsuleCollider _collider;

        private void Awake()
        {
            _collider = GetComponent<CapsuleCollider>();
        }

        private void Update()
        {
            Vector3 headPos = head.position;
            transform.position = new Vector3(headPos.x, headPos.y / 2, headPos.z);
            _collider.height = headPos.y;
        }
    }
}

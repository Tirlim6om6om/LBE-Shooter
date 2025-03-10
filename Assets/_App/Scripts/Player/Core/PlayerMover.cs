using System;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Tirlim.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Transform head;
        [SerializeField] private float speed;
        [SerializeField] private InputActionReference moveAction = null;

        private XROrigin _xrOrigin;

        [Inject]
        public void Construct(XROrigin origin)
        {
            _xrOrigin = origin;
        }

        private void OnEnable()
        {
            if (moveAction != null && moveAction.action != null)
            {
                moveAction.action.Enable();
            }
        }

        private void OnDisable()
        {
            if (moveAction != null && moveAction.action != null)
            {
                moveAction.action.Disable();
            }
        }

        void Update()
        {
            Move(moveAction.action.ReadValue<Vector2>());
            //Rotate(deviceStateL);
        }

        private void Move(Vector2 move)
        {
            Vector3 forward = head.forward * move.y;
            forward = new Vector3(forward.x, 0, forward.z);
            Vector3 right = head.right * move.x;
            right = new Vector3(right.x, 0, right.z);
            player.position += (forward + right).normalized * speed * Time.fixedDeltaTime;
            _xrOrigin.Origin.transform.position = player.position;
        }
    }
}

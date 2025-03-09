using System.Collections;
using UnityEngine;

namespace Tirlim.Gun
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float timeDestroy = 2;
        [SerializeField] private float speed = 5;
        private void OnEnable()
        {
            StartCoroutine(WaitDestroy());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator WaitDestroy()
        {
            yield return new WaitForSeconds(timeDestroy);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            transform.position += transform.forward * speed * Time.fixedDeltaTime;
        }
    }
}

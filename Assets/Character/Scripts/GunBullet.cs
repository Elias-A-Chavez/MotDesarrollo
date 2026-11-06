using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float lifetime = 3f;

    private void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
        }
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.isTrigger) return;

        Destroy(gameObject);
    }
}
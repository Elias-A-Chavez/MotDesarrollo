using UnityEngine;

public class Bullets : MonoBehaviour

{
    [SerializeField] private float speed = 30f;
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;

        rb.linearVelocity = transform.forward * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Aquí puedes añadir lógica de daño (ej: collision.gameObject.GetComponent<Enemy>())

        Destroy(gameObject);
    }
}
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 50f; // Asegúrate de que la velocidad sea alta
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Space.Self obliga a la bala a viajar hacia SU propio frente (eje azul Z positivo)
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Evita que la bala se rompa a sí misma si roza al jugador o disparadores
        if (other.CompareTag("Player") || other.isTrigger) return;

        Destroy(gameObject);
    }
}
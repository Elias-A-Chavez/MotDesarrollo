using UnityEngine;
using UnityEngine.InputSystem; 

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Camera mainCamera;

    [Header("Ajustes de Disparo")]
    [SerializeField] private float fireRate = 0.2f;
    private float nextFireTime;

    private void Update()
    {
        // Solución al Mismatch: Aseguramos el uso explícito del puntero del New Input System
        if (Pointer.current != null && Pointer.current.press.isPressed && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (mainCamera == null || bulletSpawn == null || bulletPrefab == null) return;

        Vector3 aimPoint = GetAimPoint();
        Vector3 direction = aimPoint - bulletSpawn.position;

        if (direction.sqrMagnitude < 0.001f) return;

        Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.LookRotation(direction));
    }

    private Vector3 GetAimPoint()
    {
        // Obtenemos la posición de la pantalla de forma segura con el New Input System
        Vector2 screenPosition = Pointer.current != null ? Pointer.current.position.ReadValue() : new Vector2(Screen.width / 2f, Screen.height / 2f);

        // Lanzamos el rayo usando la posición calculada
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }

        return ray.GetPoint(100f);
    }
}

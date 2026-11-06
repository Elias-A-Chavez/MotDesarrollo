using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float aimDistance = 500f;

    private float nextFireTime;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Pointer.current == null) return;
        if (!Pointer.current.press.isPressed) return;
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + fireRate;
        Shoot();
    }

    private void Shoot()
    {
        
        if (mainCamera == null || bulletSpawn == null || bulletPrefab == null)
        {
            return;
        }

        Vector3 aimPoint = GetAimPoint();
        Vector3 direction = aimPoint - bulletSpawn.position;

        if (direction.sqrMagnitude < 0.001f) return;

        
        
        Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.LookRotation(direction));
    }
    private Vector3 GetAimPoint()
    {
        Ray ray = mainCamera.ScreenPointToRay(
            new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f)
        );

        RaycastHit[] hits = Physics.RaycastAll(ray, aimDistance);
        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Player")) continue;
            if (hit.collider.transform.root == bulletSpawn.root) continue;

            return hit.point;
        }

        return ray.GetPoint(aimDistance);
    }
}


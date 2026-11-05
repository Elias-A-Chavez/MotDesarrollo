using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Detección directa del click usando el nuevo Input System
        if (Pointer.current != null && Pointer.current.press.isPressed && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (mainCamera == null || bulletSpawn == null || bulletPrefab == null) return;

        // SOLUCIÓN RADICAL: Ignoramos el modelo del arma y usamos estrictamente 
        // la rotación e inclinación exacta de la Cámara Principal.
        Quaternion cameraRotation = mainCamera.transform.rotation;

        // Clonamos la bala exactamente en la punta del arma, pero mirando hacia donde mira la cámara
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, cameraRotation);
    }
}


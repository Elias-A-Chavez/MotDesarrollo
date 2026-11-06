using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Input Settings (New Input System)")]
    [Tooltip("Enlaza aquí la acción de tipo 'Vector 2' que use 'Mouse/delta' o 'Gamepad/rightStick'")]
    public InputActionProperty lookAction;

    [Header("Camera Settings")]
    public float distance = 5f;
    public float height = 2f;
    public float mouseSensitivity = 0.1f; // Ajustada por defecto ya que los valores Delta del nuevo sistema son diferentes
    public float smoothSpeed = 10f;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    [Header("Collision Settings")]
    public float cameraCollisionOffset = 0.2f;

    [Tooltip("Layers que pueden bloquear la cámara (excluye 'Player' e 'Ignore Raycast')")]
    public LayerMask collisionLayers = ~0;

    private float yaw;
    private float pitch;

    void OnEnable()
    {
        // Asegura que la acción esté habilitada para escuchar inputs
        lookAction.action?.Enable();
    }

    void OnDisable()
    {
        lookAction.action?.Disable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yaw = player.eulerAngles.y;
        pitch = 25f;

        Quaternion initialRotation = Quaternion.Euler(pitch, yaw, 0f);
        transform.rotation = initialRotation;

        Vector3 initialPosition = player.position
                                - (initialRotation * Vector3.forward * distance)
                                + Vector3.up * height;

        transform.position = initialPosition;

        int playerLayer = LayerMask.NameToLayer("Player");
        int ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
        collisionLayers &= ~(1 << playerLayer);
        collisionLayers &= ~(1 << ignoreRaycastLayer);
    }

    void LateUpdate()
    {
        // Leer el valor del vector delta utilizando el nuevo Input System
        Vector2 lookInput = lookAction.action != null ? lookAction.action.ReadValue<Vector2>() : Vector2.zero;

        // El nuevo input delta no requiere multiplicarse por Time.deltaTime ya que mide el movimiento por frame físico/de renderizado
        yaw += lookInput.x * mouseSensitivity;
        pitch -= lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetDirection = targetRotation * Vector3.back;

        Vector3 targetPosition = player.position + Vector3.up * height;
        Vector3 desiredCameraPosition = targetPosition + targetDirection * distance;

        RaycastHit hit;
        if (Physics.Raycast(targetPosition, targetDirection, out hit, distance, collisionLayers, QueryTriggerInteraction.Ignore))
        {
            float adjustedDistance = hit.distance - cameraCollisionOffset;
            adjustedDistance = Mathf.Clamp(adjustedDistance, 0.5f, distance);
            desiredCameraPosition = targetPosition + targetDirection * adjustedDistance;
        }

        transform.position = Vector3.Lerp(transform.position, desiredCameraPosition, smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);

        player.rotation = Quaternion.Euler(0f, yaw, 0f);
    }

    public void SnapCameraToPlayer()
    {
        yaw = player.eulerAngles.y;

        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetDirection = targetRotation * Vector3.back;

        Vector3 targetPosition = player.position + Vector3.up * height;
        Vector3 desiredCameraPosition = targetPosition + targetDirection * distance;

        RaycastHit hit;
        if (Physics.Raycast(targetPosition, targetDirection, out hit, distance, collisionLayers, QueryTriggerInteraction.Ignore))
        {
            float adjustedDistance = hit.distance - cameraCollisionOffset;
            adjustedDistance = Mathf.Clamp(adjustedDistance, 0.5f, distance);
            desiredCameraPosition = targetPosition + targetDirection * adjustedDistance;
        }

        transform.position = desiredCameraPosition;
        transform.rotation = targetRotation;

        player.rotation = Quaternion.Euler(0f, yaw, 0f);
    }
}
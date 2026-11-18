using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private InputActionProperty lookAction;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float height = 2f;
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float smoothSpeed = 10f;
    [SerializeField] private Vector2 pitchMinMax = new Vector2(-40, 85);
    [SerializeField] private float cameraCollisionOffset = 0.2f;
    [SerializeField] private LayerMask collisionLayers = ~0;

    private float yaw;
    private float pitch;

    private void OnEnable()
    {
        lookAction.action?.Enable();
    }

    private void OnDisable()
    {
        lookAction.action?.Disable();
    }

    private void Start()
    {
        if (player == null)
        {
            return;
        }

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

    private void LateUpdate()
    {
        if (player == null)
        {
            return;
        }

        Vector2 lookInput = lookAction.action != null ? lookAction.action.ReadValue<Vector2>() : Vector2.zero;

        yaw += lookInput.x * mouseSensitivity;
        pitch -= lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetDirection = targetRotation * Vector3.back;

        Vector3 targetPosition = player.position + Vector3.up * height;
        Vector3 desiredCameraPosition = targetPosition + targetDirection * distance;

        if (Physics.Raycast(targetPosition, targetDirection, out RaycastHit hit, distance, collisionLayers, QueryTriggerInteraction.Ignore))
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
        if (player == null)
        {
            return;
        }

        yaw = player.eulerAngles.y;

        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetDirection = targetRotation * Vector3.back;

        Vector3 targetPosition = player.position + Vector3.up * height;
        Vector3 desiredCameraPosition = targetPosition + targetDirection * distance;

        if (Physics.Raycast(targetPosition, targetDirection, out RaycastHit hit, distance, collisionLayers, QueryTriggerInteraction.Ignore))
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

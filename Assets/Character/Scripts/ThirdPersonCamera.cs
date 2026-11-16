using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
  
    public Transform player;
    public InputActionProperty lookAction;
    public float distance = 5f;
    public float height = 2f;
    public float mouseSensitivity = 0.1f; 
    public float smoothSpeed = 10f;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float cameraCollisionOffset = 0.2f;
    public LayerMask collisionLayers = ~0;

    private float yaw;
    private float pitch;

    void OnEnable()
    {
        
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
        
        Vector2 lookInput = lookAction.action != null ? lookAction.action.ReadValue<Vector2>() : Vector2.zero;

        
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
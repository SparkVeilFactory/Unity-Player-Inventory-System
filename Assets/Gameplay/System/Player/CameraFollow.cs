using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Rotation")]
    public float mouseSensitivity = 3f;
    public float minYAngle = -20f;
    public float maxYAngle = 60f;

    [Header("Zoom")]
    public float distance = 5f;
    public float minDistance = 2f;
    public float maxDistance = 8f;
    public float zoomSpeed = 2f;

    [Header("Offset")]
    public float heightOffset = 1.5f;

    private float currentX;
    private float currentY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 🔥 STOP kamera kad je inventory otvoren
        if (InventoryToggle.IsOpen)
        {
            return;
        }

        // ROTACIJA
        currentX += Input.GetAxis("Mouse X") * mouseSensitivity;
        currentY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);

        // ZOOM
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // POZICIJA
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 targetPosition = target.position + Vector3.up * heightOffset;
        Vector3 cameraPosition = targetPosition - rotation * Vector3.forward * distance;

        transform.position = cameraPosition;
        transform.LookAt(targetPosition);
    }
}
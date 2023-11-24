using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign this to your player transform in the inspector
    public float smoothSpeed = 0.125f; // Adjust this to change the smoothing speed
    public Vector3 offset; // Adjust this for a specific offset if needed

    public float zoomSpeed = 4f; // Speed of the zoom
    public float minZoom = 5f; // Min zoom level
    public float maxZoom = 15f; // Max zoom level

    public Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);

            // Camera zooming with mouse wheel
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (cam.orthographic)
            {
                // For orthographic camera
                cam.orthographicSize -= scroll * zoomSpeed;
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
            }
            else
            {
                // For perspective camera
                cam.fieldOfView -= scroll * zoomSpeed;
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
            }
        }
    }
}

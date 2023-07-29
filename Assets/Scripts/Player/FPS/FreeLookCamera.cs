using UnityEngine;

public class FreeLookCamera : MonoBehaviour
{
    // Set the initial rotation of the camera
    private Vector2 cameraRot = Vector2.zero;
    public Vector2 CameraRot => cameraRot;

    // Set the rotation speed
    public float rotSpeed = 0.5f;

    // Set the maximum and minimum rotation angles along the X and Y axes
    public float maxYRot = 90f;
    public float minYRot = -90f;
    public float maxXRot = 45f;
    public float minXRot = -45f;

    // Set the mouse sensitivity
    public float mouseSensitivity = 0.1f;

    // Update is called once per frame
    void Update()
    {
        // Get the current state of the mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Update the camera rotation based on the mouse movement
        cameraRot.x -= mouseY;
        cameraRot.y += mouseX;

        // Clamp the camera rotation angles to the maximum and minimum values
        cameraRot.x = Mathf.Clamp(cameraRot.x, minYRot, maxYRot);
        cameraRot.y = Mathf.Clamp(cameraRot.y, minXRot, maxXRot);

        // Calculate the new rotation for the camera
        Quaternion newRot = Quaternion.Euler(cameraRot.x, cameraRot.y, 0f);

        // Apply the new rotation to the camera
        transform.rotation = newRot;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    // Reference to the player's transform.
    public Transform player;

    [Space(10)]
    // Sensitivity of the mouse movement.
    public float mouseSensitivity = 100f;

    // Rotation around the X-axis.
    private float xRotation;

    void Start()
    {
        // Lock the cursor at the center of the screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get the mouse input for X and Y axes.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
    
        // Adjust the vertical rotation based on mouse input.
        xRotation += mouseY * Time.deltaTime;

        // Clamp the xRotation within a specified range.
        xRotation = Mathf.Clamp(xRotation, -65, 80);

        // Apply the rotation to the camera's local rotation around the X-axis.
        transform.localRotation = Quaternion.Euler(-xRotation, 0, 0);

        // Rotate the player horizontally based on mouse input.
        player.Rotate(Vector3.up * mouseX * Time.deltaTime);
    }
}

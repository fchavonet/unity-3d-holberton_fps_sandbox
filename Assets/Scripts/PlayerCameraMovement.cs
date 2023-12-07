using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraMovement : MonoBehaviour
{
    // Reference to the player's transform.
    public Transform player;

    [Space(10)]
    // Sensitivity for mouse and gamepad input
    public float mouseSensitivity = 15f;
    public float gamepadSensitivity = 150f;

    // Variables to store mouse and gamepad input values
    private float mouseX = 0;
    private float mouseY = 0;
    private float gamepadX = 0;
    private float gamepadY = 0;

    // Rotation around the X-axis.
    private float xRotation;

    void Start()
    {
        // Lock the cursor at the center of the screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Read mouse input values if a mouse is connected
        if (Mouse.current != null)
        {
            mouseX = Mouse.current.delta.ReadValue().x * mouseSensitivity;;
            mouseY = Mouse.current.delta.ReadValue().y * mouseSensitivity;;
        }

        // Read gamepad input values if a gamepad is connected
        if (Gamepad.current != null)
        {
            gamepadX = Gamepad.current.rightStick.ReadValue().x * gamepadSensitivity;;
            gamepadY = Gamepad.current.rightStick.ReadValue().y * gamepadSensitivity;;
        }

        // Update the xRotation based on mouse and gamepad input.
        xRotation += mouseY * Time.deltaTime;
        xRotation += gamepadY * Time.deltaTime;

        // Clamp the xRotation within a specified range.
        xRotation = Mathf.Clamp(xRotation, -65, 80);

        // Apply the rotation to the camera's local rotation around the X-axis.
        transform.localRotation = Quaternion.Euler(-xRotation, 0, 0);

        // Rotate the player around the y-axis based on mouse and gamepad input
        player.Rotate(Vector3.up * mouseX * Time.deltaTime);
        player.Rotate(Vector3.up * gamepadX * Time.deltaTime);
    }
}

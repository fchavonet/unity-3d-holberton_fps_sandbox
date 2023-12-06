using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Reference to the CharacterController component.
    public CharacterController characterController;

    [Space(10)]
    // Movement speed, gravity, and jump height variables.
    public float speed = 10f;
    public float gravity = -10f;
    public float jumpHeight = 2;

    [Space(10)]
    // Ground check parameters.
    public Transform groundCheck;
    public LayerMask groundLayer;

    // Vector to store movement input.
    private Vector3 move;
    // Vector to store player velocity.
    private Vector3 velocity;

    // Boolean checker for grounded state.
    private bool isGrounded;

    void Start()
    {
        
    }

    void Update()
    {
        // Read horizontal and vertical input values.
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate the movement vector.
        move = transform.right * x + transform.forward * z;

        // Move the player using CharacterController.
        characterController.Move(move * speed * Time.deltaTime);

        // Check if the player is grounded.
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundLayer);

        // Reset vertical velocity if the player is grounded.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }    

        if (isGrounded)
        {
            // Check for jump input when grounded.
            if (Input.GetButtonDown("Jump"))
            {
                Jump(); // Call the Jump function when the jump button is pressed.
            }
        }
        else
        {
            // Apply gravity when the player is in the air.
            velocity.y += gravity * Time.deltaTime;
        }

        // Move the player vertically based on velocity.
        characterController.Move(velocity * Time.deltaTime);
    }

    // Function to handle player jumping.
    private void Jump()
    {
        // Calculate the vertical velocity for jumping.
        velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
    }
}

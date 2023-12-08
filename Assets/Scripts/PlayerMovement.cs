using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Reference to the CharacterController component
    public CharacterController characterController;

    [Space(10)]
    // Reference to the player's body GameObject 
    public GameObject playerBody;

    [Space(10)]
    // Movement speed, gravity, and jump height variables
    public float speed = 10f;
    public float gravity = -15f;
    public float jumpHeight = 2;
    public float crouchHeight = -1f;

    [Space(10)]
    // Ground check parameters
    public Transform groundCheck;
    public LayerMask groundLayer;

    // Vector to store movement input
    private Vector3 move;
    // Vector to store player velocity
    private Vector3 velocity;
    // Vector to store local position of the player's body
    private Vector3 normalLocalPosition;

    // Initial height of the player when not crouching
    private float normalHeight;

    // Boolean checkers
    private bool isGrounded = true;
    private bool isRunning = false;
    private bool isJumping = false;
    private bool isCrouching = false;

    // Input actions for movement, run, jump and crouch
    InputAction movement;
    InputAction run;
    InputAction jump;
    InputAction crouch;

    void Start()
    {
        // Store the initial local position of the player's body
        normalLocalPosition = playerBody.transform.localPosition;

        // Set up movement input with keyboard and gamepad bindings
        movement = new InputAction("PlayerMovement", binding: "<Gamepad>/leftStick");
        movement.AddCompositeBinding("Dpad")
            .With("Up", "<keyboard>/w")
            .With("Up", "<keyboard>/upArrow")
            .With("Down", "<keyboard>/s")
            .With("Down", "<keyboard>/downArrow")
            .With("Left", "<keyboard>/a")
            .With("Left", "<keyboard>/leftArrow")
            .With("Right", "<keyboard>/d")
            .With("Right", "<keyboard>/RightArrow");

        // Set up run input with keyboard and gamepad bindings
        run = new InputAction("Run", binding: "<keyboard>/shift");
        run.AddBinding("<Gamepad>/leftStickPress");

        // Set up jump input with keyboard and gamepad bindings
        jump = new InputAction("Jump", binding: "<keyboard>/space");
        jump.AddBinding("<Gamepad>/a");

        // Set up run input with keyboard and gamepad bindings
        crouch = new InputAction("Crouch", binding: "<keyboard>/leftCtrl");
        crouch.AddBinding("<Gamepad>/rightStickPress");

        // Enable input actions
        movement.Enable();
        run.Enable();
        jump.Enable();
        crouch.Enable();
    }

    void Update()
    {
        // Read horizontal and vertical input values
        float x = movement.ReadValue<Vector2>().x;
        float z = movement.ReadValue<Vector2>().y;

        // Calculate the movement vector
        move = transform.right * x + transform.forward * z;

        // Move the player using CharacterController
        characterController.Move(move * speed * Time.deltaTime);

        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundLayer);

        // Reset vertical velocity if the player is grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }    

        // Handle running and jumping when grounded
        if (isGrounded)
        {
            // Check if the player is running
            isRunning = Mathf.Approximately(run.ReadValue<float>(), 1);

            // Adjust player speed based on running
            if (isRunning)
            {
                speed = 15f;
            }
            else
            {
                speed = 10f;
            }

            // Check if the player is jumping
            isJumping = Mathf.Approximately(jump.ReadValue<float>(), 1);

            // Handle jumping input
            if (isJumping)
            {
                Jump();
            }
        }
        else
        {
            // Apply gravity when the player is in the air
            velocity.y += gravity * Time.deltaTime;
        }

        // Handle crouching input
        if (crouch.triggered)
            {
                Crouch();
            }

        // Move the player vertically based on velocity
        characterController.Move(velocity * Time.deltaTime);
    }

    // Function to handle player jumping
    private void Jump()
    {
        // Calculate the vertical velocity for jumping
        velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
    }

    // Function to handle player crouching
    private void Crouch()
    {
        // Toggle crouch state
        isCrouching = !isCrouching;

        // Adjust the character controller height based on crouch state
        if (isCrouching)
        {
            // Calculate the new local Y position for the player's body when crouching
            float newLocalY = normalLocalPosition.y - (normalLocalPosition.y - crouchHeight);

            // Set the new local position for the player's body
            playerBody.transform.localPosition = new Vector3(normalLocalPosition.x, newLocalY, normalLocalPosition.z);
        }
        else
        {
            // Reset the local position for the player's body to its normal position
            playerBody.transform.localPosition = normalLocalPosition;
        }
    }
}

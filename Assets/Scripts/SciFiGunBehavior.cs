using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SciFiGunBehavior : MonoBehaviour
{
    // Reference to the player's camera
    public Transform PlayerCamera;

    [Space(10)]
    // Particle system and impact effect objects
    public ParticleSystem muzzleFlush;
    public GameObject impactEffect;
    public GameObject impactGlass;

    [Space(10)]
    // Bullet parameters
    public float bulletRange = 100f;
    public float bulletForce = 1500f;
    public int bulletRate = 5;

    [Space(10)]
    // Ball objects
    public GameObject ballPoint;
    public GameObject ballPrefab;

    [Space(10)]
    // Ball parameters
    public float ballForce = 1500f;
    public int ballRate = 5;

    // Variables to control the firing rate and track impact layer
    private float nextTimeToFireBullet = 0;
    private float nextTimeToFireBall = 0;
    private int impactLayer = 0;

    // Boolean checkers
    private bool isShootingBullet = false;
    private bool isShootingBall = false;

    // Input action for shooting
    InputAction shootBullet;
    InputAction shootBall;

    void Start()
    {
        // Set up shoot bullet input with mouse and gamepad bindings
        shootBullet = new InputAction("ShootBullet", binding: "<mouse>/leftButton");
        shootBullet.AddBinding("<Gamepad>/rightTrigger");

        // Set up shoot ball input with mouse and gamepad bindings
        shootBall = new InputAction("ShootBall", binding: "<mouse>/rightButton");
        shootBall.AddBinding("<Gamepad>/leftTrigger");

        // Enable the shooting input action
        shootBullet.Enable();
        shootBall.Enable();
    }

    void Update()
    {
        // Check if the shooting bullet input is triggered
        isShootingBullet = shootBullet.ReadValue<float>() == 1;

        // Fire the gun if shooting and enough time has passed since the last shot
        if(isShootingBullet && Time.time >= nextTimeToFireBullet)
        {
            nextTimeToFireBullet = Time.time + 1f / bulletRate;
            FireBullet();
        }

        // Check if the shooting ball input is triggered
        isShootingBall = shootBall.triggered;

        // Fire the ball if shooting and enough time has passed since the last shot
        if (isShootingBall && Time.time >= nextTimeToFireBall)
        {
            nextTimeToFireBall = Time.time +1f / ballRate;
            FireBall();
        }
    }

    private void FireBullet()
    {
        // Play bullet shooting sound
        AudioManager.instance.Play("ShootBullet");

        // Raycast to detect hits along the player's camera forward direction
        RaycastHit hit;

        // Play muzzle flash particle effect
        muzzleFlush.Play();

        // Cast a ray from the player's camera to detect hits
        if (Physics.Raycast(PlayerCamera.position, PlayerCamera.forward, out hit, bulletRange))
        {
            if (hit.transform != null)
            {
                // Apply force to the hit object if it has a rigidbody
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * bulletForce);
                }

                // Identify the layer of the impacted object
                impactLayer = hit.transform.gameObject.layer;

                // Check if the impact layer is suitable for impact effects
                if (impactLayer == LayerMask.NameToLayer("Impact") || impactLayer == LayerMask.NameToLayer("Ground"))
                {
                    // Instantiate impact effect with rotation based on hit normal
                    Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
                    GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
                    impact.transform.parent = hit.transform;

                    // Destroy the impact effect after a certain time
                    Destroy(impact, 10);
                }

                // Check if the impact layer corresponds to glass surfaces
                if (impactLayer == LayerMask.NameToLayer("Glass"))
                {
                    // Instantiate glass impact effect with rotation adjustment
                    Quaternion impactRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    GameObject impact = Instantiate(impactGlass, hit.point + (hit.normal * .01f), impactRotation);
                    impact.transform.parent = hit.transform;

                    // Destroy the impact effect after a certain time
                    Destroy(impact, 10);
                }
            }
        }
    }

    private void FireBall()
    {
        // Play ball shooting sound
        AudioManager.instance.Play("ShootBall");

        // Raycast to detect hits along the player's camera forward direction
        RaycastHit hit;

        // Cast a ray from the player's camera to detect hits
        if (Physics.Raycast(PlayerCamera.position, PlayerCamera.forward, out hit, bulletRange))
        {
            // Calculate the direction towards the hit point
            Vector3 shootDirection = (hit.point - ballPoint.transform.position).normalized;

            // Instantiate and shoot the ball towards the hit point
            GameObject ball = Instantiate(ballPrefab, ballPoint.transform.position, Quaternion.LookRotation(shootDirection));
            ball.GetComponent<Rigidbody>().AddForce(shootDirection * ballForce);

            // Destroy the ball after a certain time
            Destroy(ball, 30);
        }
        else
        {
            // If no hit, shoot the ball straight ahead
            GameObject ball = Instantiate(ballPrefab, ballPoint.transform.position, transform.rotation);
            ball.GetComponent<Rigidbody>().AddForce(transform.forward * ballForce);

            // Destroy the ball after a certain time
            Destroy(ball, 30);
        }
    }
}

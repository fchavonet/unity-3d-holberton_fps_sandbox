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

    [Space(10)]
    //
    public float bulletRange = 100f;
    public float bulletForce = 150f;
    public int bulletRate = 5;

    [Space(10)]
    //
    public GameObject ballPoint;
    public GameObject ballPrefab;

    [Space(10)]
    //
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
        // Set up shoot input with mouse and gamepad bindings
        shootBullet = new InputAction("ShootBullet", binding: "<mouse>/leftButton");
        shootBullet.AddBinding("<Gamepad>/rightTrigger");

        //
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
            nextTimeToFireBullet = Time.time + 1f / ballRate;
            FireBullet();
        }

        // Check if the shooting ball input is triggered
        isShootingBall = shootBall.triggered;

        //
        if (isShootingBall && Time.time >= nextTimeToFireBall)
        {
            nextTimeToFireBall = Time.time +1f / ballRate;
            FireBall();
        }
    }

    private void FireBullet()
    {
        //
        RaycastHit hit;

        //
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
                    Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
                    GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
                    impact.transform.parent = hit.transform;

                    // Destroy the impact effect after a certain time
                    Destroy(impact, 60);
                }
            }
        }
    }

    private void FireBall()
    {
        //
        GameObject ball = Instantiate(ballPrefab, ballPoint.transform.position, transform.rotation);
        ball.GetComponent<Rigidbody>().AddForce(transform.forward * ballForce);

        //
        Destroy(ball, 30);
    }
}

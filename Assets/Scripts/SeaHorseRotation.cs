using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaHorseRotation : MonoBehaviour
{
    // Rotation speed of the SeaHorse around the Y-axis
    public float rotationSpeed = 5f;

    void Update()
    {
        // Rotate the Sea Horse around the Y-axis at the specified speed
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

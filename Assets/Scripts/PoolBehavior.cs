using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBehavior : MonoBehaviour
{
    // Called when the Collider of this object enters another Collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is on the "PoolHoles" layer.
        if (other.gameObject.layer == LayerMask.NameToLayer("PoolHoles"))
        {
            // If the entering object has the correct layer, destroy this object
            Destroy(gameObjet);
        }
    }
}

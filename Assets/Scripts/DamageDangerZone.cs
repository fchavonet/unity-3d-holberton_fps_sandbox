using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDangerZone : MonoBehaviour
{
    // Called when another collider exits the trigger zone
    void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Set the playerInDangerZone variable in DamageBehavior to true
            DamageBehavior.playerInDangerZone = true;
        }
    }

    // Called when another collider exits the trigger zone
    void OnTriggerExit(Collider other)
    {
        // Check if the entering collider has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Set the playerInDangerZone variable in DamageBehavior to false
            DamageBehavior.playerInDangerZone = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticleSpawn : MonoBehaviour
{
    public GameObject particleEffectPrefab; // Assign the particle effect prefab in the Inspector

    // Called when another collider enters the trigger collider attached to this GameObject
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the GameObject entering the trigger has the tag "Fish"
        if (other.CompareTag("food"))
        {
            // Instantiate the particle effect at the collision point
            Instantiate(particleEffectPrefab, other.transform.position, Quaternion.identity);
        }
    }
}
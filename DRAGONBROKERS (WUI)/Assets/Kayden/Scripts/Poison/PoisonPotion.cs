using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPotion : MonoBehaviour
{
    public GameObject poisonSplash;
    public GameObject poisonAfterEffect;

    // This method is called when the potion's trigger collider intersects with another collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the tag "PoisonWater"
        if (other.CompareTag("PoisonWater"))
        {
            Debug.Log("Potion triggered with PoisonWater");

            // Instantiate the poison splash and after effect at the potion's position
            Instantiate(poisonAfterEffect, transform.position, Quaternion.identity);
            Instantiate(poisonSplash, transform.position, Quaternion.identity);

            // Destroy the potion GameObject
            Destroy(gameObject);
        }
    }
}

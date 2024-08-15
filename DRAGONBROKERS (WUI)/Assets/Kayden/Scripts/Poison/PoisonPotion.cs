using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPotion : MonoBehaviour
{
    public GameObject poisonSplash;
    public GameObject poisonAfterEffect;
    public float slowDownFactor = 0.8f;
    public float slowDownDuration = 0.2f;
    public float explosionRange = 2f; // Radius of the explosion range

    private bool isInPoisonWater = false;
    private float slowDownTime;

    // This method is called when the potion's trigger collider enters another collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PoisonWater"))
        {
            Debug.Log("Potion entered PoisonWater");

            Instantiate(poisonAfterEffect, transform.position, Quaternion.identity);
            Instantiate(poisonSplash, transform.position, Quaternion.identity);

            isInPoisonWater = true;
            slowDownTime = Time.time;
        }
    }

    // This method is called when the potion's trigger collider exits another collider
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PoisonWater"))
        {
            Debug.Log("Potion exited PoisonWater");
            isInPoisonWater = false;
        }
    }

    // This method is called every frame
    void Update()
    {
        if (isInPoisonWater)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Gradually slow down over time
                float timeSinceSlowDownStarted = Time.time - slowDownTime;
                if (timeSinceSlowDownStarted < slowDownDuration)
                {
                    rb.velocity *= Mathf.Lerp(1, slowDownFactor, timeSinceSlowDownStarted / slowDownDuration);
                    rb.angularVelocity *= Mathf.Lerp(1, slowDownFactor, timeSinceSlowDownStarted / slowDownDuration);
                }
                else
                {
                    // Maintain the final reduced velocity
                    rb.velocity *= slowDownFactor;
                    rb.angularVelocity *= slowDownFactor;
                }
            }

            // Check for fish in the explosion range every frame
            CheckForFishInRange();
        }
    }

    // Check for any fish within the explosion range
    void CheckForFishInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRange);
        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Collider found with tag: " + collider.tag);

            if (collider.CompareTag("fish"))
            {
                Debug.Log("Fish in range of explosion, destroying poison potion");
                Destroy(gameObject);
                break;
            }
        }
    }

    // This method is called when the potion collides with another object (for additional debugging)
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with: " + collision.collider.name + " with tag: " + collision.collider.tag);
    }

    // Draw the explosion range in the Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPotion : MonoBehaviour
{
    public GameObject poisonSplash;
    public GameObject poisonAfterEffect;
    public GameObject smokePrefab; // Reference to the Smoke particle system prefab
    public GameObject cloudsPrefab; // Reference to the Clouds particle system prefab
    public GameObject poisonWaterCloudPrefab; // Reference to the PoisonWaterCloud particle system prefab
    public Vector3 poisonWaterCloudPosition; // Preset position for the PoisonWaterCloud
    public float slowDownFactor = 0.8f;
    public float slowDownDuration = 0.2f;
    public float explosionRange = 2f; // Radius of the explosion range
    public float presetYPosition = 0f; // Preset Y-axis position for particle effects

    private bool isInPoisonWater = false;
    private bool cloudsPlayed = false; // To ensure the Clouds particle system plays only once
    private float slowDownTime;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PoisonWater"))
        {
            Debug.Log("Potion entered PoisonWater");

            // Set the spawn position using the potion's X-axis and the preset Y-axis
            Vector3 spawnPosition = new Vector3(transform.position.x, presetYPosition, transform.position.z);

            Instantiate(poisonAfterEffect, spawnPosition, Quaternion.identity);
            Instantiate(poisonSplash, spawnPosition, Quaternion.identity);

            // Play the Clouds particle effect at the same position
            if (!cloudsPlayed)
            {
                Instantiate(cloudsPrefab, spawnPosition, Quaternion.identity);
                cloudsPlayed = true;
            }

            isInPoisonWater = true;
            slowDownTime = Time.time;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PoisonWater"))
        {
            Debug.Log("Potion exited PoisonWater");
            isInPoisonWater = false;
        }
    }

    void Update()
    {
        if (isInPoisonWater)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float timeSinceSlowDownStarted = Time.time - slowDownTime;
                if (timeSinceSlowDownStarted < slowDownDuration)
                {
                    rb.velocity *= Mathf.Lerp(1, slowDownFactor, timeSinceSlowDownStarted / slowDownDuration);
                    rb.angularVelocity *= Mathf.Lerp(1, slowDownFactor, timeSinceSlowDownStarted / slowDownDuration);
                }
                else
                {
                    rb.velocity *= slowDownFactor;
                    rb.angularVelocity *= slowDownFactor;
                }
            }

            CheckForFishInRange();
        }
    }

    void CheckForFishInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRange);
        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Collider found with tag: " + collider.tag);

            if (collider.CompareTag("fish"))
            {
                Debug.Log("Fish in range of explosion, stopping fish movement and destroying poison potion");

                // Stop the fish's movement and start moving it upwards
                TestFishMovement fishMovement = collider.GetComponent<TestFishMovement>();
                if (fishMovement != null)
                {
                    fishMovement.StopMovement();
                    fishMovement.ChangeColor(Color.green); // Change fish color to green
                }

                // Play the Smoke particle effect before destroying the potion
                Instantiate(smokePrefab, transform.position, Quaternion.identity);

                // Instantiate the PoisonWaterCloud at the preset position
                Instantiate(poisonWaterCloudPrefab, poisonWaterCloudPosition, Quaternion.identity);

                Destroy(gameObject);
                break;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with: " + collision.collider.name + " with tag: " + collision.collider.tag);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}

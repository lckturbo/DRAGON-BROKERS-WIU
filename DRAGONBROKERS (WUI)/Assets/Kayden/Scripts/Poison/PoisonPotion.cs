using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPotion : MonoBehaviour
{
    public GameObject poisonSplash;
    public GameObject poisonAfterEffect;
    public GameObject smokePrefab;
    public GameObject cloudsPrefab;
    public GameObject poisonWaterCloudPrefab;
    public Vector3 poisonWaterCloudPosition;
    public float slowDownFactor = 0.8f;
    public float slowDownDuration = 0.2f;
    public float explosionRange = 2f;
    public float presetYPosition = 0f;

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
        bool fishFound = false; // Flag to check if any fish are in range

        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Collider found with tag: " + collider.tag);

            if (collider.CompareTag("fish"))
            {
                Debug.Log("Fish in range of explosion, stopping fish movement and changing color");

                // Stop the fish's movement and start moving it upwards
                TestFishMovement fishMovement = collider.GetComponent<TestFishMovement>();
                if (fishMovement != null)
                {
                    fishMovement.StopMovement();
                    fishMovement.ChangeColor(Color.green); // Change fish color to green
                }

                fishFound = true; // Set the flag to true if at least one fish is found
            }
        }

        if (fishFound)
        {
            // Play the Smoke particle effect before destroying the potion
            Instantiate(smokePrefab, transform.position, Quaternion.identity);

            // Instantiate the PoisonWaterCloud at the preset position
            Instantiate(poisonWaterCloudPrefab, poisonWaterCloudPosition, Quaternion.identity);

            Destroy(gameObject);
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
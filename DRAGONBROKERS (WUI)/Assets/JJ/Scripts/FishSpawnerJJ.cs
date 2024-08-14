using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnerJJ : MonoBehaviour
{
    public FishingProbability fishingProbability; // Reference to the FishingProbability script

    public GameObject fishPrefab; // Single fish prefab for all fish types

    private void Start()
    {
        if (fishingProbability == null)
        {
            Debug.LogError("FishingProbability reference is not set!");
            return;
        }

        // Spawn fish periodically
        InvokeRepeating("SpawnFish", 2.0f, 5.0f); // Adjust timing as needed
    }

    private void SpawnFish()
    {
        // Determine the fish type to spawn based on the probabilities
        string fishType = fishingProbability.FishingChance();

        if (fishPrefab != null)
        {
            // Determine spawn position (left or right side of the screen)
            float screenHeight = Camera.main.orthographicSize * 2;
            float screenWidth = screenHeight * Camera.main.aspect;

            Vector3 spawnPosition;

            if (Random.value < 0.5f)
            {
                // Spawn on the left, move to the right
                spawnPosition = new Vector3(-screenWidth / 2 - 1, Random.Range(-screenHeight / 2, screenHeight / 2), 0);
            }
            else
            {
                // Spawn on the right, move to the left
                spawnPosition = new Vector3(screenWidth / 2 + 1, Random.Range(-screenHeight / 2, screenHeight / 2), 0);
            }

            GameObject fish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
            FishFSM fishFSM = fish.GetComponent<FishFSM>();
            fishFSM.SetFishType(fishType); // Set the type for behavior

            InteractFish interactFish = fish.GetComponent<InteractFish>();
            interactFish.SetFishType(fishType); // Set the type for interaction
        }
    }
}

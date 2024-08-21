using UnityEngine;
using System.Collections;

public class FoodManager : MonoBehaviour
{
    public GameObject foodPrefab;  // Drag your food prefab here in the Inspector
    public float minX = -5f;       // Minimum X position for spawning food
    public float maxX = 5f;        // Maximum X position for spawning food
    public float spawnY = 0f;      // Fixed Y position for spawning food
    public int maxFoodCount = 3;   // Number of food items to spawn
    public float cooldownDuration = 10f; // Cooldown duration in seconds

    private bool isOnCooldown = false;

    private FishFoodManager fishFoodManager;

    private void Start()
    {
        fishFoodManager = GameObject.FindObjectOfType<FishFoodManager>();
    }

    public void DropFood()
    {
        if (!isOnCooldown)
        {
            // Check if the player has enough food
            if (fishFoodManager.foodCount >= 3)
            {
                StartCoroutine(SpawnFoodRoutine());
            }
            else
            {
                Debug.Log("Not enough food to drop. You need at least 3 food items.");
            }
        }
        else
        {
            Debug.Log("Drop food is on cooldown.");
        }
    }

    private IEnumerator SpawnFoodRoutine()
    {
        for (int i = 0; i < maxFoodCount; i++)
        {
            float randomX = Random.Range(minX, maxX);
            Vector2 spawnPosition = new Vector2(randomX, spawnY);
            Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(0f); // delay between spawning each food item
        }

        // Decrease food count by 3 after dropping food
        fishFoodManager.foodCount -= 3;

        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        isOnCooldown = false;
    }
}

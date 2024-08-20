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

    // This method will be called by the button's onClick event
    public void DropFood()
    {
        if (!isOnCooldown)
        {
            StartCoroutine(SpawnFoodRoutine());
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

        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        isOnCooldown = false;
    }
}

using UnityEngine;

public class DestructablePlants : MonoBehaviour
{
    // Static counter variable to keep track of destroyed plants
    public static int destroyedPlantCount = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged "TrawlerArm"
        if (collision.gameObject.CompareTag("TrawlerArm"))
        {
            // Increment the counter
            destroyedPlantCount++;

            // Log a message to the console, including the count of destroyed plants
            Debug.Log("FUCK YOU: " + destroyedPlantCount);

            // Destroy the GameObject
            Destroy(gameObject);
        }
    }
}

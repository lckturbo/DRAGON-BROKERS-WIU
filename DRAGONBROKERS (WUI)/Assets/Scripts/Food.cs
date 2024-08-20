using UnityEngine;

public class Food : MonoBehaviour
{
    private float lifespan = 8f; // Time in seconds before the food despawns

    private void Start()
    {
        // Schedule the food to be destroyed after the lifespan
        Invoke("DespawnFood", lifespan);
    }

    private void DespawnFood()
    {
        // Destroy the food object after it has not been eaten within 8 seconds
        Destroy(gameObject);
    }
}

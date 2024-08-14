using UnityEngine;

public class DestructablePlants : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged "TrawlerArm"
        if (collision.gameObject.CompareTag("TrawlerArm"))
        {
            // Log a message to the console
            Debug.Log("coral destroyed");

            // Destroy the GameObject
            Destroy(gameObject);
        }
    }
}

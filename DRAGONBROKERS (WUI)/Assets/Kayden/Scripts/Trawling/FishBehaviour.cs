using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the surface
        if (collision.gameObject.CompareTag("FishingSurface")) // Make sure your surface GameObject has the tag "Surface"
        {
            Debug.Log("Caught fish");
            Destroy(gameObject); // Destroy the fish GameObject
        }
    }
}

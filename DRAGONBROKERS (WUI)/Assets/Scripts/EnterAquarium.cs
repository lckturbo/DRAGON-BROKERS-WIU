using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManagement namespace

public class EnterAquarium : MonoBehaviour
{
    // This method is called when another collider enters the trigger collider attached to the object where this script is attached
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player is the one entering the house
        if (other.CompareTag("Player"))
        {
            // Load the scene called "Home"
            SceneManager.LoadScene("FishTank");
        }
    }
}

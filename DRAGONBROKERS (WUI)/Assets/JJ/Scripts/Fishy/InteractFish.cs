using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractFish : MonoBehaviour
{
    private string fishType;

    // This method is called by the FishFSM or FishSpawner to set the fish type when the fish is spawned
    public void SetFishType(string type)
    {
        fishType = type;
    }

    private void OnMouseDown()
    {
        // This method is called when the fish is clicked
        Debug.Log($"You caught a {fishType}!");

        // Destroy the fish GameObject to despawn it
        Destroy(gameObject);
    }
}

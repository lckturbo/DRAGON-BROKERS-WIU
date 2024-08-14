using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DebugTestButtons : MonoBehaviour
{
    public Inventory inventory; // Reference to the Inventory script

    public GameObject BG_Panel; // Reference to the BG_Panel GameObject

    // Update is called once per frame
    void Update()
    {
        // Check if the '0' key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // Add a new fish to the inventory
            Item fishItem = new Item("Fish");
            inventory.AddItem(fishItem);

            // Print the inventory contents
            inventory.PrintInventory();
        }

        // Check if the 'E' key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the active state of the BG_Panel
            if (BG_Panel != null)
            {
                BG_Panel.SetActive(!BG_Panel.activeSelf);
            }
            else
            {
                Debug.LogWarning("BG_Panel reference is not set!");
            }
        }
    }
}

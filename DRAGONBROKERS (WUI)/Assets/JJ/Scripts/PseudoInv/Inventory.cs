using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> items; // List to store the items in the inventory

    private void Start()
    {
        items = new List<Item>(); // Initialize the inventory list
    }

    // Method to add an item to the inventory
    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log(item.itemName + " has been added to the inventory.");
    }

    // Method to print the entire inventory
    public void PrintInventory()
    {
        Debug.Log("Inventory contains:");
        foreach (Item item in items)
        {
            Debug.Log(item.itemName);
        }
    }
}

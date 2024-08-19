using UnityEngine;

public class FishClickHandler : MonoBehaviour
{
    public string itemName;  // Name of the fish/item
    public int quantity = 1;  // Quantity to add to the inventory
    public Sprite itemSprite;  // The sprite of the fish/item
    public string itemDescription;  // Description of the fish/item
    public int worth = 10;  // Value of the fish/item
    public float weight = 1f;  // Weight of the fish/item

    private InventoryManager inventoryManager;

    private void Start()
    {
        // Find the InventoryManager in the scene
        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found in the scene!");
        }
    }

    private void OnMouseDown()
    {
        if (inventoryManager != null)
        {
            // Add the item to the inventory
            int remaining = inventoryManager.AddItem(itemName, quantity, itemSprite, itemDescription, worth, weight);

            // If the item was successfully added (no remaining quantity), destroy the fish
            if (remaining <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Not enough space in inventory to add all items.");
            }
        }
    }
}

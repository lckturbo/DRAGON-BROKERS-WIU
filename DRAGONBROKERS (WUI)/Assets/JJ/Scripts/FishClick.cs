using UnityEngine;

public class FishClickHandler : MonoBehaviour
{
    private string itemName;  // Name of the fish/item
    private int quantity = 1;  // Quantity to add to the inventory
    private Sprite itemSprite;  // The sprite of the fish/item
    private string itemDescription;  // Description of the fish/item
    private int worth = 10;  // Value of the fish/item
    private float weight = 1f;  // Weight of the fish/item

    private InventoryManager inventoryManager;
    private FishingProbability fishingProbability;

    private void Start()
    {
        // Find the InventoryManager in the scene
        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found in the scene!");
        }

        fishingProbability = FindAnyObjectByType<FishingProbability>();
        if (fishingProbability == null)
        {
            Debug.LogError("Fishing Probability is not found");
        }
    }

    private void OnMouseDown()
    {
        itemName = fishingProbability.rareItemName;
        quantity = fishingProbability.rareQuantity;
        itemSprite = fishingProbability.rareSprite;
        itemDescription = fishingProbability.rareItemDescription;
        worth = fishingProbability.rworth;
        weight = fishingProbability.rWeight;

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

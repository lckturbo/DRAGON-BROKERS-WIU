using Unity.VisualScripting;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string itemName;
    public int quantity;
    public Sprite sprite;
    private InventoryManager inventoryManager;
    private FishingProbability fishingProbability;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory Canvas Variant").GetComponent<InventoryManager>();
        fishingProbability = GetComponent<FishingProbability>();
    }

    private void Update()
    {
        // Check if the addToInv boolean is true before adding the item
        Debug.Log("Checking addToInv: " + fishingProbability.addToInv); // Add this to see when Update() checks addToInv
        if (fishingProbability.addToInv)
        {
            Debug.Log("IT IS TRYING TO ADD ITEM");
            inventoryManager.AddItem(itemName, quantity, sprite);
            fishingProbability.addToInv = false; // Reset it after adding the item
        }
    }
}

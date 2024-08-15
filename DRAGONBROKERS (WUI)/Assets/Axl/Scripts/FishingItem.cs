using Unity.VisualScripting;
using UnityEngine;

public class FishingItem : MonoBehaviour
{
    public string itemName;
    public int quantity;
    public Sprite sprite;
    [TextArea] public string itemDescription;
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
        if (fishingProbability.addToInv)
        {
            Debug.Log("IT IS TRYING TO ADD ITEM");
            inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            fishingProbability.addToInv = false; // Reset it after adding the item
        }
    }
}

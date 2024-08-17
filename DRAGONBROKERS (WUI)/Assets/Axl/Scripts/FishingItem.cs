using Unity.VisualScripting;
using UnityEngine;

public class FishingItem : MonoBehaviour
{
    public string itemName;
    public int quantity;
    public Sprite sprite;
    [TextArea] public string itemDescription;

    public int worth;

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

            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription, worth);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;
            }

            fishingProbability.addToInv = false; // Reset it after adding the item
        }
    }
}

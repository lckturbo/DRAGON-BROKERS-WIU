using TMPro;
using UnityEngine;

public class SellingPrice : MonoBehaviour
{
    private InventoryManager inventoryManager;

    public TMP_Text SeasonalWorth;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory Canvas Variant").GetComponent<InventoryManager>();
        // Find the SeasonalWorthText GameObject and get the TMP_Text component
        SeasonalWorth = GameObject.Find("SeasonalWorth").GetComponent<TMP_Text>();
    }

    void Update()
    {
        DisplayItemWorth("Fish");
    }

    // Method to set the worth of items based on their itemName
    public void SetItemWorth(string itemName, int newWorth)
    {
        if (inventoryManager != null && inventoryManager.itemSlot != null)
        {
            bool itemFound = false;

            for (int i = 0; i < inventoryManager.itemSlot.Length; i++)
            {
                if (inventoryManager.itemSlot[i] != null && inventoryManager.itemSlot[i].itemName == itemName)
                {
                    inventoryManager.itemSlot[i].worth = newWorth;
                    Debug.Log($"Item '{itemName}' in slot {i} now has a worth of {newWorth}.");
                    itemFound = true;
                }
            }

            if (!itemFound)
            {
                Debug.LogWarning($"No items found with the name '{itemName}'.");
            }
        }
        else
        {
            Debug.LogError("InventoryManager or itemSlot array is not initialized.");
        }
    }

    // Function to display the worth of an item based on its name
    public void DisplayItemWorth(string itemName)
    {
        int worth = GetItemWorth(itemName);

        if (worth != -1)
        {
            SeasonalWorth.text = $"{worth}";
        }
        else
        {
            SeasonalWorth.text = $"{itemName} not found in inventory";
        }
    }

    // Function to get the worth of an item based on its name
    private int GetItemWorth(string itemName)
    {
        if (inventoryManager != null && inventoryManager.itemSlot != null)
        {
            for (int i = 0; i < inventoryManager.itemSlot.Length; i++)
            {
                if (inventoryManager.itemSlot[i] != null && inventoryManager.itemSlot[i].itemName == itemName)
                {
                    return inventoryManager.itemSlot[i].worth;
                }
            }
        }
        else
        {
            Debug.LogError("InventoryManager or itemSlot array is not initialized.");
        }

        // Return -1 if the item is not found
        return -1;
    }
}

using TMPro;
using UnityEngine;

public class SellingPrice : MonoBehaviour
{
    private InventoryManager inventoryManager;

    public TMP_Text SeasonalWorth;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory Canvas Variant").GetComponent<InventoryManager>();
        //SeasonalWorth = GameObject.Find("SeasonalWorth").GetComponent<TMP_Text>();
    }

    void Update()
    {
        CheckInventoryAndDisplayWorth();
    }

    // Method to check inventory and display worth if there are more than one item
    private void CheckInventoryAndDisplayWorth()
    {
        int itemCount = 0;

        // Loop through the inventory array
        foreach (var item in inventoryManager.itemSlot)
        {
            if (item != null) // Assuming itemSlot contains null for empty slots
            {
                itemCount++;
            }
        }

        // If there is more than one item in the inventory
        if (itemCount > 1)
        {
            GameObject seasonalWorthObject = GameObject.Find("SeasonalWorth");
            if (seasonalWorthObject != null && seasonalWorthObject.TryGetComponent(out SeasonalWorth))
            {
                DisplayItemWorth("Fish");
            }
            //else
            //{
            //    Debug.Log("SeasonalWorth GameObject or TMP_Text component not found.");
            //}
        }
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

    //For Closing Selling Shop Only *Do Not Touch*
    public void CloseShop()
    {
        if (inventoryManager != null)
        {
            inventoryManager.shopOpen = false;
            Debug.Log("Shop closed via button click.");
        }
        else
        {
            Debug.LogError("InventoryManager is not found when trying to close the shop.");
        }
    }
}

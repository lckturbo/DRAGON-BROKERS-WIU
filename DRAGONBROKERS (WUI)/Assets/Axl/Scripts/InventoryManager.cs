using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //Selective Display of UI elements
    public GameObject InventoryMenu;
    public GameObject DescriptionPortion;
    public GameObject Prices;

    //Booleans to handle UI elements display
    public bool menuActivated;
    public bool shopOpen;
    public bool shopActivated;

    //Array to store items
    public ItemSlot[] itemSlot;

    //Handling weight, Weight Manager
    public WeightManager weightManager;
    public InventoryData Data;

    private void Start()
    {
        InventoryMenu.SetActive(false);

        LoadInventory();
    }

    private void Update()
    {
        // Handle shopOpen state first
        if (shopOpen)
        {
            if (!shopActivated)
            {
                InventoryMenu.SetActive(true);
                shopActivated = true;
                Prices.SetActive(true);
            }
        }
        else if (!shopOpen && shopActivated)
        {
            // Close the inventory if shopOpen is false and the menu is active
            InventoryMenu.SetActive(false);
            shopActivated = false;
            Prices.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !shopOpen)
        {
            if (menuActivated)
            {
                InventoryMenu.SetActive(false);
                menuActivated = false;
                DescriptionPortion.SetActive(false);
            }
            else
            {
                InventoryMenu.SetActive(true);
                menuActivated = true;
                DescriptionPortion.SetActive(true); // Show Description when opened via Tab
            }
        }
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, int worth, float weight)
    {
        Debug.Log("ItemName: " + itemName + ", Quantity: " + quantity + ", ItemSprite: " + itemSprite + ", Worth: " + worth + ", Weight: " + weight);

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull && (itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0))
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, worth, weight);

                if (leftOverItems > 0)
                {
                    // Prevent infinite loops by ensuring we don't continue indefinitely
                    if (leftOverItems == quantity)
                    {
                        Debug.Log("Inventory is full, unable to add all items.");
                        return leftOverItems;
                    }
                    return AddItem(itemName, leftOverItems, itemSprite, itemDescription, worth, weight);
                }
                return 0;
            }
        }

        Debug.Log("Inventory is full, unable to add items.");
        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

    public void SaveInventory()
    {
        // Clear the current list to avoid duplicating items
        Data.items.Clear();

        // Iterate through each slot and save its data if it contains an item (quantity > 0)
        foreach (ItemSlot slot in itemSlot)
        {
            if (slot.quantity > 0)  // Check if there's any item in the slot
            {
                ItemSlotData slotData = new ItemSlotData
                {
                    itemName = slot.itemName,
                    quantity = slot.quantity,
                    itemSprite = slot.itemSprite,
                    itemDescription = slot.itemDescription,
                    worth = slot.worth,
                    weight = slot.weight
                };
                Data.items.Add(slotData);
            }
        }
    }


    public void LoadInventory()
    {
        // Check if Data is null or if it contains no items
        if (Data == null)
        {
            Debug.LogError("InventoryData ScriptableObject is not assigned.");
            return;
        }

        if (Data.items == null || Data.items.Count == 0)
        {
            Debug.Log("No items to load into inventory.");
            return;
        }

        // Load items from Data into item slots
        for (int i = 0; i < Data.items.Count; i++)
        {
            if (i < itemSlot.Length && Data.items[i] != null)
            {
                itemSlot[i].AddItem(
                    Data.items[i].itemName,
                    Data.items[i].quantity,
                    Data.items[i].itemSprite,
                    Data.items[i].itemDescription,
                    Data.items[i].worth,
                    Data.items[i].weight
                );
            }
        }
    }
}

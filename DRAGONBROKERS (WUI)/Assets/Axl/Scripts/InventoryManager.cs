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
}

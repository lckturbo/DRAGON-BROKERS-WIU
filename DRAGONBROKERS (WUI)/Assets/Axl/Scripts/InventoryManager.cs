using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public GameObject DescriptionPortion;
    public GameObject Prices;
    public bool menuActivated;
    public bool shopOpen;
    public bool shopActivated;
    public ItemSlot[] itemSlot;

    private void Update()
    {
        // Handle shopOpen state first
        if (shopOpen)
        {
            if (!shopActivated)
            {
                Time.timeScale = 0;
                InventoryMenu.SetActive(true);
                shopActivated = true;
                Prices.SetActive(true);
            }
        }
        else if (!shopOpen && shopActivated)
        {
            // Close the inventory if shopOpen is false and the menu is active
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            shopActivated = false;
            Prices.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !shopOpen)
        {
            if (menuActivated)
            {
                Time.timeScale = 1;
                InventoryMenu.SetActive(false);
                menuActivated = false;
                DescriptionPortion.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                InventoryMenu.SetActive(true);
                menuActivated = true;
                DescriptionPortion.SetActive(true); // Show Description when opened via Tab
            }
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    for (int i = 0; i < itemSlot.Length; i++)
        //    {
        //        if (itemSlot[i] != null)
        //        {
        //            Debug.Log($"Slot {i}: {itemSlot[i].itemName} - Quantity: {itemSlot[i].quantity}");
        //        }
        //        else
        //        {
        //            Debug.Log($"Slot {i}: Empty or null");
        //        }
        //    }
        //}


    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, int worth) // Added int worth
    {
        Debug.Log("itemName = " + itemName + "quantity = " + quantity + "itemSprite = " + itemSprite + "Worth = " + worth);
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].name == name || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, worth);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription, worth);
                }
                return leftOverItems;
            }
        }
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

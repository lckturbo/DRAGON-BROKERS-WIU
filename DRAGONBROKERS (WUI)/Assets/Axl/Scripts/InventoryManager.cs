using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public GameObject DescriptionPortion; // JJ
    public GameObject Prices; // JJ
    public bool menuActivated;
    public bool shopOpen; // JJ
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
                DescriptionPortion.SetActive(false); // JJ
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

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, int worth) // Added int worth, JJ
    {
        Debug.Log("itemName = " + itemName + "quantity = " + quantity + "itemSprite = " + itemSprite + "Worth = " + worth);
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, worth);
                return;
            }
        }
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

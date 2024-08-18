using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Xml.Serialization;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //ITEM DATA
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;

    public int maxNumberOfItems;

    public int worth;

    //ITEM SLOT
    public TMP_Text quantityText;
    public Image itemImage;

    //ITEM DESCRIPTION
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;
    public InventoryManager inventoryManager;

    public GoldManager _goldManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory Canvas Variant").GetComponent<InventoryManager>();

        _goldManager = GameObject.FindObjectOfType<GoldManager>();

        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found!");
        }

        if (_goldManager == null)
        {
            Debug.LogError("GoldManager not found!");
        }
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, int worth) // Added int worth
    {
        //Check to see if slot is full
        if (isFull)
        {
            return quantity;
        }

        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        this.itemDescription = itemDescription;
        this.worth = worth;

        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            //Return leftovers
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        //Update quantity text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Item Slot Clicked!");

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }


    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        ItemDescriptionNameText.text = itemName;
        ItemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
    }

    public void OnRightClick()
    {
        if (inventoryManager.shopOpen)
        {
                int index = System.Array.IndexOf(inventoryManager.itemSlot, this);

                if (index >= 0)
                {
                    // Debug the worth value before adding it to goldCount
                    Debug.Log($"Item '{itemName}' worth: {worth}");
                    _goldManager.goldCount += worth;

                    ClearSlot();
                    //inventoryManager.itemSlot[index] = null;
                    inventoryManager.DeselectAllSlots();
                }
        }
    }

    void ClearSlot()
    {
        itemName = null;
        quantity = 0;
        itemSprite = null;
        itemDescription = null;
        worth = 0;
        isFull = false;

        quantityText.enabled = false;
        itemImage.sprite = null;
        itemDescriptionImage.sprite = null;
        ItemDescriptionNameText.text = "";
        ItemDescriptionText.text = "";
        selectedShader.SetActive(false);
        thisItemSelected = false;
    }
}

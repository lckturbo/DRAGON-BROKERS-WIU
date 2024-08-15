using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SellSlot : MonoBehaviour, IPointerClickHandler
{
    // ITEM DATA
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;

    // ITEM SLOT UI COMPONENTS
    public TMP_Text quantityText;
    public Image itemImage;

    public GameObject selectedShader;
    public bool thisItemSelected;
    public InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("Sell_Inventory").GetComponent<InventoryManager>();
    }

    public void AddItem(int index)
    {
        if (index < 0 || index >= inventoryManager.itemSlot.Length)
        {
            Debug.LogError("Invalid index for itemSlot array.");
            return;
        }

        ItemSlot selectedItem = inventoryManager.itemSlot[index];

        itemName = selectedItem.itemName;
        quantity = selectedItem.quantity;
        itemSprite = selectedItem.itemSprite;
        itemDescription = selectedItem.itemDescription;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
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
    }

    public void OnRightClick()
    {
    }
}

using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;
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
    public float weight;

    // Declare a variable to track the number of items removed
    private int itemsRemovedCount = 0;

    //ITEM SLOT
    public TMP_Text quantityText;
    public Image itemImage;

    //ITEM DESCRIPTION
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;

    //REFERENCED SCRIPTS
    public InventoryManager inventoryManager;
    public GoldManager _goldManager;
    public WeightManager weightManager;
    public FishingProbability fish;

    //FISH PREFAB (Fish Tank)
    public GameObject fishPrefab;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory Canvas Variant").GetComponent<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found!");
        }

        _goldManager = GameObject.FindObjectOfType<GoldManager>();
        if (_goldManager == null)
        {
            Debug.LogError("GoldManager not found!");
        }

        weightManager = GameObject.FindObjectOfType<WeightManager>();
        if (weightManager == null)
        {
            Debug.LogError("InventoryWeightManager not found!");
        }

        fish = GameObject.FindObjectOfType<FishingProbability>();
        if (fish == null)
        {
            Debug.LogError("fishingProbability not found!");
        }
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, int worth, float weight) // Added int worth
    {
        if (weightManager == null)
        {
            Debug.LogError("WeightManager is not assigned or found.");
            return quantity;
        }

        //Check to see if slot is full
        if (isFull || !weightManager.CanAddItem(weight, quantity))
        {
            return quantity;
        }

        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        this.itemDescription = itemDescription;
        this.worth = worth;
        this.weight = weight;

        weightManager.AddWeight(weight, quantity);

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
                // Reduce the quantity by 1
                quantity--;
                weightManager.RemoveWeight(weight, 1); // Remove the weight of the sold item

                // Update the gold count
                _goldManager.goldCount += worth;
                Debug.Log($"Item '{itemName}' worth: {worth}");

                // Update the UI
                if (quantity > 0)
                {
                    quantityText.text = quantity.ToString();
                }
                else
                {
                    ClearSlot();
                }

                inventoryManager.DeselectAllSlots();
            }
        }

        if (SceneManager.GetActiveScene().name == "FishTank")
        {
            // Logic for the FishTank scene
            if (quantity > 0 && itemName == "Salmonella")
            {
                quantity--;
                weightManager.RemoveWeight(weight, 1); // Remove the weight of the sold item

                // Instantiate a fish prefab in the scene
                if (fishPrefab != null)
                {
                    float randomX = Random.Range(-9f, 9f);
                    float randomY = Random.Range(-6f, 1f);
                    Vector3 randomPosition = new Vector3(randomX, randomY, 0);
                    Instantiate(fishPrefab, randomPosition, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("Fish prefab is not assigned.");
                }

                // Update the UI
                if (quantity > 0)
                {
                    quantityText.text = quantity.ToString();
                }
                else
                {
                    ClearSlot();
                }
            }
            else if (itemName != "Salmonella")
            {
                Debug.LogWarning("Only Salmonella fish can be dropped in the FishTank scene.");
            }

            return; // Exit to prevent further execution
        }

        if (SceneManager.GetActiveScene().name == "FishingPort")
        {
            // Logic for the FishingPort scene
            if (quantity > 0)
            {
                quantity--;
                weightManager.RemoveWeight(weight, 1); // Remove the weight of the sold item
                itemsRemovedCount++; // Increment the count of items removed

                // Update the UI
                if (quantity > 0)
                {
                    quantityText.text = quantity.ToString();
                }
                else
                {
                    ClearSlot();
                }

                // Check if 5 items have been removed
                if (itemsRemovedCount >= 5)
                {
                    itemsRemovedCount = 0; // Reset the count after degrading the environment
                    fish.DegradeEnvironment();
                }
            }

            return; // Exit to prevent further execution
        }
    }

    void ClearSlot()
    {
        weightManager.RemoveWeight(weight, quantity);

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

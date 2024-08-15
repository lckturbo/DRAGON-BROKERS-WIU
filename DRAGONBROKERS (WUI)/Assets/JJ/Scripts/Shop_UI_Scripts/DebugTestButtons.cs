using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DebugTestButtons : MonoBehaviour
{
    public GameObject BG_Panel; // Reference to the BG_Panel GameObject
    public GameObject Sell_Panel;

    public bool isShop;

    private InventoryManager inventoryManager;
    public string itemName;
    public int quantity;
    public Sprite sprite;
    [TextArea] public string description;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory Canvas Variant").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager is not found!");
            return;
        }

        // Check if the '0' key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            inventoryManager.AddItem(itemName, quantity, sprite, description);
        }

        // Check if the 'E' key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the active state of the BG_Panel
            if (BG_Panel != null)
            {
                BG_Panel.SetActive(!BG_Panel.activeSelf);
            }
            else
            {
                Debug.LogWarning("BG_Panel reference is not set!");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            isShop = !isShop;

            // Toggle the active state of the BG_Panel
            if (Sell_Panel != null)
            {
                Sell_Panel.SetActive(isShop);
            }
            else
            {
                Debug.LogWarning("Sell_Panel reference is not set!");
            }

            Debug.Log("Shop is " + (isShop ? "open" : "closed"));
        }
    }
}

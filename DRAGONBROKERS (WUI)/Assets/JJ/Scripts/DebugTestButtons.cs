using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DebugTestButtons : MonoBehaviour
{
    public GameObject BG_Panel;

    private InventoryManager inventoryManager;
    public string itemName;
    public int quantity;
    public Sprite sprite;
    [TextArea] public string description;
    public int worth;

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

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            inventoryManager.AddItem(itemName, quantity, sprite, description, worth);
        }

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if (BG_Panel != null)
        //    {
        //        BG_Panel.SetActive(!BG_Panel.activeSelf);
        //    }
        //    else
        //    {
        //        Debug.LogWarning("BG_Panel reference is not set!");
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    inventoryManager.shopOpen = !inventoryManager.shopOpen;
        //}

    }
}

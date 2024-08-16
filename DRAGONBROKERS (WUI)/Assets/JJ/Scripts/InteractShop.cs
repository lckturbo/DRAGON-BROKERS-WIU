using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractShop : MonoBehaviour
{
    public GameObject BG_Panel;
    InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
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
    }

    public void CollideSell(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inventoryManager.shopOpen = !inventoryManager.shopOpen;
        }
    }

    public void CollideShop(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (BG_Panel != null)
            {
                BG_Panel.SetActive(!BG_Panel.activeSelf);
            }
            else
            {
                Debug.LogWarning("BG_Panel reference is not set!");
            }
        }
    }
}

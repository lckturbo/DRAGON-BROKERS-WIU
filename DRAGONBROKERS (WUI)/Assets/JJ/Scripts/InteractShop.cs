using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractShop : MonoBehaviour
{
    public GameObject BG_Panel;
    public GameObject SellNPC;
    public GameObject BuyNPC;
    InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("Inventory Canvas Variant").GetComponent<InventoryManager>();

        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager is not found!");
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject == SellNPC)
            {
                inventoryManager.shopOpen = !inventoryManager.shopOpen;
                Debug.Log("Toggled shopOpen state for SellNPC.");
            }
            else if (gameObject == BuyNPC)
            {
                if (BG_Panel != null)
                {
                    BG_Panel.SetActive(!BG_Panel.activeSelf);
                    Debug.Log("Toggled BG_Panel visibility for BuyNPC.");
                }
                else
                {
                    Debug.LogWarning("BG_Panel reference is not set!");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject == SellNPC)
            {
                inventoryManager.shopOpen = false;
                Debug.Log("Closed shop for SellNPC.");
            }
            else if (gameObject == BuyNPC)
            {
                if (BG_Panel != null)
                {
                    BG_Panel.SetActive(false);
                    Debug.Log("Closed BG_Panel for BuyNPC.");
                }
                else
                {
                    Debug.LogWarning("BG_Panel reference is not set!");
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItems : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private GoldManager goldManager;

    public string PoisonName;
    public int PoisonQuantity;
    public Sprite PoisonSprite;
    [TextArea] public string PoisonDescription;
    public int PoisonWorth;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory Canvas Variant").GetComponent<InventoryManager>();
        goldManager = GameObject.FindObjectOfType<GoldManager>();
    }

    public void OnBuyPoison()
    {
        if (inventoryManager != null)
        {
            goldManager.goldCount -= 10;
            inventoryManager.AddItem(PoisonName, PoisonQuantity, PoisonSprite, PoisonDescription, PoisonWorth);
        }
        else
        {
            Debug.LogError("InventoryManager is not assigned.");
        }
    }
}

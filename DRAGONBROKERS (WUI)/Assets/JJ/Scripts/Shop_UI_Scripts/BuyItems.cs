using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItems : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private GoldManager goldManager;
    private FishFoodManager fishFoodManager;

    public string PoisonName;
    public int PoisonQuantity;
    public Sprite PoisonSprite;
    [TextArea] public string PoisonDescription;
    public int PoisonWorth;
    public float PosionWeight;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory Canvas Variant").GetComponent<InventoryManager>();
        goldManager = GameObject.FindObjectOfType<GoldManager>();
        fishFoodManager = GameObject.FindObjectOfType<FishFoodManager>();
    }

    public void OnBuyPoison()
    {
        if (inventoryManager != null)
        {
            goldManager.goldCount -= 10;
            inventoryManager.AddItem(PoisonName, PoisonQuantity, PoisonSprite, PoisonDescription, PoisonWorth, PosionWeight);
        }
        else
        {
            Debug.LogError("InventoryManager is not assigned.");
        }
    }

    public void BuyFishFood()
    {
        goldManager.goldCount -= 10;
        fishFoodManager.foodCount += 3;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItems : MonoBehaviour
{
    private GoldManager goldManager;
    private FishFoodManager fishFoodManager;
    private WeightManager weightManager;
    public BuyItemsData buyItemsData;

    private void Start()
    {
        goldManager = GameObject.FindObjectOfType<GoldManager>();
        if (goldManager != null) 
        {
            Debug.Log("GoldManager not found");
        }

        fishFoodManager = GameObject.FindObjectOfType<FishFoodManager>();
        if(fishFoodManager != null)
        {
            Debug.Log("FishFoodManager not found");
        }

        weightManager = GameObject.FindObjectOfType<WeightManager>();
        if (weightManager != null)
        {
            Debug.Log("WeightManager not found");
        }
    }

    public void OnBuyTrawling()
    {
        if (goldManager.goldCount >= 300)
        {
            goldManager.goldCount -= 300;
            buyItemsData.trawlActive = true;
        }
        else
        {
            Debug.Log("Not Enough Money for Trawling");
        }
    }

    public void OnBuyMoreWeight()
    {
        if (goldManager.goldCount >= 800)
        {
            goldManager.goldCount -= 800;
            buyItemsData.IncreaseWeight = true;
        }
        else
        {
            Debug.Log("Not Enough Money for Space Upgrade Within Inventory");
        }
    }

    public void OnBuyPoison()
    {
        if (goldManager.goldCount >= 100)
        {
            goldManager.goldCount -= 100;
            buyItemsData.poisonActive = true;
        }
        else
        {
            Debug.Log("Not Enough Money for Poison");
        }
    }

    public void BuyFishFood()
    {
        if (goldManager.goldCount >= 10)
        {
            goldManager.goldCount -= 10;
            fishFoodManager.foodCount += 3;
        }
        else
        {
            Debug.Log("Not Enough Money for Fish Food");
        }
    }

    public void BuyPlatRod()
    {
        if (goldManager.goldCount >= 100)
        {
            goldManager.goldCount -= 100;
            buyItemsData.PlatRod = true;
        }
        else
        {
            Debug.Log("Not Enough Money for Plat Rod");
        }
    }

    public void BuyEmeRod()
    {
        if (goldManager.goldCount >= 400)
        {
            goldManager.goldCount -= 400;
            buyItemsData.IRod = false;
            buyItemsData.EmeRod = true;
        }
        else
        {
            Debug.Log("Not Enough Money for EmeRod");
        }
    }

    public void BuyIRod()
    {
        if (goldManager.goldCount >= 600)
        {
            goldManager.goldCount -= 600;
            buyItemsData.EmeRod = false;
            buyItemsData.IRod = true;
        }
        else
        {
            Debug.Log("Not Enough Money for IRod");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Trawl: " + buyItemsData.trawlActive + " , MoreWeight: " + buyItemsData.IncreaseWeight + " ,Poision: " + buyItemsData.poisonActive 
                + " ,PlatRod: " + buyItemsData.PlatRod + " ,EmeRod: " + buyItemsData.EmeRod + ", IRod: " + buyItemsData.IRod);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            buyItemsData.poisonActive = false;
            buyItemsData.trawlActive = false;
            buyItemsData.IncreaseWeight = false;
            buyItemsData.PlatRod = false;
            buyItemsData.EmeRod = false;
            buyItemsData.IRod = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItems : MonoBehaviour
{
    private GoldManager goldManager;
    private FishFoodManager fishFoodManager;
    private WeightManager weightManager;
    public BuyItemsData buyItemsData;

    public bool poisonActive = false; //Poison
    public bool trawlActive = false; //Trawling Boat Add on
    public bool IncreaseWeight = false; //Max Weight Increase Add on

    public bool PlatRod = false;
    public bool EmeRod = false;
    public bool IRod = false;

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
            trawlActive = true;
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
            IncreaseWeight = true;
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
            poisonActive = true;
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
            PlatRod = true;
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
            EmeRod = true;
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
            IRod = true;
        }
        else
        {
            Debug.Log("Not Enough Money for IRod");
        }
    }

    public void SaveBuyItems()
    {
        buyItemsData.poisonActive = poisonActive;
        buyItemsData.trawlActive = trawlActive;
        buyItemsData.IncreaseWeight = IncreaseWeight;
        buyItemsData.PlatRod = PlatRod;
        buyItemsData.EmeRod = EmeRod;
        buyItemsData.IRod = IRod;

        Debug.Log("poisonActive: " + poisonActive);
        Debug.Log("trawlActive: " + trawlActive);
        Debug.Log("increaseweight: " + IncreaseWeight);
        Debug.Log("platrod: " + PlatRod);
        Debug.Log("emerod: " + EmeRod);
        Debug.Log("irod: " + IRod);
    }

    public void LoadGold()
    {
        poisonActive = buyItemsData.poisonActive;
        trawlActive = buyItemsData.trawlActive;
        IncreaseWeight = buyItemsData.IncreaseWeight;
        PlatRod = buyItemsData.PlatRod;
        EmeRod = buyItemsData.EmeRod;
        IRod = buyItemsData.IRod;

        Debug.Log("poisonActive: " + poisonActive);
        Debug.Log("trawlActive: " + trawlActive);
        Debug.Log("increaseweight: " + IncreaseWeight);
        Debug.Log("platrod: " + PlatRod);
        Debug.Log("emerod: " + EmeRod);
        Debug.Log("irod: " + IRod);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("PLATINUM ROD IS " + PlatRod);
        }
    }
}

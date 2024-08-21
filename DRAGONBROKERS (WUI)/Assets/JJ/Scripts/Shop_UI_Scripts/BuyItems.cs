using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItems : MonoBehaviour
{
    private GoldManager goldManager;
    private FishFoodManager fishFoodManager;

    public bool poisonActive = false;

    private void Start()
    {
        goldManager = GameObject.FindObjectOfType<GoldManager>();
        fishFoodManager = GameObject.FindObjectOfType<FishFoodManager>();
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
        goldManager.goldCount -= 10;
        fishFoodManager.foodCount += 3;
    }
}

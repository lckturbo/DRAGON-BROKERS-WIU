using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.U2D;

public class FishingProbability : MonoBehaviour
{
    public float BaseSeasonalChance = 0.6f;
    public float BaseRareChance = 0.29f;
    public float BaseNothingChance = 0.1f;
    public float BaseLegendaryChance = 0.01f;

    private float SeasonalChance;
    private float RareChance;
    private float NothingChance;
    private float LegendaryChance;

    public string seasonalItemName;
    public int seasonalQuantity;
    public Sprite seasonalSprite;
    [TextArea] public string seasonalItemDescription;

    public string rareItemName;
    public int rareQuantity;
    public Sprite rareSprite;
    [TextArea] public string rareItemDescription;

    public string legendaryItemName;
    public int legendaryQuantity;
    public Sprite legendarySprite;
    [TextArea] public string legendaryItemDescription;

    private Environment currentEnvironment;
    private InventoryManager inventoryManager;

    public bool addToInv = true;

    public enum Environment
    {
        Perfect,
        SlightDamage,
        ModerateDamage,
        SeverelyDamaged
    }

    private void Start()
    {
        // Initialize to a default environment, e.g., Perfect
        SetEnvironment(Environment.Perfect);
        inventoryManager = GameObject.Find("Inventory Canvas Variant").GetComponent<InventoryManager>();
    }

    private void Update()
    {
        //Inputs here are meant for debugging
        {
            // Cycle through environments with 1, 2, 3, 4 keys
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetEnvironment(Environment.Perfect);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetEnvironment(Environment.SlightDamage);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetEnvironment(Environment.ModerateDamage);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetEnvironment(Environment.SeverelyDamaged);
            }

            // Roll the chances 100 times with the press of "M"
            if (Input.GetKeyDown(KeyCode.M))
            {
                TestFishingProbability(100);
            }

            // Roll the chances once with the press of "R"
            if (Input.GetKeyDown(KeyCode.R))
            {
                FishingChance();
            }
        }

    }

    // Method to set the environment type and adjust chances
    public void SetEnvironment(Environment environment)
    {
        currentEnvironment = environment;

        switch (currentEnvironment)
        {
            case Environment.Perfect:
                SeasonalChance = BaseSeasonalChance;
                RareChance = BaseRareChance;
                NothingChance = BaseNothingChance;
                LegendaryChance = BaseLegendaryChance;
                break;

            case Environment.SlightDamage:
                SeasonalChance = BaseSeasonalChance * 0.9f;
                RareChance = BaseRareChance * 0.9f;
                NothingChance = BaseNothingChance * 1.2f;
                LegendaryChance = BaseLegendaryChance * 0.8f;
                break;

            case Environment.ModerateDamage:
                SeasonalChance = BaseSeasonalChance * 0.75f;
                RareChance = BaseRareChance * 0.75f;
                NothingChance = BaseNothingChance * 1.5f;
                LegendaryChance = BaseLegendaryChance * 0.5f;
                break;

            case Environment.SeverelyDamaged:
                SeasonalChance = BaseSeasonalChance * 0.5f;
                RareChance = BaseRareChance * 0.5f;
                NothingChance = BaseNothingChance * 2.0f;
                LegendaryChance = BaseLegendaryChance * 0.25f;
                break;
        }

        // Debug.Log($"Environment set to: {currentEnvironment}");
        // Debug.Log($"SeasonalChance: {SeasonalChance}, RareChance: {RareChance}, NothingChance: {NothingChance}, LegendaryChance: {LegendaryChance}");
    }

    public void FishingRodChance(Animator playerAnim)
    {
        // Generate a random float between 0.0 and 1.0
        float chance = Random.value;

        // Calculate cumulative probabilities
        float seasonalThreshold = SeasonalChance;
        float rareThreshold = seasonalThreshold + RareChance;
        float nothingThreshold = rareThreshold + NothingChance;
        float legendaryThreshold = nothingThreshold + LegendaryChance;

        // Determine the animation to play based on the chance and thresholds
        if (chance < seasonalThreshold)
        {
            inventoryManager.AddItem(seasonalItemName, seasonalQuantity, seasonalSprite, seasonalItemDescription);
            playerAnim.Play("playerWonFish");
            Debug.Log("Animation Played: 1");
        }
        else if (chance < rareThreshold)
        {
            inventoryManager.AddItem(rareItemName, rareQuantity, rareSprite, rareItemDescription);
            playerAnim.Play("playerWonFish2");
            Debug.Log("Animation Played: 2");
        }
        else if (chance < nothingThreshold)
        {
            playerAnim.Play("playerStill");
            Debug.Log("Animation Played: No fish caught");
        }
        else if (chance < legendaryThreshold)
        {
            inventoryManager.AddItem(legendaryItemName, legendaryQuantity, legendarySprite, legendaryItemDescription);
            playerAnim.Play("playerWonFish3");
            Debug.Log("Animation Played: 3");
        }
    }

    /*
     Eveything meant for debugging will be below here!
    | | | |
    | | | |
    | | | |
     */
    // Method to determine the result based on the current chances
    public string FishingChance()
    {
        // Generate a random float between 0.0 and 1.0 using UnityEngine's Random
        float chance = UnityEngine.Random.value;


        // Calculate cumulative probabilities
        float seasonalThreshold = SeasonalChance;
        float rareThreshold = seasonalThreshold + RareChance;
        float nothingThreshold = rareThreshold + NothingChance;
        float legendaryThreshold = nothingThreshold + LegendaryChance;

        // Debug.Log($"Chance: {chance}, Thresholds -> Seasonal: {seasonalThreshold}, Rare: {rareThreshold}, Nothing: {nothingThreshold}, Legendary: {legendaryThreshold}");

        // Determine the result based on the chance and thresholds
        if (chance < seasonalThreshold)
        {
            // Debug.Log("Result: Seasonal Fish");
            return "Seasonal Fish";
        }
        else if (chance < rareThreshold)
        {
            // Debug.Log("Result: Rare Fish");
            return "Rare Fish";
        }
        else if (chance < nothingThreshold)
        {
            // Debug.Log("Result: No Fish Caught");
            return "No Fish";
        }
        else if (chance < legendaryThreshold)
        {
            // Debug.Log("Result: Legendary Fish");
            return "Legendary Fish";
        }

        return "Unknown";
    }

    // Method to test the probability by rolling it multiple times (Debugging Only)
    public void TestFishingProbability(int rolls)
    {
        int seasonalCount = 0;
        int rareCount = 0;
        int nothingCount = 0;
        int legendaryCount = 0;

        for (int i = 0; i < rolls; i++)
        {
            string result = FishingChance();
            switch (result)
            {
                case "Seasonal Fish":
                    seasonalCount++;
                    break;
                case "Rare Fish":
                    rareCount++;
                    break;
                case "No Fish":
                    nothingCount++;
                    break;
                case "Legendary Fish":
                    legendaryCount++;
                    break;
            }
        }

        // Debug.Log($"Results after {rolls} rolls:");
        // Debug.Log($"Seasonal Fish: {seasonalCount} times");
        // Debug.Log($"Rare Fish: {rareCount} times");
        // Debug.Log($"No Fish: {nothingCount} times");
        // Debug.Log($"Legendary Fish: {legendaryCount} times");
    }
}

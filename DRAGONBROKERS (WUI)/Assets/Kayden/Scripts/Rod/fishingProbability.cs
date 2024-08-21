using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.U2D;
using static FishingProbability;

public class FishingProbability : MonoBehaviour
{
    //Scriptable Objects
    public GameData gameData;

    //Inventory
    public bool addToInv = true;

    //OverFishing Count
    private int fishingCount = 0;

    //Environment
    public enum Environment
    {
        Perfect,
        SlightDamage,
        ModerateDamage,
        SeverelyDamaged
    }

    private Environment currentEnvironment = Environment.Perfect;
    private InventoryManager inventoryManager;

    //Seasons
    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    public Season currentSeason = Season.Spring;

    //Fish Init
    // Seasonal fish for each season
    public string springFishName;
    public int springFishQuantity;
    public Sprite springFishSprite;
    [TextArea] public string springFishDescription;
    public int springFishWorth;
    public float springFishWeight;

    public string summerFishName;
    public int summerFishQuantity;
    public Sprite summerFishSprite;
    [TextArea] public string summerFishDescription;
    public int summerFishWorth;
    public float summerFishWeight;

    public string autumnFishName;
    public int autumnFishQuantity;
    public Sprite autumnFishSprite;
    [TextArea] public string autumnFishDescription;
    public int autumnFishWorth;
    public float autumnFishWeight;

    public string winterFishName;
    public int winterFishQuantity;
    public Sprite winterFishSprite;
    [TextArea] public string winterFishDescription;
    public int winterFishWorth;
    public float winterFishWeight;

    public string rareItemName;
    public int rareQuantity;
    public Sprite rareSprite;
    [TextArea] public string rareItemDescription;
    public int rworth;
    public int rWeight;

    public string legendaryItemName;
    public int legendaryQuantity;
    public Sprite legendarySprite;
    [TextArea] public string legendaryItemDescription;
    public int lworth;
    public int lWeight;

    //Fish Base Chances
    public float BaseSeasonalChance = 0.6f;
    public float BaseRareChance = 0.29f;
    public float BaseNothingChance = 0.1f;
    public float BaseLegendaryChance = 0.01f;

    // Chance modifier for off-season fish
    public float offSeasonModifier = 0.1f;

    private float SeasonalChance;
    private float RareChance;
    private float NothingChance;
    private float LegendaryChance;

    //START
    private void Start()
    {
        SetEnvironment(gameData.currentEnvironment);
        SetSeason(gameData.currentSeason);

        inventoryManager = GameObject.Find("Inventory Canvas Variant").GetComponent<InventoryManager>();
    }

    //UPDATE
    private void Update()
    {
        //Inputs here are meant for debugging
        {
            //Cycle ENVIRONMENT
            {
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
            }

            //Cycel SEASON
            {
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    SetSeason(Season.Spring);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    SetSeason(Season.Summer);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    SetSeason(Season.Autumn);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    SetSeason(Season.Winter);
                }
            }

            // Roll the chances 100 times with the press of "M"
            if (Input.GetKeyDown(KeyCode.M))
            {
                TestFishingProbability(100);
            }

            // Roll the chances once with the press of "R"
            if (Input.GetKeyDown(KeyCode.N))
            {
                FishingChance();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                Debug.Log("Environment: " + currentEnvironment + " ,Season: " + currentSeason);
            }
        }

    }

    // Method to set the environment type [ENVIRONMENT]
    public void SetEnvironment(Environment environment)
    {
        gameData.currentEnvironment = environment;

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

        Debug.Log($"Environment set to: {currentEnvironment}");
        Debug.Log($"SeasonChance: {SeasonalChance}, RareChance: {RareChance}, NothingChance: {NothingChance}, LegendaryChance: {LegendaryChance}");

        // Calculate on-season and off-season chances
        float onSeasonChance = SeasonalChance * (1.0f - offSeasonModifier); // Portion for on-season fish
        float offSeasonChance = SeasonalChance * offSeasonModifier; // Portion for off-season fish
        Debug.Log($"On-Season Chance: {onSeasonChance}, Off-Season Chance: {offSeasonChance}");
    }

    // Method to set the season type [SEASON]
    public void SetSeason(Season newSeason)
    {
        gameData.currentSeason = newSeason;
        Debug.Log($"Season set to: {currentSeason}");
    }

    // Method to worsen the environment by 1 level each time
    public void DegradeEnvironment()
    {
        switch (currentEnvironment)
        {
            case Environment.Perfect:
                SetEnvironment(Environment.SlightDamage);
                break;
            case Environment.SlightDamage:
                SetEnvironment(Environment.ModerateDamage);
                break;
            case Environment.ModerateDamage:
                SetEnvironment(Environment.SeverelyDamaged);
                break;
            case Environment.SeverelyDamaged:
                Debug.Log("Environment is already at the lowest level: Severely Damaged.");
                break;
        }

        Debug.LogWarning("Environment has been degraded.");
    }

    // Method to change the season
    public void ChangeSeason()
    {
        switch (currentSeason)
        {
            case Season.Spring:
                currentSeason = Season.Summer;
                break;
            case Season.Summer:
                currentSeason = Season.Autumn;
                break;
            case Season.Autumn:
                currentSeason = Season.Winter;
                break;
            case Season.Winter:
                currentSeason = Season.Spring;
                break;
        }

        // Log the change
        Debug.Log($"Season changed to: {currentSeason}");
    }

    private void AddFishToInventory(string fishName, int fishQuantity, Sprite fishSprite, string fishDescription, int fishWorth, Animator playerAnim, float weight)
    {
        float offSeasonChance = SeasonalChance * offSeasonModifier;
        float offSeasonRandom = Random.value;

        if (offSeasonRandom < offSeasonChance)
        {
            inventoryManager.AddItem(fishName, fishQuantity, fishSprite, fishDescription, fishWorth, weight);
            playerAnim.Play("playerWonFish");
            Debug.Log($"Caught off-season {fishName}");
        }
        else
        {
            // Regular in-season catch
            inventoryManager.AddItem(fishName, fishQuantity, fishSprite, fishDescription, fishWorth, weight);
            playerAnim.Play("playerWonFish");
            Debug.Log($"Caught {fishName}");
        }
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
            // Determine which seasonal fish to catch based on the current season
            switch (currentSeason)
            {
                case Season.Spring:
                    AddFishToInventory(springFishName, springFishQuantity, springFishSprite, springFishDescription, springFishWorth, playerAnim, springFishWeight);
                    break;
                case Season.Summer:
                    AddFishToInventory(summerFishName, summerFishQuantity, summerFishSprite, summerFishDescription, summerFishWorth, playerAnim, summerFishWeight);
                    break;
                case Season.Autumn:
                    AddFishToInventory(autumnFishName, autumnFishQuantity, autumnFishSprite, autumnFishDescription, autumnFishWorth, playerAnim, autumnFishWeight);
                    break;
                case Season.Winter:
                    AddFishToInventory(winterFishName, winterFishQuantity, winterFishSprite, winterFishDescription, winterFishWorth, playerAnim, winterFishWeight);
                    break;
            }
        }
        else if (chance < rareThreshold)
        {
            inventoryManager.AddItem(rareItemName, rareQuantity, rareSprite, rareItemDescription, rworth, rWeight);
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
            inventoryManager.AddItem(legendaryItemName, legendaryQuantity, legendarySprite, legendaryItemDescription, lworth, lWeight);
            playerAnim.Play("playerWonFish3");
            Debug.Log("Animation Played: 3");
        }

        // Increment the fishing attempt count
        fishingCount++;

        // Check if it's time to degrade the environment
        if (fishingCount >= 5)
        {
            fishingCount = 0; // Reset the count after degrading the environment
            DegradeEnvironment();
        }
    }

    // Method to determine the result based on the current chances
    public string FishingChance()
    {
        // Generate a random float between 0.0 and 1.0 using UnityEngine's Random
        float chance = UnityEngine.Random.value;

        // Calculate probabilities within the SeasonalChance
        float onSeasonChance = SeasonalChance * (1.0f - offSeasonModifier); // Portion for on-season fish
        float offSeasonChance = SeasonalChance * offSeasonModifier; // Portion for off-season fish

        // Calculate cumulative probabilities
        float onSeasonThreshold = onSeasonChance;
        float offSeasonThreshold = onSeasonThreshold + offSeasonChance;
        float rareThreshold = offSeasonThreshold + RareChance;
        float nothingThreshold = rareThreshold + NothingChance;
        float legendaryThreshold = nothingThreshold + LegendaryChance;

        // Determine the result based on the chance and thresholds
        string result;

        if (chance < onSeasonThreshold)
        {
            // On-season fish
            Debug.Log("Result: On-Season Fish");
            result = GetOnSeasonFishName();
        }
        else if (chance < offSeasonThreshold)
        {
            // Off-season fish
            Debug.Log("Result: Off-Season Fish");
            result = GetOffSeasonFishName();
        }
        else if (chance < rareThreshold)
        {
            Debug.Log("Result: Rare Fish");
            result = "Rare Fish";
        }
        else if (chance < nothingThreshold)
        {
            Debug.Log("Result: No Fish Caught");
            result = "No Fish";
        }
        else if (chance < legendaryThreshold)
        {
            Debug.Log("Result: Legendary Fish");
            result = "Legendary Fish";
        }
        else
        {
            result = "Unknown";
        }

        // Increment the fishing attempt count
        fishingCount++;

        // Check if it's time to degrade the environment
        if (fishingCount >= 5)
        {
            fishingCount = 0; // Reset the count after degrading the environment
            DegradeEnvironment();
        }

        return result;
    }

    // Helper method to get the on-season fish name based on the current season
    private string GetOnSeasonFishName()
    {
        switch (currentSeason)
        {
            case Season.Spring:
                return "Spring Fish";
            case Season.Summer:
                return "Summer Fish";
            case Season.Autumn:
                return "Autumn Fish";
            case Season.Winter:
                return "Winter Fish";
        }
        return "Unknown Fish";
    }

    // Helper method to get an off-season fish name (random season other than the current one)
    private string GetOffSeasonFishName()
    {
        var offSeasons = new List<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter };
        offSeasons.Remove(currentSeason);

        Season randomOffSeason = offSeasons[UnityEngine.Random.Range(0, offSeasons.Count)];

        switch (randomOffSeason)
        {
            case Season.Spring:
                return "Spring Fish";
            case Season.Summer:
                return "Summer Fish";
            case Season.Autumn:
                return "Autumn Fish";
            case Season.Winter:
                return "Winter Fish";
        }
        return "Unknown Fish";
    }

    // Method to test the probability by rolling it multiple times (Debugging Only)
    public void TestFishingProbability(int rolls)
    {
        int springFishCount = 0;
        int summerFishCount = 0;
        int autumnFishCount = 0;
        int winterFishCount = 0;
        int rareCount = 0;
        int nothingCount = 0;
        int legendaryCount = 0;

        for (int i = 0; i < rolls; i++)
        {
            string result = FishingChance();
            switch (result)
            {
                case "Spring Fish":
                    springFishCount++;
                    break;
                case "Summer Fish":
                    summerFishCount++;
                    break;
                case "Autumn Fish":
                    autumnFishCount++;
                    break;
                case "Winter Fish":
                    winterFishCount++;
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

        Debug.Log($"Results after {rolls} rolls:");
        Debug.Log($"Spring Fish: {springFishCount} times");
        Debug.Log($"Summer Fish: {summerFishCount} times");
        Debug.Log($"Autumn Fish: {autumnFishCount} times");
        Debug.Log($"Winter Fish: {winterFishCount} times");
        Debug.Log($"Rare Fish: {rareCount} times");
        Debug.Log($"No Fish: {nothingCount} times");
        Debug.Log($"Legendary Fish: {legendaryCount} times");

        // Degrade the environment after 100 rolls
        DegradeEnvironment();
    }

    //Saving Environment and Seasons
    public void SaveData()
    {
        gameData.currentSeason = currentSeason;
        gameData.currentEnvironment = currentEnvironment;
    }

}

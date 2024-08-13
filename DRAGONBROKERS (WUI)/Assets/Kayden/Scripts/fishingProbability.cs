using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Environment currentEnvironment;

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
    }

    private void Update()
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
            FishingRodChance();
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

        Debug.Log($"Environment set to: {currentEnvironment}");
        Debug.Log($"SeasonalChance: {SeasonalChance}, RareChance: {RareChance}, NothingChance: {NothingChance}, LegendaryChance: {LegendaryChance}");
    }

    // Method to determine the result based on the current chances
    public string FishingRodChance()
    {
        // Generate a random float between 0.0 and 1.0 using UnityEngine's Random
        float chance = UnityEngine.Random.value;

        // Calculate cumulative probabilities
        float seasonalThreshold = SeasonalChance;
        float rareThreshold = seasonalThreshold + RareChance;
        float nothingThreshold = rareThreshold + NothingChance;
        float legendaryThreshold = nothingThreshold + LegendaryChance;

        Debug.Log($"Chance: {chance}, Thresholds -> Seasonal: {seasonalThreshold}, Rare: {rareThreshold}, Nothing: {nothingThreshold}, Legendary: {legendaryThreshold}");

        // Determine the result based on the chance and thresholds
        if (chance < seasonalThreshold)
        {
            Debug.Log("Result: Seasonal Fish");
            return "Seasonal Fish";
        }
        else if (chance < rareThreshold)
        {
            Debug.Log("Result: Rare Fish");
            return "Rare Fish";
        }
        else if (chance < nothingThreshold)
        {
            Debug.Log("Result: No Fish Caught");
            return "No Fish";
        }
        else if (chance < legendaryThreshold)
        {
            Debug.Log("Result: Legendary Fish");
            return "Legendary Fish";
        }

        return "Unknown";
    }

    // Method to test the probability by rolling it multiple times
    public void TestFishingProbability(int rolls)
    {
        int seasonalCount = 0;
        int rareCount = 0;
        int nothingCount = 0;
        int legendaryCount = 0;

        for (int i = 0; i < rolls; i++)
        {
            string result = FishingRodChance();
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

        Debug.Log($"Results after {rolls} rolls:");
        Debug.Log($"Seasonal Fish: {seasonalCount} times");
        Debug.Log($"Rare Fish: {rareCount} times");
        Debug.Log($"No Fish: {nothingCount} times");
        Debug.Log($"Legendary Fish: {legendaryCount} times");
    }
}

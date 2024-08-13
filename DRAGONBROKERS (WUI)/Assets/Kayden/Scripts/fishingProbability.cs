using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishingProbability : MonoBehaviour
{
    public float SeasonalChance = 0.6f;
    public float RareChance = 0.29f;
    public float NothingChance = 0.1f;
    public float LegendaryChance = 0.01f;

    // Method to determine which animation to play
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
            playerAnim.Play("playerWonFish");
            Debug.Log("Animation Played: 1");
        }
        else if (chance < rareThreshold)
        {
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
            playerAnim.Play("playerWonFish3");
            Debug.Log("Animation Played: 3");
        }
    }
}
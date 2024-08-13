using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishingProbability : MonoBehaviour
{
    public float SeasonalChance = 0.7f;
    public float RareChance = 0.29f;
    public float LegendaryChance = 0.01f;

    // Method to determine which animation to play
    public void FishingRodChance(Animator playerAnim)
    {
        // Generate a random float between 0.0 and 1.0
        float chance = Random.value;

        // Determine the animation to play based on the chance and thresholds
        if (chance < SeasonalChance)
        {
            playerAnim.Play("playerWonFish");
            Debug.Log("Animation Played: 1");
        }
        else if (chance < RareChance)
        {
            playerAnim.Play("playerWonFish2");
            Debug.Log("Animation Played: 2");
        }
        else if (chance < LegendaryChance)
        {
            playerAnim.Play("playerWonFish3");
            Debug.Log("Animation Played: 3");
        }
    }
}

using UnityEngine;

public class DayManager : MonoBehaviour
{
    public int maxDays = 4;
    public EnergyDepletion energyDepletion;
    public FishingProbability fishingProbability;
    public GameData gameData;

    private void Update()
    {
        if (energyDepletion.stopTimer)
        {
            ChangeDay();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Day: " + gameData.currentDay);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            gameData.currentDay = 1;
            Debug.Log("Day: " + gameData.currentDay);
        }
    }

    public void ChangeDay()
    {
        gameData.currentDay++;

        fishingProbability.ChangeSeason();

        // Check if we've reached the maximum number of days
        if (gameData.currentDay > maxDays)
        {
            //LOGIC TO END GAME
        }

        // Reset the energy
        energyDepletion.ResetEnergy();
        Debug.Log("Day changed to: " + gameData.currentDay);
    }
}

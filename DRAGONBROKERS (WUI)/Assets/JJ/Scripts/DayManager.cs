using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public int maxDays = 4;
    public EnergyDepletion energyDepletion;
    public FishingProbability fishingProbability;
    public GameData gameData;

    private void Start()
    {
        SeasonChange();
    }

    private void Update()
    {
        //Lock day to always start at 1
        if (gameData.currentDay == 0)
        {
            gameData.currentDay = 1;
        }

        if (energyDepletion.stopTimerDay)
        {
            ChangeDay();
            SeasonChange();
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

    public void SeasonChange()
    {
        if (gameData.currentDay == 1)
        {
            fishingProbability.SetSeason(FishingProbability.Season.Spring);
        }
        if (gameData.currentDay == 2)
        {
            fishingProbability.SetSeason(FishingProbability.Season.Summer);
        }
        if (gameData.currentDay == 3)
        {
            fishingProbability.SetSeason(FishingProbability.Season.Autumn);
        }
        if (gameData.currentDay == 4)
        {
            fishingProbability.SetSeason(FishingProbability.Season.Winter);
        }
    }

    public void ChangeDay()
    {
        gameData.currentDay++;

        // Check if we've reached the maximum number of days
        if (gameData.currentDay > maxDays)
        {
            SceneManager.LoadScene("Ending");
        }
        else
        {
            SceneManager.LoadScene("Home");
        }

        // Reset the energy
        energyDepletion.ResetEnergy();

        Debug.Log("Day changed to: " + gameData.currentDay);
    }
}

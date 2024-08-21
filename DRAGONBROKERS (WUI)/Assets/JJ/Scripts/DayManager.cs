using UnityEngine;

public class DayManager : MonoBehaviour
{
    public int currentDay = 1;
    public int maxDays = 4;
    public EnergyDepletion energyDepletion;
    public FishingProbability fishingProbability;

    //public GameObject bed;
    public GameObject player;

    private Collider2D bedCollider;

    private void Start()
    {

    }

    private void Update()
    {
        if (energyDepletion.stopTimer)
        {
            ChangeDay();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has voluntarily gone to bed
        if (other.gameObject == player)
        {
            ChangeDay();
        }
    }

    public void ChangeDay()
    {
        currentDay++;

        fishingProbability.ChangeSeason();

        // Check if we've reached the maximum number of days
        if (currentDay > maxDays)
        {
            //LOGIC TO END GAME
        }

        // Reset the energy
        energyDepletion.ResetEnergy();
        Debug.Log("Day changed to: " + currentDay);
    }
}

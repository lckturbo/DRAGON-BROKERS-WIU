using UnityEngine;

public class DayManager : MonoBehaviour
{
    public int currentDay = 1;
    public int maxDays = 4;
    public EnergyDepletion energyDepletion;
    public FishingProbability fishingProbability;

    //public GameObject bed; // Reference to the bed GameObject
    public GameObject player; // Reference to the player object

    private Collider2D bedCollider;

    private void Start()
    {
        // Get the bed's collider component
        //bedCollider = bed.GetComponent<Collider2D>();

        //if (bedCollider == null)
        //{
        //    Debug.LogError("No Collider2D found on the bed GameObject.");
        //    return;
        //}
    }

    private void Update()
    {
        // Check if energy has depleted
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
        // Increment the day
        currentDay++;

        fishingProbability.ChangeSeason();

        // Check if we've reached the maximum number of days
        if (currentDay > maxDays)
        {
            //LOGIC TO END GAME
        }

        // Reset the energy
        energyDepletion.ResetEnergy();

        // Optional: Trigger any day-related events or updates here
        Debug.Log("Day changed to: " + currentDay);
    }
}

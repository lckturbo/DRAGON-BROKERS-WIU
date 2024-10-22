using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnergyDepletion : MonoBehaviour
{
    public Slider energySlider;
    public EnergyData energyData; // Reference to the ScriptableObject
    public bool stopTimer = false;
    public bool stopTimerDay = false;

    public InventoryManager inventoryManager;
    public GoldManager goldManager;
    public FishFoodManager fishFoodManager;
    public FishingProbability fishingProbability;

    private void Start()
    {
        // Initialize energyTimer at 150 only if it's the first run
        if (energyData.currentEnergy <= 0)
        {
            energyData.currentEnergy = 150f;
        }

        // Set up the slider
        energySlider.maxValue = 150f;
        energySlider.value = energyData.currentEnergy;

        StartTimer();
    }
        
    private void Update()
    {
        // Check for 'L' key press to reset energy
        if (Input.GetKeyDown(KeyCode.L))
        {
            ResetEnergy();
            Debug.Log(energyData.currentEnergy);
        }
    }

    public void StartTimer()
    {
        StartCoroutine(TimeStart());
    }

    IEnumerator TimeStart()
    {
        while (!stopTimer)
        {
            energyData.currentEnergy -= Time.deltaTime;
            yield return null; // Update every frame

            if (energyData.currentEnergy <= 0)
            {
                inventoryManager.SaveInventory();
                goldManager.SaveGold();
                fishFoodManager.SaveFood();
                fishingProbability.SaveData();
                stopTimer = true;
                stopTimerDay = true;
            }

            if (!stopTimer)
            {
                energySlider.value = energyData.currentEnergy;
            }
        }
    }

        // Method to reset the energy back to full
    public void ResetEnergy()
    {
        energyData.currentEnergy = 150f;
        energySlider.value = energyData.currentEnergy;
        stopTimer = false; // Restart the timer if it was stopped
        stopTimerDay = false;
        StartTimer(); // Start the timer again
    }
}

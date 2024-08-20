using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnergyDepletion : MonoBehaviour
{
    public Slider energySlider;
    public float energyTimer;
    public bool stopTimer = false;
    public InventoryManager inventoryManager;
    public GoldManager goldManager;

    private void Start()
    {
        energySlider.maxValue = energyTimer;
        energySlider.value = energyTimer;
        StartTimer();
    }

    public void StartTimer()
    {
        StartCoroutine(TimeStart());
    }

    IEnumerator TimeStart()
    {
        while (stopTimer == false)
        {
            energyTimer -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);

            if (energyTimer <= 0)
            {
                inventoryManager.SaveInventory();
                goldManager.SaveGold();
                stopTimer = true;
            }

            if (stopTimer == false)
            {
                energySlider.value = energyTimer;
            }
        }
    }
}

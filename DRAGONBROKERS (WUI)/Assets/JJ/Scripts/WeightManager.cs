using UnityEngine;
using TMPro;

public class WeightManager : MonoBehaviour
{
    public TMP_Text weightText;

    public float MaxWeight = 100f;
    private float CurrentWeight = 0f;

    private void Update()
    {
        WeightText();
    }
    public float GetCurrentWeight()
    {
        return CurrentWeight;
    }

    //Check if Weight is over the limit
    public bool CanAddItem(float itemWeight, int quantity)
    {
        return CurrentWeight + (itemWeight * quantity) <= MaxWeight;
    }

    public void AddWeight(float itemWeight, int quantity)
    {
        CurrentWeight += itemWeight * quantity;
        Debug.Log($"Added {itemWeight * quantity} weight. Current total weight: {CurrentWeight}/{MaxWeight}");
    }

    public void RemoveWeight(float itemWeight, int quantity)
    {
        CurrentWeight -= itemWeight * quantity;
        Debug.Log($"Removed {itemWeight * quantity} weight. Current total weight: {CurrentWeight}/{MaxWeight}");
    }

    private void WeightText()
    {
        if (weightText != null)
        {
            weightText.text = $"Weight: {CurrentWeight}/{MaxWeight}";
        }
        else
        {
            Debug.LogWarning("Weight Text is not assigned!");
        }
    }
}

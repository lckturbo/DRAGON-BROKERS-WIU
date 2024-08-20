using TMPro;
using UnityEngine;

public class FishFoodManager : MonoBehaviour
{
    public int foodCount = 0;
    public TMP_Text foodText;
    public FoodData foodData; // This will reference the ScriptableObject to save/load gold

    private void Start()
    {
        LoadFood(); // Load gold data when the game starts
        UpdateFoodUI(); // Update the UI with the loaded gold value
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foodCount += 25;
            Debug.Log(foodCount);
            UpdateFoodUI(); // Update the UI every time the gold count changes
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foodCount = 0;
            Debug.Log(foodCount);
            UpdateFoodUI(); // Update the UI every time the gold count changes
        }

        UpdateFoodUI();
    }

    public void SaveFood()
    {
        // Save the current gold count to the ScriptableObject
        foodData.food = foodCount;
        Debug.Log("Food saved: " + foodCount);
    }

    public void LoadFood()
    {
        // Load the gold count from the ScriptableObject
        foodCount = foodData.food;
        Debug.Log("Food loaded: " + foodCount);
    }

    private void UpdateFoodUI()
    {
        // Update the UI text to reflect the current gold count
        foodText.text = foodCount.ToString();
    }
}

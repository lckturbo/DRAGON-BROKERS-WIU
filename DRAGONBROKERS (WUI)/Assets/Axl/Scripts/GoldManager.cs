using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public int goldCount = 0;
    public TMP_Text goldText;
    public GoldData goldData; // This will reference the ScriptableObject to save/load gold

    private void Start()
    {
        LoadGold(); // Load gold data when the game starts
        UpdateGoldUI(); // Update the UI with the loaded gold value
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            goldCount += 25;
            Debug.Log(goldCount);
            UpdateGoldUI(); // Update the UI every time the gold count changes
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            goldCount = 0;
            Debug.Log(goldCount);
            UpdateGoldUI(); // Update the UI every time the gold count changes
        }

        UpdateGoldUI();
    }

    public void SaveGold()
    {
        // Save the current gold count to the ScriptableObject
        goldData.gold = goldCount;
        Debug.Log("Gold saved: " + goldCount);
    }

    public void LoadGold()
    {
        // Load the gold count from the ScriptableObject
        goldCount = goldData.gold;
        Debug.Log("Gold loaded: " + goldCount);
    }

    private void UpdateGoldUI()
    {
        // Update the UI text to reflect the current gold count
        goldText.text = goldCount.ToString();
    }
}

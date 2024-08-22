using UnityEngine;

[CreateAssetMenu(fileName = "UpgradesData", menuName = "Upgrades/UpgradesData")]
public class BuyItemsData : ScriptableObject
{
    public static BuyItemsData Instance { get; private set; }

    public bool poisonActive;
    public bool trawlActive;
    public bool IncreaseWeight;
    public bool PlatRod;
    public bool EmeRod;
    public bool IRod;

    private void OnEnable()
    {
        // Ensure there is only one instance of this ScriptableObject
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }
}

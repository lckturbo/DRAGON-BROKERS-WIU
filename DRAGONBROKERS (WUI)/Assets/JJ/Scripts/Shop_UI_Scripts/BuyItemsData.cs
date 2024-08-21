using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradesData", menuName = "Upgrades/UpgradesData")]
public class BuyItemsData : ScriptableObject
{
    public bool poisonActive = false;
    public bool trawlActive = false;
    public bool IncreaseWeight = false;
    public bool PlatRod = false;
    public bool EmeRod = false;
    public bool IRod = false;
}

[System.Serializable]
public class PlayerUpgrades
{
    public bool poisonActive = false;
    public bool trawlActive = false;
    public bool IncreaseWeight = false;
    public bool PlatRod = false;
    public bool EmeRod = false;
    public bool IRod = false;
}

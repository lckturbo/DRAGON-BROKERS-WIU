using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoldData", menuName = "Gold/GoldData")]
public class GoldData : ScriptableObject
{
    public int gold;  // This will store the player's gold
}

[System.Serializable]
public class PlayerGold
{
    public int gold;
}

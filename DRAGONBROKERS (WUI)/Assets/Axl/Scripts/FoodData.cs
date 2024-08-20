using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "Food/FoodData")]
public class FoodData : ScriptableObject
{
    public int food;  // This will store the player's gold
}

[System.Serializable]
public class PlayerFood
{
    public int food;
}

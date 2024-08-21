using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public FishingProbability.Environment currentEnvironment;
    public FishingProbability.Season currentSeason;
    public int currentDay;
}

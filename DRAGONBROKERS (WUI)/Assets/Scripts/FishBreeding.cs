using UnityEngine;

public class FishBreeding : MonoBehaviour
{
    public FishSpawner fishSpawner;
    public float breedingInterval = 10f; // Time between breeding checks

    private void Start()
    {
        InvokeRepeating("CheckBreedingConditions", breedingInterval, breedingInterval);
    }

    private void CheckBreedingConditions()
    {
        // Implement your breeding logic here (e.g., proximity of fishes, random chance)
        // If conditions are met:
        fishSpawner.SpawnFish();
    }
}

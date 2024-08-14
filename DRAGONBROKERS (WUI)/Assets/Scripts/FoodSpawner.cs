using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public Transform spawnPoint;

    public void SpawnFood()
    {
        Instantiate(foodPrefab, spawnPoint.position, Quaternion.identity);
    }
}
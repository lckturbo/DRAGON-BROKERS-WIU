using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public Transform spawnArea;

    public void SpawnFish()
    {
        Vector2 randomPosition = new Vector2(
            Random.Range(spawnArea.position.x - 5f, spawnArea.position.x + 5f),
            Random.Range(spawnArea.position.y - 3f, spawnArea.position.y + 3f)
        );

        Instantiate(fishPrefab, randomPosition, Quaternion.identity);
    }
}

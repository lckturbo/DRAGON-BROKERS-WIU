using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public GameObject foodPrefab;  // Drag your food prefab here in the Inspector
    public Transform spawnPoint;   // Define where the food will spawn

    // This method will be called by the button's onClick event
    public void DropFood()
    {
        Instantiate(foodPrefab, spawnPoint.position, Quaternion.identity);
    }
}

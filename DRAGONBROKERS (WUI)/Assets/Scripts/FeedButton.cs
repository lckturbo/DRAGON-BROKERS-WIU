using UnityEngine;

public class FeedButton : MonoBehaviour
{
    public FoodSpawner foodSpawner;

    public void OnFeedButtonClick()
    {
        foodSpawner.SpawnFood();
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition3 : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorageposition;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;
    public InventoryManager inventoryManager;
    public GoldManager goldManager;
    public FishFoodManager fishFoodManager;
    public FishingProbability fishingProbability;

    public void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    // Call this method from the button click event
    public void OnButtonClick()
    {
        StartCoroutine(FadeCo());
    }

    private IEnumerator FadeCo()
    {
        // Save the player's inventory, gold, and position before transitioning
        inventoryManager.SaveInventory();
        goldManager.SaveGold();
        fishFoodManager.SaveFood();
        fishingProbability.SaveData();
        playerStorageposition.initialValue = playerPosition;

        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }

        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}

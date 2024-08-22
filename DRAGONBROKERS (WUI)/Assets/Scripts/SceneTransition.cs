using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
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
    public BuyItems buyItems;

    public void Awake()
    {
        if(fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            inventoryManager.SaveInventory();
            goldManager.SaveGold();
            fishFoodManager.SaveFood();
            fishingProbability.SaveData();
            playerStorageposition.initialValue = playerPosition;
            StartCoroutine(FadeCo());
        }
    }

    public IEnumerator FadeCo()
    {
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

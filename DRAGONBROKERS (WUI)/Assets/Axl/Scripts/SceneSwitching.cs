using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour
{
    public int sceneIndex;
    private EnergyDepletion energyDepletion;
    public InventoryManager inventoryManager;

    private void Start()
    {
        energyDepletion = GetComponent<EnergyDepletion>();

        if (energyDepletion == null)
        {
            Debug.LogError("EnergyDepletion component not found on this GameObject!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryManager.SaveInventory();
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }

        if (energyDepletion != null && energyDepletion.stopTimer == true)
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
            Debug.Log("Scene switched to index: " + sceneIndex);
        }
    }
}

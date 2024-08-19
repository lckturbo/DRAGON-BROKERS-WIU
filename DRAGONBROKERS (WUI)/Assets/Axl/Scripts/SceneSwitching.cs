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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryManager.SaveInventory();
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }

        if (energyDepletion.stopTimer == true)
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}

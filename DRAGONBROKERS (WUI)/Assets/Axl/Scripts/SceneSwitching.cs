using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour
{
    public int sceneIndex;
    private EnergyDepletion energyDepletion;

    private void Start()
    {
        energyDepletion = GetComponent<EnergyDepletion>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }

        if (energyDepletion.stopTimer == true)
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}

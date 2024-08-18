using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
}
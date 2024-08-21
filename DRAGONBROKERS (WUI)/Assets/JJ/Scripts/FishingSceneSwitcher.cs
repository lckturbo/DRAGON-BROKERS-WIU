using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelActivator : MonoBehaviour
{
    public GameObject panel;           // The panel to be activated
    public Button button1;             // The first button
    public Button button2;             // The second button
    public Button button3;             // The third button

    public string sceneForButton1;     // The scene name for button1
    public string sceneForButton2;         // The scene name for button2
    public string sceneForButton3;       // The scene name for button3

    private void Start()
    {
        // Initially, the panel is inactive
        panel.SetActive(false);

        // Make sure button2 and button3 are inactive initially
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);

        // Add listeners to the buttons
        button1.onClick.AddListener(OnButton1Clicked);
        button2.onClick.AddListener(OnButton2Clicked);
        button3.onClick.AddListener(OnButton3Clicked);
    }

    private void Update()
    {
        // Toggle the first boolean with the 7 key
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            button2.gameObject.SetActive(true);
        }

        // Toggle the second boolean with the 8 key
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            button3.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the area
        if (other.CompareTag("Player"))
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.Log("Trigger detected, but the object is not tagged as 'Player'.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player left the area
        if (other.CompareTag("Player"))
        {
            panel.SetActive(false);
        }
    }

    private void OnButton1Clicked()
    {
        Debug.Log("Button1 clicked, switching to scene: " + sceneForButton1);
        SceneManager.LoadScene(sceneForButton1);
    }

    private void OnButton2Clicked()
    {
        Debug.Log("Button2 clicked, switching to scene: " + sceneForButton2);
        SceneManager.LoadScene(sceneForButton2);
    }

    private void OnButton3Clicked()
    {
        Debug.Log("Button3 clicked, switching to scene: " + sceneForButton3);
        SceneManager.LoadScene(sceneForButton3);
    }
}

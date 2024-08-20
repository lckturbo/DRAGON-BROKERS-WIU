using UnityEngine;
using UnityEngine.UI;

public class PanelActivator : MonoBehaviour
{
    public GameObject panel;           // The panel to be activated
    public Button button1;             // The first button
    public Button button2;             // The second button
    public Button button3;             // The third button

    private void Start()
    {
        // Initially, the panel is inactive
        panel.SetActive(false);

        // Make sure button2 and button3 are inactive initially
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
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
        if (other.CompareTag("Port"))
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.Log("Trigger detected, but the object is not tagged as 'Port'");
        }
    }
}

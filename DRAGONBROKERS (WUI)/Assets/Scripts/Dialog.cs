using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public SignalGame contextOn;
    public SignalGame contextOff;

    public GameObject dialogBox;
    public Text dialogText;
    public List<string> dialogues; // List to hold multiple lines of dialogue
    private int currentDialogueIndex = 0; // Index to keep track of the current line
    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        if (dialogues.Count > 0)
        {
            dialogText.text = dialogues[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                if (currentDialogueIndex < dialogues.Count - 1)
                {
                    currentDialogueIndex++;
                    dialogText.text = dialogues[currentDialogueIndex];
                }
                else
                {
                    dialogBox.SetActive(false);
                    currentDialogueIndex = 0; // Reset to the first line if needed
                }
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialogues[currentDialogueIndex];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger area.");
            contextOn.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger area.");
            contextOff.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }

}

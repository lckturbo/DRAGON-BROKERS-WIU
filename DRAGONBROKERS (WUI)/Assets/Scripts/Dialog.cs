using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public GameObject dialogBox;       // The dialog box UI element
    public Text dialogText;            // The UI text component for the dialog
    public List<string> dialogues;     // List to hold multiple lines of dialogue
    public GameObject contextSignal;   // The context signal UI element

    private int currentDialogueIndex = 0; // Index to keep track of the current line
    public bool playerInRange;

    void Start()
    {
        if (dialogues.Count > 0)
        {
            dialogText.text = dialogues[0];
        }

        // Make sure the dialog box and context signal are hidden initially
        dialogBox.SetActive(false);
        contextSignal.SetActive(false);
    }

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
                    contextSignal.SetActive(true); // Show the context signal again after dialog ends
                }
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialogues[currentDialogueIndex];
                contextSignal.SetActive(false); // Hide the context signal when dialog starts
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger area.");
            playerInRange = true;
            contextSignal.SetActive(true); // Show the context signal when the player enters
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger area.");
            playerInRange = false;
            dialogBox.SetActive(false);
            contextSignal.SetActive(false); // Hide the context signal when the player leaves
        }
    }
}

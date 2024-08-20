using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrawlingItem : MonoBehaviour
{
    public string itemName;
    public int quantity;
    public Sprite sprite;
    [TextArea] public string itemDescription;

    public int worth;
    public float weight;

    private InventoryManager inventoryManager;
    public bool fishCaught = false;

    private AudioSource sfxAudioSrc;
    [SerializeField] private AudioClip splashAudioClip;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory Canvas Variant").GetComponent<InventoryManager>();
        sfxAudioSrc = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the surface
        if (collision.gameObject.CompareTag("FishingSurface")) // Make sure your surface GameObject has the tag "Surface"
        {
            if (!sfxAudioSrc.isPlaying)
            {
                Debug.Log("audio playing");
                sfxAudioSrc.clip = splashAudioClip;
                sfxAudioSrc.Play();
            }

            inventoryManager.AddItem(itemName, quantity, sprite, itemDescription, worth, weight);
            fishCaught = true;
            Debug.Log("Caught fish");
            Destroy(gameObject, splashAudioClip.length); // Adjust the delay if necessary
        }
    }
}

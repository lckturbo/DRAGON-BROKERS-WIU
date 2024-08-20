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

    public GameObject winEffect;
    private ParticleSystem winParticleSystem;
    public Vector2 winEffectOffset = new Vector2(1, 1);  // Offset distance for the win effect

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
            inventoryManager.AddItem(itemName, quantity, sprite, itemDescription, worth, weight);
            fishCaught = true;
            Debug.Log("Caught fish");

            if (!sfxAudioSrc.isPlaying)
            {
                Debug.Log("audio playing");
                sfxAudioSrc.clip = splashAudioClip;
                sfxAudioSrc.Play();
            }

            // Instantiate the win effect at a position relative to the fish, using the offset from the Inspector
            Vector3 winEffectPosition = transform.position + (Vector3)winEffectOffset;
            GameObject effectInstance = Instantiate(winEffect, winEffectPosition, Quaternion.identity);

            // Destroy the win effect after 3 seconds
            Destroy(effectInstance, 3f);

            // Destroy the fish GameObject
            Destroy(gameObject, splashAudioClip.length);
        }
    }
}

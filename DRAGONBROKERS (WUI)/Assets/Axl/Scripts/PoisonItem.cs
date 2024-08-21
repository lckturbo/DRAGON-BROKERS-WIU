using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonItem : MonoBehaviour
{
    public string itemName;
    public int quantity;
    public Sprite sprite;
    [TextArea] public string itemDescription;

    public int worth;
    public float weight;

    private InventoryManager inventoryManager;
    public FishingProbability fishingProbability; // Reference to FishingProbability script
    public bool fishCaught = false;

    public GameObject winEffect;
    private ParticleSystem winParticleSystem;
    public Vector2 winEffectOffset = new Vector2(1, 1);  // Offset distance for the win effect

    private AudioSource sfxAudioSrc;
    [SerializeField] private AudioClip splashAudioClip;

    public static int poisonFishCaught = 0;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory Canvas Variant").GetComponent<InventoryManager>();
        fishingProbability = GameObject.FindObjectOfType<FishingProbability>(); // Find the FishingProbability component
        sfxAudioSrc = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the surface
        if (collision.gameObject.CompareTag("FishingSurface"))
        {
            if (!sfxAudioSrc.isPlaying)
            {
                sfxAudioSrc.clip = splashAudioClip;
                sfxAudioSrc.Play();
            }

            poisonFishCaught++;
            Debug.Log("I WANT TO TOUCH YOU: " + poisonFishCaught);

            // Get the random fish/item from FishingProbability
            string caughtFishName = fishingProbability.FishingChance();

            // Based on the caughtFishName, determine the item properties
            switch (caughtFishName)
            {
                case "Spring Fish":
                    itemName = fishingProbability.springFishName;
                    quantity = fishingProbability.springFishQuantity;
                    sprite = fishingProbability.springFishSprite;
                    itemDescription = fishingProbability.springFishDescription;
                    worth = fishingProbability.springFishWorth;
                    weight = fishingProbability.springFishWeight;
                    break;
                case "Summer Fish":
                    itemName = fishingProbability.summerFishName;
                    quantity = fishingProbability.summerFishQuantity;
                    sprite = fishingProbability.summerFishSprite;
                    itemDescription = fishingProbability.summerFishDescription;
                    worth = fishingProbability.summerFishWorth;
                    weight = fishingProbability.summerFishWeight;
                    break;
                case "Autumn Fish":
                    itemName = fishingProbability.autumnFishName;
                    quantity = fishingProbability.autumnFishQuantity;
                    sprite = fishingProbability.autumnFishSprite;
                    itemDescription = fishingProbability.autumnFishDescription;
                    worth = fishingProbability.autumnFishWorth;
                    weight = fishingProbability.autumnFishWeight;
                    break;
                case "Winter Fish":
                    itemName = fishingProbability.winterFishName;
                    quantity = fishingProbability.winterFishQuantity;
                    sprite = fishingProbability.winterFishSprite;
                    itemDescription = fishingProbability.winterFishDescription;
                    worth = fishingProbability.winterFishWorth;
                    weight = fishingProbability.winterFishWeight;
                    break;
                case "Rare Fish":
                    itemName = fishingProbability.rareItemName;
                    quantity = fishingProbability.rareQuantity;
                    sprite = fishingProbability.rareSprite;
                    itemDescription = fishingProbability.rareItemDescription;
                    worth = fishingProbability.rworth;
                    weight = fishingProbability.rWeight;
                    break;
                case "Legendary Fish":
                    itemName = fishingProbability.legendaryItemName;
                    quantity = fishingProbability.legendaryQuantity;
                    sprite = fishingProbability.legendarySprite;
                    itemDescription = fishingProbability.legendaryItemDescription;
                    worth = fishingProbability.lworth;
                    weight = fishingProbability.lWeight;
                    break;
                case "No Fish":
                    Debug.Log("No fish caught");
                    return; // No fish was caught, so exit the method
            }

            inventoryManager.AddItem(itemName, quantity, sprite, itemDescription, worth, weight);
            fishCaught = true;
            Debug.Log($"Caught fish: {itemName}");

            // Instantiate the win effect at a position relative to the fish, using the offset from the Inspector
            Vector3 winEffectPosition = transform.position + (Vector3)winEffectOffset;
            GameObject effectInstance = Instantiate(winEffect, winEffectPosition, Quaternion.identity);

            // Destroy the win effect after 3 seconds
            Destroy(effectInstance, 3f);

            // Destroy the fish GameObject
            Destroy(gameObject);
        }
    }
}

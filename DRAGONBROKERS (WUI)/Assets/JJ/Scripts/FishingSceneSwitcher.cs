using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelActivator : MonoBehaviour
{
    public GameObject panel;           // The panel to be activated
    public Button button1;             // The first button
    public Button button2;             // The second button
    public Button button3;             // The third button

    public InventoryManager inventoryManager;
    public GoldManager goldManager;
    public FishFoodManager fishFoodManager;
    public FishingProbability fishingProbability;
    public BuyItems buyItems;

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

        inventoryManager = GameObject.Find("Inventory Canvas Variant").GetComponent<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found!");
        }

        goldManager = GameObject.FindObjectOfType<GoldManager>();
        if (goldManager == null)
        {
            Debug.LogError("GoldManager not found!");
        }

        fishFoodManager = GameObject.FindObjectOfType<FishFoodManager>();
        if (fishFoodManager == null)
        {
            Debug.LogError("FishFoodManager not found!");
        }

        fishingProbability = GameObject.FindObjectOfType<FishingProbability>();
        if (fishingProbability == null)
        {
            Debug.LogError("fishingProbability not found!");
        }

        buyItems = GameObject.FindObjectOfType<BuyItems>();
        if (buyItems == null)
        {
            Debug.Log("BuyItems not found");
        }
    }

    private void Update()
    {
        // Toggle the first boolean with the 7 key
        if (buyItems.trawlActive == true)
        {
            button2.gameObject.SetActive(true);
        }

        // Toggle the second boolean with the 8 key
        if (buyItems.poisonActive == true)
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
            //Debug.Log("Trigger detected, but the object is not tagged as 'Player'.");
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
        Debug.Log("Button1 clicked, switching to scene: ");

        inventoryManager.SaveInventory();
        goldManager.SaveGold();
        fishFoodManager.SaveFood();
        fishingProbability.SaveData();

        SceneManager.LoadScene("FishingRodScene");
    }

    private void OnButton2Clicked()
    {
        Debug.Log("Button2 clicked, switching to scene: ");

        inventoryManager.SaveInventory();
        goldManager.SaveGold();
        fishFoodManager.SaveFood();
        fishingProbability.SaveData();

        SceneManager.LoadScene("FishingPort");
    }

    private void OnButton3Clicked()
    {
        Debug.Log("Button3 clicked, switching to scene: ");

        inventoryManager.SaveInventory();
        goldManager.SaveGold();
        fishFoodManager.SaveFood();
        fishingProbability.SaveData();

        SceneManager.LoadScene("FishingPoison");
    }
}

using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FishManager : MonoBehaviour
{
    public int commonFishCount = 0;
    public TMP_Text commonFishText;
    public int rareFishCount = 0;
    public TMP_Text rareFishText;
    public Sprite commonFishSprite;
    public Sprite rareFishSprite;
    public InventoryManager inventoryManager;
    public FishingProbability fishingProbability;
    public string commonFish;
    public string rareFish;
    public GameObject seasonalFish;


    //To add to other scripts
    //public fishManager _fishManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory Canvas Variant").GetComponent<InventoryManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            commonFishCount++;
            inventoryManager.AddItem(commonFish, 1, commonFishSprite);
            Debug.Log(commonFishCount);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            rareFishCount++;
            inventoryManager.AddItem(rareFish, 1, rareFishSprite);
            Debug.Log(rareFishCount);
        }

        commonFishText.text = commonFishCount.ToString();
        rareFishText.text = rareFishCount.ToString();
    }
}

using UnityEngine;

public class UpgradeBar : MonoBehaviour
{
    public GameObject Bar;
    public BuyItems buyItems;

    private bool scaleDoubled = false;
    private bool scaleTripled = false;

    void Start()
    {
        buyItems.GetComponent<BuyItems>();
        if (buyItems == null)
        {
            Debug.LogError("BuyItems reference is not assigned.");
        }

        if (Bar == null)
        {
            Debug.LogError("Bar is not assigned.");
        }
    }

    void Update()
    {
        if (!scaleDoubled && buyItems.buyItemsData.PlatRod)
        {
            // Increase Size by x2
            Vector3 currentScale = Bar.transform.localScale;
            currentScale.y *= 2;  // Double the y-scale
            Bar.transform.localScale = currentScale;  // Apply the updated scale
            scaleDoubled = true;
            Debug.Log("PlatRod active: Scale doubled.");
        }

        if (!scaleTripled && buyItems.buyItemsData.IRod)
        {
            // Increase Size to the fullest
            Vector3 currentScale = Bar.transform.localScale;
            currentScale.y *= 3;  // Triple the y-scale
            Bar.transform.localScale = currentScale;  // Apply the updated scale
            scaleTripled = true;
            Debug.Log("IRod active: Scale tripled.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBar : MonoBehaviour
{
    //public BuyItems buyItems;

    public GameObject Bar;

    // Start is called before the first frame update
    void Start()
    {
        //buyItems = GetComponent<BuyItems>();
        //if (buyItems == null )
        //{
        //    Debug.Log("BuyItems cant be found");
        //}

        if (Bar == null)
        {
            Debug.Log("Bar is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) //buyItems.PlatRod
        {
            // Increase Size by x2
            Vector3 currentScale = Bar.transform.localScale;
            currentScale.y *= 2;  // Double the y-scale
            Bar.transform.localScale = currentScale;  // Apply the updated scale
            Debug.Log("U pressed: Scale doubled.");
        }

        if (Input.GetKeyDown(KeyCode.I)) //buyItems.IRod
        {
            // Increase Size to the fullest
            Vector3 currentScale = Bar.transform.localScale;
            currentScale.y *= 3;  // Triple the y-scale
            Bar.transform.localScale = currentScale;  // Apply the updated scale
            Debug.Log("I pressed: Scale tripled.");
        }
    }
}

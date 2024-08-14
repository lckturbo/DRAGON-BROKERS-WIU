using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public int goldCount = 0;
    public TMP_Text goldText;

    //To add to other scripts
    //public GoldManager _goldManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            goldCount += 25;
            Debug.Log(goldCount);
        }

        goldText.text = goldCount.ToString();
    }
}

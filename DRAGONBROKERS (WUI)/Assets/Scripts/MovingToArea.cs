using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MovingToArea : MonoBehaviour
{
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    // Static flags to manage transitions
    private static bool enteringSentosa = false;
    private static bool enteringLakeside = false;
    private static bool enteringVillageTown = false;
    private static bool enteringMineForest = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Sentosa transitions
            if (placeName == "Sentosa" && !enteringSentosa)
            {
                enteringSentosa = true;
                enteringLakeside = false;
                enteringVillageTown = false;
                enteringMineForest = false;
                if (needText)
                {
                    StartCoroutine(placeNameCo());
                }
            }
            else if (placeName == "Singapore" && enteringSentosa)
            {
                enteringSentosa = false;
                if (needText)
                {
                    StartCoroutine(placeNameCo());
                }
            }

            // Lakeside transitions
            else if (placeName == "Lakeside" && !enteringLakeside)
            {
                enteringLakeside = true;
                enteringSentosa = false;
                enteringVillageTown = false;
                enteringMineForest = false;
                if (needText)
                {
                    StartCoroutine(placeNameCo());
                }
            }
            else if (placeName == "Singapore" && enteringLakeside)
            {
                enteringLakeside = false;
                if (needText)
                {
                    StartCoroutine(placeNameCo());
                }
            }

            // VillageTown transitions
            else if (placeName == "VillageTown" && !enteringVillageTown)
            {
                enteringVillageTown = true;
                enteringSentosa = false;
                enteringLakeside = false;
                enteringMineForest = false;
                if (needText)
                {
                    StartCoroutine(placeNameCo());
                }
            }
            else if (placeName == "Singapore" && enteringVillageTown)
            {
                enteringVillageTown = false;
                if (needText)
                {
                    StartCoroutine(placeNameCo());
                }
            }

            // Mine/Forest transitions
            else if (placeName == "Mine/Forest" && !enteringMineForest)
            {
                enteringMineForest = true;
                enteringSentosa = false;
                enteringLakeside = false;
                enteringVillageTown = false;
                if (needText)
                {
                    StartCoroutine(placeNameCo());
                }
            }
            else if (placeName == "SINGLISH" && enteringMineForest)
            {
                enteringMineForest = false;
                if (needText)
                {
                    StartCoroutine(placeNameCo());
                }
            }
        }
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;

        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}

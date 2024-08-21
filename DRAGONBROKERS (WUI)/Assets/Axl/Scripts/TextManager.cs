using UnityEngine.SceneManagement;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public GameObject overFishingText;
    public GameObject trawlingText;
    public GameObject poisonText;
    public GameObject ruralText;
    public GameObject sustainableText;
    public GameObject endText;

    private FadingText fadingText;

    private void Start()
    {
        overFishingText.SetActive(true);
        trawlingText.SetActive(false);
        poisonText.SetActive(false);
        ruralText.SetActive(false);
        sustainableText.SetActive(false);
        endText.SetActive(false);

        fadingText = overFishingText.GetComponent<FadingText>();
        fadingText.StartFading(this);
    }

    public void OnTextFadeComplete(GameObject fadedText)
    {
        if (fadedText == overFishingText)
        {
            trawlingText.SetActive(true);
            fadingText = trawlingText.GetComponent<FadingText>();
            fadingText.StartFading(this);
        }
        else if (fadedText == trawlingText)
        {
            poisonText.SetActive(true);
            fadingText = poisonText.GetComponent<FadingText>();
            fadingText.StartFading(this);
        }
        else if (fadedText == poisonText)
        {
            ruralText.SetActive(true);
            fadingText = ruralText.GetComponent<FadingText>();
            fadingText.StartFading(this);
        }
        else if (fadedText == ruralText)
        {
            sustainableText.SetActive(true);
            fadingText = sustainableText.GetComponent<FadingText>();
            fadingText.StartFading(this);
        }
        else if (fadedText == sustainableText)
        {
            endText.SetActive(true);
            fadingText = endText.GetComponent<FadingText>();
            fadingText.StartFading(this);
        }
        else
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}

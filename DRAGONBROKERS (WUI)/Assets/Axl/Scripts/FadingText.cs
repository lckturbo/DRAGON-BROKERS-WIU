using TMPro;
using UnityEngine;

public class FadingText : MonoBehaviour
{
    public float fadeTime;
    private TextMeshProUGUI fadingText;
    private float alphaValue;
    private float fadePerSecond;
    private TextManager textManager;

    private void Start()
    {
        fadingText = GetComponent<TextMeshProUGUI>();
        fadePerSecond = 1 / fadeTime;
        alphaValue = fadingText.color.a;
    }

    public void StartFading(TextManager manager)
    {
        textManager = manager;
    }

    private void Update()
    {
        if (fadeTime > 0)
        {
            alphaValue -= fadePerSecond * Time.deltaTime;
            fadingText.color = new Color(fadingText.color.r, fadingText.color.g, fadingText.color.b, alphaValue);
            fadeTime -= Time.deltaTime;
        }
        else
        {
            textManager.OnTextFadeComplete(gameObject);
            gameObject.SetActive(false);
        }
    }
}

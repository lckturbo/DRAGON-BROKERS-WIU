using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverCheck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject additionalUI;

    void Start()
    {
        additionalUI.SetActive(false); // Ensure the UI is hidden initially
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        additionalUI.SetActive(true); // Show the additional UI when hovering over the button
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        additionalUI.SetActive(false); // Hide the additional UI when the mouse leaves the button
    }
}

using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public GameObject rodsScrollView;   // Assign the Scroll View for Rods
    public GameObject boatsScrollView;  // Assign the Scroll View for Boats
    public GameObject workersScrollView;  // Assign the Scroll View for Workers

    private GameObject currentOpenScrollView = null;

    // These functions will be linked to each tab button
    public void ToggleRodsView()
    {
        ToggleScrollView(rodsScrollView);
    }

    public void ToggleBoatsView()
    {
        ToggleScrollView(boatsScrollView);
    }

    public void ToggleWorkersView()
    {
        ToggleScrollView(workersScrollView);
    }

    private void ToggleScrollView(GameObject scrollView)
    {
        if (currentOpenScrollView == scrollView)
        {
            // If the current scroll view is already open, close it
            scrollView.SetActive(false);
            currentOpenScrollView = null;
        }
        else
        {
            // Close the currently open scroll view
            if (currentOpenScrollView != null)
            {
                currentOpenScrollView.SetActive(false);
            }

            // Open the selected scroll view
            scrollView.SetActive(true);
            currentOpenScrollView = scrollView;
        }
    }
}

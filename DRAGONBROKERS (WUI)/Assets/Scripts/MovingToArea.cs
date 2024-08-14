using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingToArea : MonoBehaviour
{
    public Vector2 cameraChange;  // Offset for the camera position change
    public Vector3 playerChange;  // Offset for the player position change
    public Vector2 newMinCameraPosition; // New min position for the camera in the new area
    public Vector2 newMaxCameraPosition; // New max position for the camera in the new area
    private CameraMovement cam;  // Reference to the CameraMovement script
    public bool needText;

    public string placeName;
    public GameObject text;
    public Text placeText;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Update camera's min and max positions
            cam.minPosition = newMinCameraPosition;
            cam.maxPosition = newMaxCameraPosition;

            // Move the player by the defined offset
            other.transform.position += playerChange;
        }
    }
}

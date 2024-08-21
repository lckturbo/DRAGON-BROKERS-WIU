using UnityEngine;

public class Teleporting : MonoBehaviour
{
    public EnergyDepletion _energy;
    public GameObject waypoint;

    private void Start()
    {
        if (_energy == null)
        {
            Debug.LogError("EnergyDepletion reference is not assigned!");
        }

        if (waypoint == null)
        {
            Debug.LogError("Waypoint reference is not assigned!");
        }
    }

    private void Update()
    {
        if (_energy != null && _energy.stopTimer == true)
        {
            transform.position = waypoint.transform.position;
            Debug.Log("Teleported to waypoint: " + waypoint.transform.position);
        }
    }
}

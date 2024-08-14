using UnityEngine;

public class Teleporting : MonoBehaviour
{
    public EnergyDepletion _energy;
    public GameObject waypoint;

    private void Update()
    {
        if (_energy.GetComponent<EnergyDepletion>().stopTimer == true)
        {
            transform.position = waypoint.transform.position;
        }
    }
}

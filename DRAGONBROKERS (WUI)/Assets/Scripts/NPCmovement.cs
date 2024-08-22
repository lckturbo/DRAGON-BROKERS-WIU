using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints;  // Array to hold all the waypoints
    public float moveSpeed = 2f;   // Speed at which the NPC moves
    private int currentWaypointIndex = 0;  // Index of the current waypoint

    void Update()
    {
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        if (waypoints.Length == 0) return;

        // Move towards the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        // Flip the NPC sprite based on movement direction
        if (direction.x > 0 && transform.localScale.x < 0 || direction.x < 0 && transform.localScale.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;  // Flip the sprite by inverting the x scale
            transform.localScale = scale;
        }

        // Check if the NPC has reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;  // Move to the next waypoint
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;  // Loop back to the first waypoint
            }
        }
    }
}

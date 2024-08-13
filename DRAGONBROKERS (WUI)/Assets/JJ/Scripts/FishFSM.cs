using System.Collections;
using UnityEngine;

public class FishFSM : MonoBehaviour
{
    public float patrolTime = 2.0f;
    public float swimSpeed = 2.0f;

    private Vector3 targetPosition;
    private string fishType;

    private enum FishState
    {
        Patrolling,
        SwimmingToDespawn
    }

    private FishState currentState;

    private void Start()
    {
        currentState = FishState.Patrolling;
        StartCoroutine(Patrol());
    }

    public void SetFishType(string type)
    {
        fishType = type;
        // Modify the fish's appearance or behavior based on the type
        if (fishType == "No Fish")
        {
            // Modify behavior for "No Fish" (e.g., lower speed, different color)
            swimSpeed *= 0.8f; // Example: slower swimming speed
            GetComponent<SpriteRenderer>().color = Color.gray; // Example: change color to gray
        }
    }

    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
    }

    private IEnumerator Patrol()
    {
        // Patrolling state logic (random movement within a small area)
        Vector3 patrolTarget = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

        float elapsedTime = 0f;
        while (elapsedTime < patrolTime)
        {
            transform.position = Vector3.Lerp(transform.position, patrolTarget, elapsedTime / patrolTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // After patrolling, switch to SwimmingToDespawn state
        currentState = FishState.SwimmingToDespawn;
    }

    private void Update()
    {
        if (currentState == FishState.SwimmingToDespawn)
        {
            // Move towards the target position (opposite edge of the screen)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, swimSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                Destroy(gameObject); // Despawn when the fish reaches the target position
            }
        }
    }
}

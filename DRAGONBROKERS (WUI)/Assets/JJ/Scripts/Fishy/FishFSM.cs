using System.Collections;
using UnityEngine;

public class FishFSM : MonoBehaviour
{
    public float swimSpeed = 2.0f;
    public float patrolTime = 2.0f;

    private Vector3 initialTargetPosition;  // The target in the middle of the screen
    private Vector3 finalTargetPosition;    // The target at the edge of the screen
    private string fishType;

    private enum FishState
    {
        SwimMiddle,
        Patrolling,
        SwimmingToDespawn
    }

    private FishState currentState;

    private void Start()
    {
        currentState = FishState.SwimMiddle;
        SetRandomMiddleTarget();
        SetFinalTargetPosition();
    }

    public void SetFishType(string type)
    {
        fishType = type;
        // Modify the fish's appearance or behavior based on the type
        if (fishType == "No Fish")
        {
            swimSpeed *= 0.8f; // Example: slower swimming speed
            GetComponent<SpriteRenderer>().color = Color.gray; // Example: change color to gray
        }
    }

    private void SetRandomMiddleTarget()
    {
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Camera.main.aspect;

        initialTargetPosition = new Vector3(
            Random.Range(-screenWidth / 4, screenWidth / 4),
            Random.Range(-screenHeight / 4, screenHeight / 4),
            0
        );
    }

    private void SetFinalTargetPosition()
    {
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Camera.main.aspect;

        // Determine the final target based on where the fish starts
        if (transform.position.x < 0)
        {
            finalTargetPosition = new Vector3(screenWidth / 2 + 1, transform.position.y, 0); // Swim to the right
        }
        else
        {
            finalTargetPosition = new Vector3(-screenWidth / 2 - 1, transform.position.y, 0); // Swim to the left
        }
    }

    private IEnumerator Patrol()
    {
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
        switch (currentState)
        {
            case FishState.SwimMiddle:
                // Move towards the middle of the screen
                transform.position = Vector3.MoveTowards(transform.position, initialTargetPosition, swimSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, initialTargetPosition) < 0.1f)
                {
                    // When reached, transition to Patrolling state
                    currentState = FishState.Patrolling;
                    StartCoroutine(Patrol());
                }
                break;

            case FishState.Patrolling:
                // Patrolling logic is handled in the Coroutine
                break;

            case FishState.SwimmingToDespawn:
                // Move towards the final target position (opposite edge of the screen)
                transform.position = Vector3.MoveTowards(transform.position, finalTargetPosition, swimSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, finalTargetPosition) < 0.1f)
                {
                    Destroy(gameObject); // Despawn when the fish reaches the target position
                }
                break;
        }
    }
}

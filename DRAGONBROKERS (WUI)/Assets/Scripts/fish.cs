using UnityEngine;

public class Fish : MonoBehaviour
{
    public float speed = 2f;
    public float yMin = -4.5f;
    public float yMax = 4.5f;
    public float detectionRange = 1.3f;
    public LayerMask foodLayer;
    // public float minChangeDirectionInterval = 4f; // Commented out
    // public float maxChangeDirectionInterval = 8f; // Commented out
    public float minIdleDuration = 0.25f;
    public float maxIdleDuration = 0.75f;
    public float pitchAmount = 35f; // Angle for pitching up/down
    public float foodChaseSpeedMultiplier = 1.5f;
    public float borderBuffer = 0.25f;

    private Rigidbody2D rb;
    private bool movingRight = true; // Indicates current direction
    private Transform targetFood;
    private float stateTimer;
    // private float changeDirectionInterval; // Commented out
    private float idleDuration;
    private bool hasCollided = false;

    private enum State { Idle, Swimming, ChasingFood }
    private State currentState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //SetRandomIntervals(); // Set intervals for state changes
        TransitionToState(State.Swimming); // Start in the Swimming state
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleState();
                break;
            case State.Swimming:
                SwimmingState();
                break;
            case State.ChasingFood:
                ChasingFoodState();
                break;
        }

        ApplyPitch();
    }

    private void IdleState()
    {
        rb.velocity = Vector2.zero;

        if (Time.time >= stateTimer)
        {
            TransitionToState(State.Swimming);
        }
    }

    private void SwimmingState()
    {
        if (Time.time >= stateTimer)
        {
            // SetRandomDirection(); // Commented out to stop random direction change
            TransitionToState(State.Idle);
            return;
        }

        MoveNaturally();
        CheckBounds();
        DetectFood();
    }

    private void ChasingFoodState()
    {
        if (targetFood == null)
        {
            TransitionToState(State.Swimming);
            return;
        }

        Vector2 direction = (targetFood.position - transform.position).normalized;
        rb.velocity = direction * speed * foodChaseSpeedMultiplier;

        if (Vector2.Distance(transform.position, targetFood.position) < 1.0f)
        {
            EatFood();
        }
    }

    private void TransitionToState(State newState)
    {
        currentState = newState;

        switch (newState)
        {
            case State.Idle:
                stateTimer = Time.time + idleDuration;
                break;
            case State.Swimming:
                // stateTimer = Time.time + changeDirectionInterval; // Commented out
                stateTimer = Time.time + Random.Range(minIdleDuration, maxIdleDuration); // Resetting state timer for swimming state
                break;
            case State.ChasingFood:
                // No timer, will exit based on conditions
                break;
        }
    }

    private void MoveNaturally()
    {
        float verticalMovement = Mathf.Sin(Time.time * speed) * speed * 0.5f;
        rb.velocity = new Vector2((movingRight ? 1 : -1) * speed, verticalMovement);

        // Clamp the fish's position within the tank's vertical limits
        Vector2 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, yMin, yMax);
        transform.position = clampedPosition;
    }

    // private void SetRandomDirection() // Commented out
    // {
    //     movingRight = Random.value > 0.5f; // Commented out
    //     FlipSprite(); // Commented out
    // }

    private void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x = movingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    private void CheckBounds()
    {
        Vector2 position = transform.position;

        if (position.x >= 9f - borderBuffer || position.x <= -9f + borderBuffer)
        {
            if (!hasCollided)
            {
                movingRight = !movingRight; // Direction will now toggle only on collision with the tank edges
                FlipSprite();
                hasCollided = true;
                stateTimer = Time.time + 0.1f;
            }
        }
        else
        {
            hasCollided = false;
        }

        if (position.y >= yMax || position.y <= yMin)
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }
    }

    private void DetectFood()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, foodLayer);

        if (hit.collider != null)
        {
            targetFood = hit.transform;
            TransitionToState(State.ChasingFood);
        }
    }

    private void EatFood()
    {
        if (targetFood != null)
        {
            Debug.Log("Eating food: " + targetFood.name);
            Destroy(targetFood.gameObject);
            targetFood = null;
            TransitionToState(State.Idle);
        }
    }

    private void ApplyPitch()
    {
        Vector2 velocity = rb.velocity;

        // Determine pitch angle based on vertical velocity
        float pitchAngle = Mathf.Clamp((velocity.y / speed) * pitchAmount, -pitchAmount, pitchAmount);

        // Apply pitch to rotation
        if (Mathf.Abs(velocity.x) > 0.1f || Mathf.Abs(velocity.y) > 0.1f)
        {
            // Correctly rotate the fish around its Z-axis
            transform.rotation = Quaternion.Euler(0, 0, pitchAngle);
        }
        else
        {
            transform.rotation = Quaternion.identity; // Reset rotation if swimming straight
        }
    }

    // private void SetRandomIntervals() // Commented out
    // {
    //     changeDirectionInterval = Random.Range(minChangeDirectionInterval, maxChangeDirectionInterval); // Commented out
    //     idleDuration = Random.Range(minIdleDuration, maxIdleDuration); // Commented out
    // }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        float gizmoLength = detectionRange;
        Gizmos.DrawRay(transform.position, direction * gizmoLength);
    }
}

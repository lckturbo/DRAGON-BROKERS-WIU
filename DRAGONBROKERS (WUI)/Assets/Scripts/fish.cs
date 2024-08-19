using UnityEngine;

public class fish : MonoBehaviour
{
    public float speed = 2f;
    public float yMin = -4.5f;
    public float yMax = 4.5f;
    public float detectionRange = 1.3f;
    public LayerMask foodLayer;
    public float minIdleDuration = 0.25f;
    public float maxIdleDuration = 0.75f;
    public float pitchAmount = 35f; // Angle for pitching up/down
    public float foodChaseSpeedMultiplier = 1.5f;
    public float borderBuffer = 0.25f;
    public float sizeIncreaseAmount = 0.05f; // Amount to increase size each time the fish eats
    public float breedingCooldown = 15f; // Time before fish can eat or breed again
    public GameObject fishPrefab; // Prefab for the new fish

    private Rigidbody2D rb;
    private bool movingRight = true; // Indicates current direction
    private Transform targetFood;
    private float stateTimer;
    private float idleDuration;
    private bool hasCollided = false;
    private bool canBreed = true;
    private bool hasEaten = false;
    private float breedingTimer;

    private enum State { Idle, Swimming, ChasingFood }
    private State currentState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TransitionToState(State.Swimming); // Start in the Swimming state 
    }

    private void Update()
    {
        if (!canBreed && Time.time >= breedingTimer)
        {
            canBreed = true;
        }

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
        FlipSprite();
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
                stateTimer = Time.time + Random.Range(minIdleDuration, maxIdleDuration);
                break;
            case State.ChasingFood:
                break;
        }
    }

    private void MoveNaturally()
    {
        float verticalMovement = Mathf.Sin(Time.time * speed) * speed * 0.5f;
        rb.velocity = new Vector2((movingRight ? 1 : -1) * speed, verticalMovement);

        Vector2 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, yMin, yMax);
        transform.position = clampedPosition;
    }

    private void FlipSprite()
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();

        if (renderer == null) return;

        if (movingRight)
            renderer.flipX = false;
        else
            renderer.flipX = true;

        //Vector3 scale = transform.localScale;
        //scale.x = movingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        //transform.localScale = scale;
    }

    private void CheckBounds()
    {
        Vector2 position = transform.position;

        if (position.x >= 9f - borderBuffer || position.x <= -9f + borderBuffer)
        {
            if (!hasCollided)
            {
                movingRight = !movingRight;
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
            transform.localScale += new Vector3(sizeIncreaseAmount, sizeIncreaseAmount, 0);
            hasEaten = true;
            TransitionToState(State.Idle);
        }
    }

    private void ApplyPitch()
    {
        Vector2 velocity = rb.velocity;

        float pitchAngle = Mathf.Clamp((velocity.y / speed) * pitchAmount, -pitchAmount, pitchAmount);

        if (Mathf.Abs(velocity.x) > 0.1f || Mathf.Abs(velocity.y) > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, pitchAngle);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("fish"))
        {
            fish otherFish = other.GetComponent<fish>();
            if (otherFish != null && hasEaten && otherFish.hasEaten && canBreed && otherFish.canBreed)
            {
                Debug.Log("Breeding fish");
                Breed(otherFish);
            }
        }
    }

    private void Breed(fish otherFish)
    {
        // Reduce size of the fish that are breeding
        transform.localScale -= new Vector3(sizeIncreaseAmount, sizeIncreaseAmount, 0);
        otherFish.transform.localScale -= new Vector3(sizeIncreaseAmount, sizeIncreaseAmount, 0);

        // Ensure the fish size does not go below 1x1x1
        transform.localScale = Vector3.Max(transform.localScale, new Vector3(1, 1, 1));
        otherFish.transform.localScale = Vector3.Max(otherFish.transform.localScale, new Vector3(1, 1, 1));

        // Disable breeding and eating temporarily
        canBreed = false;
        otherFish.canBreed = false;
        breedingTimer = Time.time + breedingCooldown;
        otherFish.breedingTimer = Time.time + breedingCooldown;

        // Spawn a new fish
        if (fishPrefab != null)
        {
            Instantiate(fishPrefab, transform.position, Quaternion.identity);
            Debug.Log("Spawned new fish");
        }

        // Reset the eaten state
        hasEaten = false;
        otherFish.hasEaten = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        float gizmoLength = detectionRange;
        Gizmos.DrawRay(transform.position, direction * gizmoLength);
    }
}

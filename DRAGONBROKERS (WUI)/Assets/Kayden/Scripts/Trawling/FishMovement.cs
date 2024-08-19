using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float fleeDistance = 5f; // Distance to flee from TrawlerArm

    public GameObject loseEffect;
    private ParticleSystem loseParticleSystem;
    public Vector2 loseEffectOffset = new Vector2(1, 1);  // Offset distance for the lose effect

    public enum FishState
    {
        Swimming,
        Caught
    }

    private FishState currentState;
    private Transform currentTarget;
    private SpriteRenderer spriteRenderer;
    private Transform trawlerArm; // Reference to the TrawlerArm
    private float caughtTime; // Time when the fish was caught
    private bool isFleeing; // Flag to indicate if the fish is currently fleeing

    private void Start()
    {
        // Initialize the state and target
        currentState = FishState.Swimming;
        currentTarget = leftEdge; // Start by targeting the left edge
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component

        // Find the TrawlerArm in the scene
        trawlerArm = GameObject.FindGameObjectWithTag("TrawlerArm")?.transform;
    }

    private void Update()
    {
        switch (currentState)
        {
            case FishState.Swimming:
                HandleSwimming();
                break;
            case FishState.Caught:
                if (isFleeing)
                {
                    HandleFleeing();
                }
                else
                {
                    // Start fleeing when the state is set to Caught
                    isFleeing = true;
                    caughtTime = Time.time;
                }
                break;
        }
    }

    private void HandleSwimming()
    {
        // Move towards the current target
        MoveTowardsTarget();

        // Check if the fish has reached the target
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            // Switch targets
            if (currentTarget == leftEdge)
            {
                currentTarget = rightEdge;
            }
            else
            {
                currentTarget = leftEdge;
            }
        }

        // Flip the sprite based on movement direction
        FlipSprite();
    }

    private void HandleFleeing()
    {
        // Move away from the TrawlerArm
        if (trawlerArm != null)
        {
            Vector2 directionAwayFromTrawler = (transform.position - trawlerArm.position).normalized;
            transform.position += (Vector3)directionAwayFromTrawler * speed * Time.deltaTime;

            // Check if 5 seconds have passed since the fish was caught
            if (Time.time - caughtTime >= 5f)
            {
                // Play the lose effect at a position relative to the fish
                Vector3 loseEffectPosition = transform.position + (Vector3)loseEffectOffset;
                GameObject effectInstance = Instantiate(loseEffect, loseEffectPosition, Quaternion.identity);

                // Destroy the lose effect after 3 seconds
                Destroy(effectInstance, 3f);

                Destroy(gameObject);
            }
        }
    }

    private void MoveTowardsTarget()
    {
        // Calculate the direction to the target
        Vector2 direction = (currentTarget.position - transform.position).normalized;
        // Move the fish
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void FlipSprite()
    {
        // Determine if the fish is moving left or right
        if (currentTarget.position.x < transform.position.x)
        {
            // Flip the sprite to face left
            spriteRenderer.flipX = true;
        }
        else
        {
            // Flip the sprite to face right
            spriteRenderer.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged "TrawlerArm"
        if (collision.gameObject.CompareTag("TrawlerArm"))
        {
            // Switch to Caught state
            SetState(FishState.Caught);
        }
    }

    public void SetState(FishState newState)
    {
        if (currentState != newState)
        {
            // Handle exit logic for the current state
            if (currentState == FishState.Swimming)
            {
                // Logic to handle exiting the Swimming state (if any)
            }

            // Update to the new state
            currentState = newState;

            // Handle entry logic for the new state
            if (currentState == FishState.Caught)
            {
                // Initialize fleeing state
                isFleeing = true;
                caughtTime = Time.time;
                Debug.Log("caught");
            }
        }
    }
}

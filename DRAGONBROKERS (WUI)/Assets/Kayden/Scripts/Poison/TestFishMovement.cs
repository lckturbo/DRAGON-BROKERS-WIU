using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFishMovement : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float moveUpSpeed = 1f; // Speed at which the fish moves up in the Dead state
    private float targetYPosition;

    public enum FishState
    {
        Swimming,
        Dead
    }

    private FishState currentState;
    private Transform currentTarget;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Initialize the state and target
        currentState = FishState.Swimming;
        currentTarget = leftEdge; // Start by targeting the left edge
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    private void Update()
    {
        switch (currentState)
        {
            case FishState.Swimming:
                HandleSwimming();
                break;
            case FishState.Dead:
                HandleDead();
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

    private void HandleDead()
    {
        // Slowly move the fish up until it reaches the target Y position
        if (transform.position.y < targetYPosition)
        {
            transform.position += new Vector3(0, moveUpSpeed * Time.deltaTime, 0);
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

    public void SetState(FishState newState)
    {
        if (currentState != newState)
        {
            Debug.Log($"Changing state from {currentState} to {newState}.");

            // Update to the new state
            currentState = newState;

            // Handle entering logic for the new state
            if (newState == FishState.Dead)
            {
                targetYPosition = transform.position.y + 10f;
                Debug.Log($"Dead state entered. Target Y position set to {targetYPosition}.");
            }
        }
    }

    public void StopMovement()
    {
        // Stop the fish's movement by setting the state to Dead
        SetState(FishState.Dead);
    }

    public void ChangeColor(Color color)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with: " + collision.collider.name + " with tag: " + collision.collider.tag);
    }
}

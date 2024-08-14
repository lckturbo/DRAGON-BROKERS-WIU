using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    public VectorValue startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;

        // Check for horizontal input first
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            change.x = Input.GetAxisRaw("Horizontal");
        }
        // If no horizontal input, then check for vertical input
        else if (Input.GetAxisRaw("Vertical") != 0)
        {
            change.y = Input.GetAxisRaw("Vertical");
        }

        MoveCharacter();
    }

    void MoveCharacter()
    {
        if (change != Vector3.zero)
        {
            myRigidbody.MovePosition(
                transform.position + change * speed * Time.fixedDeltaTime
            );
        }
    }
}

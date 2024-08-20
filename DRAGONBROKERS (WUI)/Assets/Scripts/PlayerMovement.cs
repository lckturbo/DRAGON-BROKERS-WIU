using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    public VectorValue startingPosition;
    private AudioSource sfxAudioSrc;
    private bool walk = false;

    private Animator animator;

    [SerializeField] private AudioClip walkAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.position = startingPosition.initialValue;
        sfxAudioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;

        // Check for horizontal input first
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            change.x = Input.GetAxisRaw("Horizontal");
            walk = true;
        }
        // If no horizontal input, then check for vertical input
        else if (Input.GetAxisRaw("Vertical") != 0)
        {
            change.y = Input.GetAxisRaw("Vertical");
            walk = true;
        }
        else
        {
            walk = false;
        }

        if (walk == true)
        {
            sfxAudioSrc.clip = walkAudioClip;
            if (!sfxAudioSrc.isPlaying)
            {
                sfxAudioSrc.clip = walkAudioClip;
                sfxAudioSrc.Play();
            }
        }

        UpdateAnimationAndMove();
    }


    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishingBarScript : MonoBehaviour
{
    public Rigidbody rb;
    public bool atTop;
    public float targetTime = 4.0f;
    public float savedTargetTime;

    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public GameObject p5;
    public GameObject p6;
    public GameObject p7;
    public GameObject p8;

    public bool onFish;
    public FishingScript playerS;
    public GameObject bobber;

    public float jumpForce = 0.03f; // Adjust this value as needed

    void Start()
    {
        rb.useGravity = true; // Ensure gravity is enabled
    }

    void Update()
    {
        if (onFish)
        {
            targetTime += Time.deltaTime;
        }
        else
        {
            targetTime -= Time.deltaTime;
        }

        if (targetTime <= 0.0f)
        {
            ResetBar();
            playerS.fishGameLost();
        }

        if (targetTime >= 8.0f)
        {
            ResetBar();
            playerS.fishGameWon();
        }

        UpdateBar();

        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector3(0, jumpForce, 0); // Apply velocity instead of AddForce
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("fish"))
        {
            onFish = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("fish"))
        {
            onFish = false;
        }
    }

    private void ResetBar()
    {
        transform.localPosition = new Vector3(-0.07583f, -0.2f, 0);
        onFish = false;
        Destroy(GameObject.Find("blobber(Clone)"));
        targetTime = 4.0f;
    }

    private void UpdateBar()
    {
        p1.SetActive(targetTime >= 1.0f);
        p2.SetActive(targetTime >= 2.0f);
        p3.SetActive(targetTime >= 3.0f);
        p4.SetActive(targetTime >= 4.0f);
        p5.SetActive(targetTime >= 5.0f);
        p6.SetActive(targetTime >= 6.0f);
        p7.SetActive(targetTime >= 7.0f);
        p8.SetActive(targetTime >= 8.0f);
    }
}
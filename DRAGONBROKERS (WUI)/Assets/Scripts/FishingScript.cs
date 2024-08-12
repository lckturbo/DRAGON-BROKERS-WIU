using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator playerAnim;
    public bool isFishing;
    public bool poleBack;
    public bool throwBobber;
    public Transform fishingPoint;
    public GameObject bobber;

    public float targetTime = 0.0f;
    public float savedTargetTime;
    public float extraBobberDistance;

    public GameObject fishGame;

    public float timeTillCatch = 0.0f;
    public bool winnerAnim;

    void Start()
    {
        isFishing = false;
        fishGame.SetActive(false);
        throwBobber = false;
        targetTime = 0.0f;
        savedTargetTime = 0.0f;
        extraBobberDistance = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isFishing == false && winnerAnim == false)
        {
            poleBack = true;
        }
        if (isFishing == true)
        {
            timeTillCatch += Time.deltaTime;
            if (timeTillCatch >= 3)
            {
                fishGame.SetActive(true);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && isFishing == false && winnerAnim == false)
        {
            poleBack = false;
            isFishing = true;
            throwBobber = true;
            if (targetTime >= 3)
            {
                extraBobberDistance += 3;
            }
            else
            {
                extraBobberDistance += targetTime;
            }
        }

        Vector3 temp = new Vector3(extraBobberDistance, 0, 0);
        fishingPoint.transform.position += temp;

        if (poleBack == true)
        {
            playerAnim.Play("playerSwingBack");
            savedTargetTime = targetTime;
            targetTime += Time.deltaTime;
        }

        if (isFishing == true)
        {
            if (throwBobber == true)
            {
                Instantiate(bobber, fishingPoint.position, fishingPoint.rotation, transform);
                fishingPoint.transform.position -= temp;

                throwBobber = false;
                targetTime = 0.0f;
                savedTargetTime = 0.0f;
                extraBobberDistance = 0.0f;
            }
            playerAnim.Play("playerFishing");
        }

        if (Input.GetKeyDown(KeyCode.P) && timeTillCatch <= 3)
        {
            playerAnim.Play("playerStill");
            poleBack = false;
            throwBobber = false;
            isFishing = false;
            timeTillCatch = 0;
        }
    }

    public void fishGameWon()
    {
        // Generate a random float between 0.0 and 1.0
        float chance = Random.value;

        // If chance is less than 0.5, play "playerWonFish"; otherwise, play "playerWonFish2"
        // Change the percentage here
        if (chance < 0.5f)
        {
            playerAnim.Play("playerWonFish");
            Debug.Log("Played animation: 1");
        }
        else
        {
            playerAnim.Play("playerWonFish2");
            Debug.Log("Played animation: 2");
        }

        // Reset the game state
        fishGame.SetActive(false);
        poleBack = false;
        throwBobber = false;
        isFishing = false;
        timeTillCatch = 0;
    }

    /*
     public void fishGameWon()
{
    // Generate a random float between 0.0 and 1.0
    float chance = Random.value;

    // Determine which animation to play based on the weighted probabilities
    if (chance < 0.2f)
    {
        playerAnim.Play("playerWonFish");
    }
    else if (chance < 0.5f)
    {
        playerAnim.Play("playerWonFish2");
    }
    else
    {
        playerAnim.Play("playerWonFish3");
    }

    // Reset the game state
    fishGame.SetActive(false);
    poleBack = false;
    throwBobber = false;
    isFishing = false;
    timeTillCatch = 0;
}

     */


    public void fishGameLost()
    {
        playerAnim.Play("playerStill");
        fishGame.SetActive(false);
        poleBack = false;
        throwBobber = false;
        isFishing = false;
        timeTillCatch = 0;
    }
}
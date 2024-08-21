using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingScript : MonoBehaviour
{
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

    public FishingProbability fishingProbability;

    // Reference to the CameraShake script
    public CameraShake cameraShake;

    // Flag to track if camera shake has occurred
    private bool hasShakenCamera = false;

    // References to the win/lose effects GameObjects and Particle Systems
    public GameObject winEffect;
    public GameObject loseEffect;
    private ParticleSystem winParticleSystem;
    private ParticleSystem loseParticleSystem;

    private AudioSource sfxAudioSrc;
    [SerializeField] private AudioClip splashAudioClip;
    [SerializeField] private AudioClip fishAudioClip;

    public static int rodFishCaught = 0;

    void Start()
    {
        isFishing = false;
        fishGame.SetActive(false);
        throwBobber = false;
        targetTime = 0.0f;
        savedTargetTime = 0.0f;
        extraBobberDistance = 0.0f;

        // Initialize the Particle Systems
        winParticleSystem = winEffect.GetComponentInChildren<ParticleSystem>();
        loseParticleSystem = loseEffect.GetComponentInChildren<ParticleSystem>();

        // Make sure the effects are not visible at the start
        winEffect.SetActive(false);
        loseEffect.SetActive(false);

        sfxAudioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isFishing == false && winnerAnim == false)
        {
            poleBack = true;
            if (!sfxAudioSrc.isPlaying)
            {
                sfxAudioSrc.clip = splashAudioClip;
                sfxAudioSrc.Play();
            }
        }
        if (isFishing == true)
        {
            timeTillCatch += Time.deltaTime;
            if (timeTillCatch >= 3)
            {
                fishGame.SetActive(true);

                // Trigger the camera shake only once
                if (!hasShakenCamera)
                {
                    if (!sfxAudioSrc.isPlaying)
                    {
                        sfxAudioSrc.clip = fishAudioClip;
                        sfxAudioSrc.Play();
                    }
                    cameraShake.ShakeCamera();
                    hasShakenCamera = true;
                }
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

        if (isFishing == true && !IsAnimationPlaying("playerSwingBack"))
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

    private bool IsAnimationPlaying(string animationName)
    {
        return playerAnim.GetCurrentAnimatorStateInfo(0).IsName(animationName) && playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;
    }

    public void fishGameWon()
    {
        if (!sfxAudioSrc.isPlaying)
        {
            sfxAudioSrc.clip = splashAudioClip;
            sfxAudioSrc.Play();
        }

        rodFishCaught++;
        Debug.Log("YOU ARE TOUCHING ME: " +  rodFishCaught);

        fishingProbability.FishingRodChance(playerAnim);

        fishGame.SetActive(false);
        poleBack = false;
        throwBobber = false;
        isFishing = false;
        timeTillCatch = 0;

        // Trigger the win effect
        PlayEffect(winEffect, winParticleSystem);

        // Reset the shake flag
        hasShakenCamera = false;
    }

    public void fishGameLost()
    {
        if (!sfxAudioSrc.isPlaying)
        {
            sfxAudioSrc.clip = splashAudioClip;
            sfxAudioSrc.Play();
        }
        playerAnim.Play("playerStill");
        fishGame.SetActive(false);
        poleBack = false;
        throwBobber = false;
        isFishing = false;
        timeTillCatch = 0;

        // Trigger the lose effect
        PlayEffect(loseEffect, loseParticleSystem);

        // Reset the shake flag
        hasShakenCamera = false;
    }

    private void PlayEffect(GameObject effect, ParticleSystem particleSystem)
    {
        effect.SetActive(true);
        particleSystem.Play();

        // Optionally, you can set a timer to disable the effect after a certain duration
        StartCoroutine(DisableEffectAfterTime(effect, particleSystem.main.duration));
    }

    private IEnumerator DisableEffectAfterTime(GameObject effect, float duration)
    {
        yield return new WaitForSeconds(duration);
        effect.SetActive(false);
    }
}

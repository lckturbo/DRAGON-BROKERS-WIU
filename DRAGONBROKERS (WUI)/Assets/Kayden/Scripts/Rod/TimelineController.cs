using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector timeline;
    public FishingScript fishingScript;

    void Start()
    {
        if (timeline != null)
        {
            timeline.stopped += OnTimelineStopped;
        }
    }

    void Update()
    {
        if (timeline != null && timeline.state == PlayState.Playing)
        {
            DisablePlayerControls();
        }
    }

    void DisablePlayerControls()
    {
        if (fishingScript != null)
        {
            fishingScript.enabled = false;
        }
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        EnablePlayerControls();
    }

    void EnablePlayerControls()
    {
        if (fishingScript != null)
        {
            fishingScript.enabled = true;
        }
    }

    void OnDestroy()
    {
        if (timeline != null)
        {
            timeline.stopped -= OnTimelineStopped;
        }
    }
}

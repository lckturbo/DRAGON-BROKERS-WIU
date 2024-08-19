using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PoisonTimeline : MonoBehaviour
{
    public PlayableDirector timeline;
    public Slingshot slingshot;

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
        if (slingshot != null)
        {
            slingshot.enabled = false;
        }
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        EnablePlayerControls();
    }

    void EnablePlayerControls()
    {
        if (slingshot != null)
        {
            slingshot.enabled = true;
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

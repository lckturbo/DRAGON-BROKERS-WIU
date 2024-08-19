using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TrawlTimeline : MonoBehaviour
{
    public PlayableDirector timeline;
    public IKManager ikManager;

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
        if (ikManager != null)
        {
            ikManager.enabled = false;
        }
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        EnablePlayerControls();
    }

    void EnablePlayerControls()
    {
        if (ikManager != null)
        {
            ikManager.enabled = true;
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

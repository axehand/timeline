using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class ObjectSpecificBehaviour : PlayableBehaviour
{
    public string objectName;
    public bool hasToPause = false;

    private GameObject gameObject;
    private ParticleSystem particleSystem;
    private PlayableDirector director;

    public enum ObjectType
    {
        Particle,
        Camera
    }

    public ObjectType objType = ObjectType.Particle;

    public override void OnPlayableCreate(Playable playable)
    {
        director = playable.GetGraph().GetResolver() as PlayableDirector;

        if (!string.IsNullOrEmpty(objectName))
        {
            gameObject = GameObject.Find(objectName);
            if (gameObject != null)
            {
                switch (objType)
                {
                    case ObjectType.Particle:
                        particleSystem = gameObject.GetComponent<ParticleSystem>();
                        break;
                    case ObjectType.Camera:
                        break;
                }
            }
        }
    }
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (info.weight > 0f)
        {
            PlayObject();
        }
    }

    private void PlayObject()
    {
        switch (objType)
        {
            case ObjectType.Particle:
                if (particleSystem != null && !particleSystem.isPlaying)
                {
                    particleSystem.Play();
                }
                break;
            case ObjectType.Camera:
                break;
        }
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (info.effectivePlayState == PlayState.Paused)
        {
            StopObject();
            if (hasToPause)
            {
                GameManager.Instance.PauseTimeline(director);
            }
        }
    }

    private void StopObject()
    {
        switch (objType)
        {
            case ObjectType.Particle:
                if (particleSystem != null)
                {
                    particleSystem.Stop();
                }
                break;
            case ObjectType.Camera:
                break;
        }
    }
}

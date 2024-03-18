using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class ObjectSpecificClip : PlayableAsset, ITimelineClipAsset
{
    public ObjectSpecificBehaviour template = new ObjectSpecificBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.All; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<ObjectSpecificBehaviour>.Create (graph, template);
        ObjectSpecificBehaviour clone = playable.GetBehaviour ();
        return playable;
    }
}

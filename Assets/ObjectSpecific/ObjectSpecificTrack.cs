using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.1341669f, 0.1378382f, 0.1415094f)]
[TrackClipType(typeof(ObjectSpecificClip))]
[TrackBindingType(typeof(GameObject))]
public class ObjectSpecificTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<ObjectSpecificMixerBehaviour>.Create (graph, inputCount);
    }
}

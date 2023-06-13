using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class FloatParameter : PlayableBehaviour
{
    public string parameterName;
    public float parameterValue;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (playerData is Animator animator)
        {
            animator.SetFloat(parameterName, parameterValue);
        }
    }
}

[System.Serializable]
public class FloatParameterAsset : PlayableAsset, ITimelineClipAsset
{
    public ExposedReference<Animator> targetAnimator;
    public string parameterName;
    public float parameterValue;

    public ClipCaps clipCaps => ClipCaps.None;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<FloatParameter>.Create(graph);
        var floatParameter = playable.GetBehaviour();
        floatParameter.parameterName = parameterName;
        floatParameter.parameterValue = parameterValue;

        return playable;
    }
}

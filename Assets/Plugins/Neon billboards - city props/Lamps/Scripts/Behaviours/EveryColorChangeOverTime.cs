using Mixaill3d.Lamps.Scripts.Core;
using UnityEngine;

namespace Mixaill3d.Lamps.Scripts.Behaviours
{
    [CreateAssetMenu(fileName = "EveryColorChangeOverTime", menuName = "Mixaill3d/LampsBehaviour/EveryColorChangeOverTime")]
    public class EveryColorChangeOverTime : LampBasicBehaviour
    {
        [SerializeField] protected Gradient[] _gradients;

        protected override void ProcessSingleLampBehaviour(Renderer renderer, float timeOffset, float speed)
        {
            var currentTime = GetCurrentTime(timeOffset, speed);
            var siblingIndex = renderer.transform.GetSiblingIndex();
            var color = _gradients[siblingIndex].Evaluate(currentTime);
            SetColor(renderer, color);
        }
        
    }
}
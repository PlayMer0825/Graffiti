using System;
using UnityEngine;

namespace Mixaill3d.Lamps.Scripts.Core
{
    public class LampBasicBehaviour : ScriptableObject
    {
        public virtual void ProcessBehaviour(GameObject[] objects, float timeOffset, float speed)
        {
            foreach (var lamp in objects)
            {
                ProcessSingleLampBehaviour(lamp.GetComponent<Renderer>(), timeOffset, speed);
            }
        }

        protected void SetColor(Renderer renderer, Color color)
        {
            var material = renderer.material;
            material.SetColor("_EmissionColor", color);
        }

        protected virtual void ProcessSingleLampBehaviour(Renderer renderer, float timeOffset, float speed)
        {
            throw new Exception("It is basic behaviour. There is no realization.");
        }
        
        protected float GetCurrentTime(float timeOffset, float speed)
        {
            return Mathf.Repeat(Time.time * speed + timeOffset, 1f);
        }
    }
}

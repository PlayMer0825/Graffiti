using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OperaHouse {
    public class PercentageUI : MonoBehaviour {
        private P3dChangeCounterFill percentage_Circle = null;
        private P3dChangeCounterText percentage_Count = null;
        private P3dChangeCounter curCounter = null;

        private void Awake() {
            percentage_Circle = GetComponentInChildren<P3dChangeCounterFill>();
            percentage_Count = GetComponentInChildren<P3dChangeCounterText>();
        }

        public void RegisterForChangeCounter(P3dChangeCounter counter) {
            if(counter == null)
                return;

            curCounter = counter;
            percentage_Circle.Counters.Add(curCounter);
            percentage_Count.Counters.Add(curCounter);
        }

        private void OnEnable() {
            RegisterForChangeCounter(null);
        }

        private void OnDisable() {
            if(percentage_Circle.Counters.Contains(curCounter))
                percentage_Circle.Counters.Remove(curCounter);
            if(percentage_Count.Counters.Contains(curCounter))
                percentage_Count.Counters.Remove(curCounter);

            curCounter = null;
        }
    }

}
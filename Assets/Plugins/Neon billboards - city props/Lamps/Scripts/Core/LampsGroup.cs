using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mixaill3d.Lamps.Scripts.Core
{
    public class LampsGroup : MonoBehaviour
    {
        [SerializeField] private LampBasicBehaviour _behaviour;
        [SerializeField] private float _timeOffset;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private GameObject[] _lamps;

        public List<GameObject> FindLamps(Transform t)
        {
            List<GameObject> result = new List<GameObject>();
            FindLamps(t, result);
            return result;
        }

        private void FindLamps(Transform t, List<GameObject> result)
        {
            foreach (Transform child in t)
            {
                if (child.name.Contains("_Lamp_"))
                    result.Add(child.gameObject);
                FindLamps(child, result);
            }
        }

        public void Awake()
        {
            InitializeLamps();
            LampsController.Instance.RegisterGroup(this);
        }

        public void OnDestroy()
        {
            LampsController.Instance.RegisterGroup(this);
        }

        private void InitializeLamps()
        {
            if (_lamps == null || _lamps.Length == 0)
            {
                _lamps = FindLamps(transform).ToArray();
            }
        }

        public void UpdateLightning()
        {
            _behaviour.ProcessBehaviour(_lamps, _timeOffset, _speed);
        }
    }
}

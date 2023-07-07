using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public class Singleton<T> : MonoBehaviour where T : Component {
        private static T _instance = null;
        public static T Instance {
            get {
                if(_instance == null) {
                    _instance = FindObjectOfType<T>();

                    if(_instance == null) {
                        Debug.LogWarning($"{typeof(T)} not Exist");
                        GameObject go = new GameObject(typeof(T).Name);
                        _instance = go.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake() {
            if(_instance != null)
                Destroy(_instance.gameObject);

            _instance = this as T;
        }
    }
}
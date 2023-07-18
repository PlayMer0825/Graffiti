using UnityEngine;

namespace Insomnia {
    public class Singleton<T> : MonoBehaviour where T : Component {
        protected static T _instance = null;
        public static T Instance {
            get {
                if(_instance == null) {
                    _instance = FindObjectOfType<T>();

                    if(_instance == null) {
                        Debug.LogWarning($"{typeof(T)} not Exist");
                        return null;
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake() {
            if(_instance != null)
                Destroy(_instance.gameObject);

            _instance = gameObject.GetComponent<T>();
        }
    }
}
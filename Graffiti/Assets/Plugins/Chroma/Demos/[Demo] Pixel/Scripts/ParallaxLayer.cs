using UnityEngine;

namespace Dustyroom {
public class ParallaxLayer : MonoBehaviour {
    public float amount = 1;

    private Camera _camera;
    private float _initialCameraPosition;
    private Vector3 _initialPosition;

    private void Start() {
        _initialPosition = transform.position;
        _camera = Camera.main;
        Debug.Assert(_camera != null, nameof(_camera) + " != null");
        _initialCameraPosition = _camera.transform.position.x;
    }

    private void FixedUpdate() {
        var offset = _initialCameraPosition - _camera.transform.position.x;
        transform.position = _initialPosition - Vector3.right * (offset * amount * 0.1f);
    }
}
}
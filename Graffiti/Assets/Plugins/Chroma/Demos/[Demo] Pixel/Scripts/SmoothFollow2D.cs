using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SmoothFollow2D : MonoBehaviour {
    public Transform target;
    public Vector2 staticWindow = new Vector2(2f, 1);
    public Rect boundsWorldSpace = Rect.MinMaxRect(-100, -100, 100, 100);

    private Camera _camera;

    private void Start() {
        _camera = GetComponent<Camera>();
    }

    private void Update() {
        if (target == null) {
            return;
        }

        var goal = transform.position;

        if (Mathf.Abs(goal.x - target.position.x) > staticWindow.x) {
            goal.x = Mathf.Clamp(transform.position.x, target.position.x - staticWindow.x,
                                 target.position.x + staticWindow.x);
        }

        if (Mathf.Abs(goal.y - target.position.y) > staticWindow.y) {
            goal.y = Mathf.Clamp(transform.position.y, target.position.y - staticWindow.y,
                                 target.position.y + staticWindow.y);
        }

        goal.x = Mathf.Clamp(goal.x, boundsWorldSpace.xMin, boundsWorldSpace.xMax);
        goal.y = Mathf.Clamp(goal.y, boundsWorldSpace.yMin, boundsWorldSpace.yMax);

        transform.position = goal;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boundsWorldSpace.center, boundsWorldSpace.size);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, staticWindow * 2);
    }
}
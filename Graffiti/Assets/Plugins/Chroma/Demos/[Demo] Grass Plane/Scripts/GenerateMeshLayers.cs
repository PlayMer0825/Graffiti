using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(GenerateMeshLayers))]
class DecalMeshHelperEditor : Editor {
    public override void OnInspectorGUI() {
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();
        var script = (GenerateMeshLayers)target;
        EditorGUILayout.Space();
        if (EditorGUI.EndChangeCheck()) {
            script.Generate();
        }
    }
}
#endif

public class GenerateMeshLayers : MonoBehaviour {
    public Mesh source;
    [Range(1, 50)] public int layerCount = 16;
    public float layerHeight = 0.5f;

    public void Generate() {
        if (source == null) return;

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null) {
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }

        Mesh mesh = new Mesh();
        meshFilter.sharedMesh = mesh;

        int vertexCount = source.vertexCount * layerCount;
        int triangleCount = source.triangles.Length * layerCount;

        Vector3[] vertices = new Vector3[vertexCount];
        Vector3[] normals = new Vector3[vertexCount];
        Vector2[] uv = new Vector2[vertexCount];
        var colors = new Color[vertexCount];
        int[] triangles = new int[triangleCount];

        var sourceVertices = source.vertices;
        var sourceNormals = source.normals;
        var sourceUV = source.uv;
        var sourceTriangles = source.triangles;

        for (int layer = 0; layer < layerCount; layer++) {
            int lv = layer * sourceVertices.Length;
            for (int i = 0; i < sourceVertices.Length; i++) {
                vertices[lv + i] = sourceVertices[i] + sourceNormals[i] * (layer * layerHeight);
                normals[lv + i] = sourceNormals[i];
                uv[lv + i] = sourceUV[i];
                var redChannel = layer / (layerCount - 1f);
                colors[lv + i] = new Color(redChannel, 0, 0, 1);
            }

            int lt = layer * sourceTriangles.Length;
            for (int i = 0; i < sourceTriangles.Length; i++) {
                triangles[lt + i] = sourceTriangles[i] + lv;
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}
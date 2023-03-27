using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerTest : MonoBehaviour
{
    [SerializeField] private P3dMaterialCloner _p3dClone = null;
    [SerializeField] private MeshRenderer _mesh = null;
    [SerializeField] private MeshCollider _col = null;
    [SerializeField] private P3dPaintDecal _decal = null;
    //[SerializeField] private GameObject _razor = null;
    [SerializeField] private GameObject _decalObject = null;
    

    private void Awake() {
        _decal = GetComponent<P3dPaintDecal>();
    }

    private void OnEnable() {
        //Texture tex = _p3dClone.Current.mainTexture;
        Texture tex = _mesh.material.mainTexture;
        Vector3 size = _mesh.bounds.size;
        _decal.Texture = tex;
        //tex.
        Debug.Log($"Size: {_col.sharedMesh.bounds.size}");
        //_decal. = _col.sharedMesh.bounds.size;
    }

    private void OnDisable() {
        //Vector3 dir = _decalObject.transform.position - Camera.main.transform.position;
        //Ray ray = new Ray(Camera.main.transform.position, dir);

        //if(Physics.Raycast(ray, 100, LayerMask.NameToLayer("Paintable"))) {
        //    //TODO: 여기서 P3D Hit Screen같은걸로 그림 그려주기
        //}
        
    }
}

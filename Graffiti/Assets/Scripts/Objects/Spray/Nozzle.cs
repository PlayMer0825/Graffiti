using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nozzle : MonoBehaviour {
    [SerializeField] private ParticleSystem e_particle = null;
    [SerializeField] private P3dPaintSphere e_sprayDrawer = null;

    public void LookAt(Vector3 pos) {
        e_particle.transform.LookAt(pos);
    }

    public void Play() { e_particle.Play(); }
    public void Stop() { e_particle.Stop(); }

    public void ChangeColor(Color color) {
        e_particle.startColor = color;
        e_sprayDrawer.Color = color;
    }
}

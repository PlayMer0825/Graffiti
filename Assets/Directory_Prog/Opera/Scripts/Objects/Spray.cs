using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public class Spray : MonoBehaviour {
        [SerializeField] ParticleSystem _particle = null;
        [SerializeField] P3dPaintSphere _p3dPaint = null;

        private void Awake() {
            _particle = GetComponent<ParticleSystem>();
            _p3dPaint = GetComponent<P3dPaintSphere>();
        }

        public Color Color {
            get => _p3dPaint.Color;
            set {
                _p3dPaint.Color = value;
                _particle.startColor = value;
            }
        }

        public float Opacity {
            get => _p3dPaint.Opacity;
            set {
                _p3dPaint.Opacity = value;
            }
        }

        public float Radius {
            get => _p3dPaint.Radius;
            set {
                _p3dPaint.Radius = value;
            }
        }
    }
}
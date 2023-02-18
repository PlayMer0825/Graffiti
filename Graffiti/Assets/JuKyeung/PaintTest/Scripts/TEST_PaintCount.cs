using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaintIn3D
{
    public class TEST_PaintCount : MonoBehaviour
    {
        P3dColorCounter colorCounter;
        public GameObject targetObj;

        private void Awake()
        {
            colorCounter = GetComponent<P3dColorCounter>();
        }


        void ColorCount()
        {
        }


    }
}

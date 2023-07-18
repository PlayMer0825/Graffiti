using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insomnia
{
    public class Test : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"{gameObject.name}: OnCollisionEnter");
        }

        private void OnCollisionExit(Collision collision)
        {
            Debug.Log($"{gameObject.name}: OnCollisionExit");
        }

        private void OnCollisionStay(Collision collision)
        {
            Debug.Log($"{gameObject.name}: OnCollisionStay");
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"{gameObject.name}: OnTriggerEnter");
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"{gameObject.name}: OnTriggerExit");
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log($"{gameObject.name}: OnTriggerStay");
        }
    }

}

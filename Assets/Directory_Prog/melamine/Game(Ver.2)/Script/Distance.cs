using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    public Text distanceText;
    float distance;
    public Transform other;
    public Transform other1;

    void Update()
    {
        distance = other.position.x - other1.transform.position.x;
        distanceText.text = distance.ToString();
    }
}

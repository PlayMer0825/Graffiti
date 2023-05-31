using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour
{

    // Start is called before the first frame update

    private void Start()
    {
        //Board.bench_State = false;
    }

    private void Update()
    {
        if (Board.isBench)
        {
            this.GetComponent<BoxCollider>().enabled = true;
        }
        else if(!Board.isBench)
        {
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour
{

    // Start is called before the first frame update

    private void Start()
    {
        Board.bench_State = false;
    }
    private void OnTriigerStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("ok");
            Board.bench_State = true;
        }
        else if(collision.gameObject.tag != "Player")
        {
            Debug.Log("ok");
            Board.bench_State = false;
        }
            
    }

}

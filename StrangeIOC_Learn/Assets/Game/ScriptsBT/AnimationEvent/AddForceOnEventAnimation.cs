using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceOnEventAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rg;
    public Vector2 extra;
    //public float power;
    public bool disable;
    void Start()
    {
        //curPower = power;
    }


    public void AddForceForward(int power)
    {
        rg.AddForce(Vector2.right * power);
    }

    public void AddForceUp(int power)
    {
        if (disable)
        {
            //Debug.Log("Disable");
            power = 0;
            disable = false;
        }
        //Debug.Log("AddForce: " + power);
        rg.AddForce(Vector2.up * power);
    }

    public void AddForceDown(int power)
    {
        //.Log("ad");
        if(power != 0)
        {
            rg.isKinematic = false;
            rg.gravityScale = 3.5f;
            rg.AddForce(-Vector2.up * power);
        }        
    }
}

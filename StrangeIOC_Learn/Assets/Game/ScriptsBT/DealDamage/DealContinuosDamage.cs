using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealContinuosDamage :MonoBehaviour
{
    public BoxCollider2D col;
    public float frequency;
    public float dealDamageTimeStay;
    float curTime;
    bool dealingDamage;

    [Header("Frequency over time")]
    float offset;
    public float rate;
    public float maxFrequency;
    private void OnEnable()
    {
        curTime = dealDamageTimeStay;
        col.enabled = true;
        dealingDamage = true;
        offset = rate;
    }

    private void Update()
    {
        curTime -= Time.deltaTime;
        if(curTime <=0)
        {
            if (dealingDamage)
            {
                col.enabled = false;
                
                offset += rate;
                curTime = frequency - offset;
                if (curTime > maxFrequency)
                    curTime = maxFrequency;
                
                //curTime = frequency;
                dealingDamage = false;
            }
            else
            {
                col.enabled = true;
                curTime = dealDamageTimeStay;
                dealingDamage = true;
            }           
        }
    }

}

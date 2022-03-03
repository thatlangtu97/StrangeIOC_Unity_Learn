using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileRotateByTime : ProjectileMovement
{
    public float speed;
    public AnimationCurve CurveSpeed;
    public float angleStart;
    private float timeCount;
//    public Vector3 direction;
    float fixedDeltaTime;

    private void OnEnable()
    {
        timeCount = 0f;
        transform.rotation = quaternion.Euler(0f, 0f, angleStart);
    }

    public override void UpdatePosition()
    {
//        direction = transform.right * transform.localScale.x;
                       
        fixedDeltaTime = Time.fixedDeltaTime;
        //transform.rotation = quaternion.(speed);
        transform.Rotate(0f,0f,CurveSpeed.Evaluate(timeCount)*speed);
        timeCount += Time.fixedTime;
    }
}

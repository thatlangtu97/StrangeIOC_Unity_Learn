using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileForwardMovement : ProjectileMovement
{
    public float speed;
    public Vector3 direction;
    float fixedDeltaTime;
    public AnimationCurve curve = new AnimationCurve(new Keyframe(0,1f));
    private float timeCount;

    public void OnEnable()
    {
        timeCount = 0;
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    public override void UpdatePosition()
    {
        //updateDirMove();
        //updateRotation();
        //direction = new Vector3(transform.right.x * transform.localScale.x,
        //                        transform.right.y * transform.localScale.y,
        //                        transform.right.z * transform.localScale.z
        //                        );
        direction = transform.right * transform.localScale.x;
                       
        
        timeCount += fixedDeltaTime;
        transform.position += direction * fixedDeltaTime * (speed * curve.Evaluate(timeCount));
    }
    //private void updateDirMove()
    //{
    //    //if (transform.localScale.x < 0)
    //    //{
    //    //    dirMove.x = Mathf.Abs(dirMove.x) * -1f;
    //    //}
    //    //else
    //    //{
    //    //    dirMove.x = Mathf.Abs(dirMove.x);
    //    //}
    //}
    //private void updateRotation()
    //{
    //    //if (transform.localScale.x < 0)
    //    //{
    //    //    transform.right = dirMove * -1f;
    //    //}
    //    //else
    //    //{
    //    //    transform.right = dirMove;
    //    //}
    //}
}

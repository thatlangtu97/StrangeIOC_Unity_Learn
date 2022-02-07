using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileForwardMovement : ProjectileMovement
{
    public float speed;
    public Vector3 direction;
    float fixedDeltaTime;
    public override void UpdatePosition()
    {
        //updateDirMove();
        //updateRotation();
        //direction = new Vector3(transform.right.x * transform.localScale.x,
        //                        transform.right.y * transform.localScale.y,
        //                        transform.right.z * transform.localScale.z
        //                        );
        direction = transform.right * transform.localScale.x;
                       
        fixedDeltaTime = Time.fixedDeltaTime;
        transform.position += direction * speed * fixedDeltaTime;
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

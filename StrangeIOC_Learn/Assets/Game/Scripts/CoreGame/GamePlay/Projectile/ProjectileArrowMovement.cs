using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArrowMovement : ProjectileMovement
{
    public float speed;
    public Vector3 direction;
    public float timeStartMoveDown;
    public TrailRenderer trail;
    [Range(0.1f,1f)]
    public float speedDown;
    Vector3 dirMove;
    float fixedDeltaTime;
    float timeCount = 0;
    private float localScaleX;
    private void OnEnable()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
        localScaleX = transform.localScale.x;
        timeCount = 0f;
        trail.enabled = false;
    }
    private void OnDisable()
    {
        trail.Clear();
    }
    public override void UpdatePosition()
    {
        localScaleX = transform.localScale.x;
        direction = transform.right * transform.localScale.x;
        transform.position +=  direction * speed  * fixedDeltaTime;
        timeCount += fixedDeltaTime;
        trail.enabled = true;
        CaculatePosition();
        updateRotation();


    }
    public override void CaculatePosition()
    {
        if(timeCount > timeStartMoveDown)
        {
            direction.y -= fixedDeltaTime * speedDown ;
        }
    }
    private void updateRotation()
    {
        transform.right = direction / localScaleX ;
    }

    //public Transform enemyTransform;
    //public Vector3 dir;
    //private void OnDrawGizmos()
    //{
    //    Vector3 p0 = transform.position;
    //    Vector3 p1 = dir;
    //    Vector3 p3 = enemyTransform.position;
    //    for (float t = 0f; t < 1f; t += 0.05f)
    //    {
    //        Gizmos.color = Color.yellow;
    //        Vector3 gm = Mathf.Pow(1 - t, 3) * p0 +
    //            3 * Mathf.Pow(1 - t, 2) * t * p1 +
    //            3 * (1 - t) * Mathf.Pow(t, 2) * p1 +
    //            Mathf.Pow(t, 3) * p3;
    //        Vector3 gm2 = Mathf.Pow(1 - (t + 0.05f), 3) * p0 +
    //            3 * Mathf.Pow(1 - (t + 0.05f), 2) * (t + 0.05f) * p1 +
    //            3 * (1 - (t + 0.05f)) * Mathf.Pow((t + 0.05f), 2) * p1 +
    //            Mathf.Pow((t + 0.05f), 3) * p3;
    //        Gizmos.DrawLine(gm, gm2);
    //    }
    //    //Gizmos.color = Color.green;
    //    //Gizmos.DrawLine(startPosition, directionStart);
    //    //Gizmos.DrawLine(directionEnd, enemyTransform.position);

    //}
}

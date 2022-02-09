using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileForwardToTargetMovement : ProjectileMovement
{    public float speed;
    public Vector3 direction;    
    [Range(2f, 10f)]
    public float radiusCastEnemy;
    public LayerMask layerMaskEnemy;
    float fixedDeltaTime;
    Collider2D[] cols;
    public Transform enemyTransform;
    public bool hasTarget;
    private void OnEnable()
    {
//        hasTarget = false;
//        FindEnemy();
    }

    public void FindEnemy()
    {
        cols = Physics2D.OverlapCircleAll(transform.position, radiusCastEnemy, layerMaskEnemy);

        if (cols != null)
        {
            foreach (var col in cols)
            {
                if (col != null)
                {
                    Debug.Log(col.gameObject.name);
                    enemyTransform = col.transform;
                    direction = enemyTransform.position - transform.position;
                    transform.right = direction * transform.localScale.x;
                    hasTarget = true;
                    break;
                }
            }
        }
    }
    public override void UpdatePosition()
    {
//        if (hasTarget)
//        {
        direction = transform.right * transform.localScale.x;
//        * transform.localScale.x;

            fixedDeltaTime = Time.fixedDeltaTime;
            transform.position += direction * speed * fixedDeltaTime;
//        }
    }
}

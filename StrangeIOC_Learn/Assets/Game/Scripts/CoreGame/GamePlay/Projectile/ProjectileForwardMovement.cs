using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileForwardMovement : ProjectileMovement
{
    public float speed;
    public Vector3 direction;
    public Vector3 dirMove;
    float fixedDeltaTime;
    private void OnEnable()
    {
        dirMove = transform.right; /*new Vector3(direction.x * transform.localScale.x, direction.y, direction.z);*/
    }
    public override void UpdatePosition()
    {
        updateDirMove();
        updateRotation();
        direction = transform.right;
        fixedDeltaTime = Time.fixedDeltaTime;
        transform.position += dirMove * speed * fixedDeltaTime;
    }
    private void updateDirMove()
    {
        if (transform.localScale.x < 0)
        {
            dirMove.x = Mathf.Abs(dirMove.x) * -1f;
        }
        else
        {
            dirMove.x = Mathf.Abs(dirMove.x);
        }
    }
    private void updateRotation()
    {
        if (transform.localScale.x < 0)
        {
            transform.right = dirMove * -1f;
        }
        else
        {
            transform.right = dirMove;
        }
    }
}

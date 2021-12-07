using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas.Unity;
using BehaviorDesigner.Runtime;

public class GetGroundPointTask : Action
{
    public LayerMask whatIsGround;
    public SharedVector2 castPoint;
    public SharedVector2 point;

    public override void OnStart()
    {
        RaycastHit2D hit = Physics2D.Raycast(castPoint.Value, Vector2.down, 10f, whatIsGround);
        if (hit.collider != null)
        {
            point.Value = hit.point;
        }
    }

}
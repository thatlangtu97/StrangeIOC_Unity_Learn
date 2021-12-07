using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSetTarget : Action
{
    public LayerMask whatIsGround;
    public Vector2 offSet;
    public Vector2 checkDirection;
    public SharedVector2 target;
    public SharedComponentManager componentManager;
    public override void OnStart()
    {
        base.OnStart();
        RaycastHit2D hit = Physics2D.Raycast(componentManager.Value.entity.centerPoint.centerPoint.position, checkDirection, 100f, whatIsGround);
        if (hit.collider != null)
        {
            target.Value =  hit.point + offSet;
        }
    }
}

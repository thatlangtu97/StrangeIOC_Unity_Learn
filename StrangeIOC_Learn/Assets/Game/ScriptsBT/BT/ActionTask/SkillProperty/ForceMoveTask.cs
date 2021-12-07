using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMoveTask : Action
{
    public SharedComponentManager component;
    public bool isNeedMove;
    public override void OnStart()
    {
        component.Value.entity.isStopMove = !isNeedMove;
        if(isNeedMove)
        {
            component.Value.entity.moveByDirection.direction = transform.right;
        }
       
    }
}

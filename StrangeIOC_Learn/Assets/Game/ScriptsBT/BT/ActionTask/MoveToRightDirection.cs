using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRightDirection : Action
{
    public SharedComponentManager component;
    public override void OnStart()
    {
        component.Value.entity.isStopMove = false;
        component.Value.entity.moveByDirection.direction = transform.right;
    }
}

using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMoveTask : Action
{
    public SharedComponentManager component;
    public override void OnStart()
    {
        component.Value.entity.isStopMove = true;
    }
}

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
public class SetDestinationTask : Action
{
    public SharedComponentManager component;
    public SharedVector2 destination;
    public override void OnStart()
    {
        //Debug.Log(destination.Value);
            component.Value.entity.moveByDestination.destination = destination.Value;
            component.Value.entity.moveByDestination.isEnable = true;
        component.Value.entity.isStopMove = false;
    }
}

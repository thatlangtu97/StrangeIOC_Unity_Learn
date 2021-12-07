using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckReachDestinationConditional : Conditional
{
    public SharedComponentManager component;
    public float minDistance = 0.1f;
    public bool updatePosition;
    public bool disableMovementOnSucess;
    public override TaskStatus OnUpdate()
    {
        if(updatePosition)
        component.Value.entity.moveByDestination.destination = component.Value.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint.position;
        if (Vector2.Distance(transform.position, component.Value.entity.moveByDestination.destination) < minDistance)
        {
            if (disableMovementOnSucess)
                component.Value.entity.moveByDestination.isEnable = false;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDirectionByFacing : Conditional
{
    public SharedComponentManager component;
    public Vector2 direction;
    public SharedFloat distance;
    public LayerMask castMask;
    CharacterDirection characterDirection;
    public override TaskStatus OnUpdate()
    {
        if(characterDirection==null)
        {
            characterDirection = GetComponent<CharacterDirection>();
        }
        var newDir = direction;
        if(characterDirection.isFaceRight==false) // neu left thi dao chieu x
        {
            newDir.x *= -1;
        }
        var hit = Physics2D.Raycast(component.Value.entity.centerPoint.centerPoint.position, newDir, distance.Value, castMask);
        if(hit)
        {
            //if (component.Value.entity.stateMachineContainer.stateMachine.isFreezeAndAddforce)
            //    component.Value.entity.stateMachineContainer.stateMachine.softBody.GetComponent<Detectable>().immuneToForce = true;
            return TaskStatus.Success;
            
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}

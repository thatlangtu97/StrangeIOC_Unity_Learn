using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
public class ImmuneToCCTask : Action
{
    public bool isImmune;
    public bool isImmuneAll;
    Detectable body;
    public override void OnStart()
    {
        if(body==null)
        {
            body = gameObject.GetComponentInChildren<Detectable>();
        }

        
        
        
        if(!isImmuneAll)
        {
            body.immuneToForce = isImmune;
            body.componentManager.entity.stateMachineContainer.stateMachine.immuneHit = isImmune;
            body.componentManager.entity.stateMachineContainer.stateMachine.immuneKnock = isImmune;
            body.componentManager.entity.stateMachineContainer.stateMachine.isImmuneStun = isImmune;
        }
        else
        {
            body.immune = isImmune;
        }
       
    }
}

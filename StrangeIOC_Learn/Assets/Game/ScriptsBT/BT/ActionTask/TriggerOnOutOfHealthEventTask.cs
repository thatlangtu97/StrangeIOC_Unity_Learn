using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnOutOfHealthEventTask : Action
{
    // Start is called before the first frame update
    public SharedComponentManager component;
    public override void OnStart()
    {
        component.Value.entity.stateMachineContainer.stateMachine.OnRunOutOfHealth(gameObject);
    }
}

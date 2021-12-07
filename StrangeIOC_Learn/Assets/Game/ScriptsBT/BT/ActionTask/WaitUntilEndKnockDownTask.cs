using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUntilEndKnockDownTask : Action
{
    public SharedComponentManager component;

    public override void OnStart()
    {
   
        //base.OnStart();
    }
    public override TaskStatus OnUpdate()
    {
        
     
        return TaskStatus.Success;

    }

}

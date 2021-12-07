using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTime : Action
{
    public float waitTime;
    float currWaitTime;
    public override void OnStart()
    {
        currWaitTime = waitTime;
    }
    public override TaskStatus OnUpdate()
    {
        currWaitTime -= Time.deltaTime;
        if(currWaitTime<=0f)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}

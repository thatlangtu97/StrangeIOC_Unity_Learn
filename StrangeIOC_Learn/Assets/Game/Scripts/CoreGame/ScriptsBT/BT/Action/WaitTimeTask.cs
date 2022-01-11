﻿using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[TaskDescription(@"bộ đếm time theo time scale của Component manager với time scale của Unity")]
[TaskCategory("Extension")]
[TaskIcon("{SkinColor}WaitIcon.png")]
public class WaitTimeTask : Action
{
    public SharedComponentManager componentManager;
    public SharedFloat waitTime = 1;
    private float startTime;
    public override void OnStart()
    {
        // Remember the start time.
        startTime =waitTime.Value;

    }

    public override TaskStatus OnUpdate()
    {
       
        if (startTime <=0)
        {
            return TaskStatus.Success;
        }
        startTime -= Time.deltaTime * componentManager.Value.timeScale;
        // Otherwise we are still waiting.
        return TaskStatus.Running;
    }
}
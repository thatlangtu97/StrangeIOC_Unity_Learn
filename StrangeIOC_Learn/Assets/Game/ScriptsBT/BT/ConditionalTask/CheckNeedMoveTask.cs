﻿using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNeedMoveTask : Conditional
{
    public SharedComponentManager componentManager;
    public SharedFloat rangeToEnemy;
    public float stopRange=0.99f;
    public float cooldown = 0.5f;
    float lastTime = 0f;
    public bool useRandomRange;
    public override TaskStatus OnUpdate()
    {
        if(useRandomRange)
        stopRange = Random.Range(1, 5);
        if(Time.timeSinceLevelLoad-lastTime>cooldown)
        {
            if (rangeToEnemy.Value > stopRange || rangeToEnemy.Value < -stopRange)
            {
                return TaskStatus.Success;

            }
            else
            {
                lastTime = Time.timeSinceLevelLoad;
                return TaskStatus.Failure;
                
            }
        }
        else
        {
            return TaskStatus.Failure;
        }
       


    }
}

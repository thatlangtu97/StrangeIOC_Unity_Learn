using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class CheckNeedMoveTask : Conditional
{
    public SharedComponentManager componentManager;
    public SharedFloat rangeToEnemy;
    public float distance;
    //public float stopRange =/*0.99f;*/0f;
    //public float cooldown = 0.5f;
    //float lastTime = 0f;
    //public bool useRandomRange;
    public override TaskStatus OnUpdate()
    {

        if (rangeToEnemy.Value < distance)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;


    }
}

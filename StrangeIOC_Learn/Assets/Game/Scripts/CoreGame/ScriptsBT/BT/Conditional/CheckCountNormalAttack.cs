using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class CheckCountNormalAttack : Conditional
{
    public SharedInt countNormalAttack;
    public int maxNormalAttack;
    public override TaskStatus OnUpdate()
    {
        if(countNormalAttack.Value< maxNormalAttack)
        {
            return TaskStatus.Success;
        }
        else
        {
            //countNormalAttack.Value = 0;
            return TaskStatus.Failure;
        }
    }
}

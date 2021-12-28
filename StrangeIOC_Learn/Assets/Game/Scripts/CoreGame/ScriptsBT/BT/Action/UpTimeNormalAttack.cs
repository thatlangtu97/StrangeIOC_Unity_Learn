using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class UpTimeNormalAttack : Action
{
    public SharedInt countNormalAttack;
    public override void OnStart()
    {
        base.OnStart();
        countNormalAttack.Value += 1;
    }
}

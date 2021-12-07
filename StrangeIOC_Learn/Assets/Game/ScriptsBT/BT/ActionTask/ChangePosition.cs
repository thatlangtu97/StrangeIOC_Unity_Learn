using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosition : Action
{
    public SharedVector2 pos;
    public override void OnStart()
    {
        transform.position = pos.Value;

    }
}

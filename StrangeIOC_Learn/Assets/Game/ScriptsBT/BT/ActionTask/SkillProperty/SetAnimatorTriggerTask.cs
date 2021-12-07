using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorTriggerTask : Action
{
    public SharedAnimator animator;
    public string triggerName;
    public override void OnStart()
    {
        base.OnStart();
        animator.Value.SetTrigger(triggerName);
    }
}

using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDownAnimTask : Action
{
    public SharedComponentManager component;
    public bool isDown;
    public override void OnStart()
    {
        base.OnStart();
        if(isDown)
        {
            component.Value.entity.animatorContainer.animator.SetTrigger(AnimationName.KNOCK_DOWN);
            component.Value.entity.animatorContainer.animator.SetBool(AnimationName.LOCK_HIT_ANIM, true);
        }
        else
        {
            component.Value.entity.animatorContainer.animator.SetTrigger(AnimationName.GET_UP);
            component.Value.entity.animatorContainer.animator.SetBool(AnimationName.LOCK_HIT_ANIM, false);
            component.Value.entity.animatorContainer.animator.ResetTrigger(AnimationName.HIT);
        }
  
    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}

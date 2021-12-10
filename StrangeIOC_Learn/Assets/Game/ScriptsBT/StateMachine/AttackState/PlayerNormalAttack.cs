using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttack : State
{
    
    public float timecount;
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.isAttack = true;
        controller.animator.SetTrigger(AnimationTriger.ATTACK+1);
    }
    public override void UpdateState()
    {
        base.UpdateState();
    }
    public override void ExitState()
    {
        base.ExitState();
        controller.componentManager.isAttack = false;
    }
}

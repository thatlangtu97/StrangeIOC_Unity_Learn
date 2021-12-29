using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyNormalAttack", menuName = "State/EnemyNormalAttack")]
public class EnemyNormalAttack : State
{
    
    public override void EnterState()
    {
        base.EnterState();
        //controller.componentManager.isAttack = true;
        controller.animator.SetTrigger(AnimationTriger.ATTACK);
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
    public override void OnHit()
    {
        base.OnHit();
        controller.ChangeState(NameState.HitState);
    }
}

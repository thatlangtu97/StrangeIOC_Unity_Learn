using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyMeleeMoveState", menuName = "State/Enemy/EnemyMeleeMoveState")]
public class EnemyMeleeMoveState : State
{
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(AnimationTriger.MOVE);
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if(controller.componentManager.speedMove!=0)
            controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.speedMove * controller.componentManager.timeScale, 0f) /** Time.deltaTime */;
        else
        {
            controller.ChangeState(NameState.IdleState);
        }
    }
    public override void OnHit()
    {
        base.OnHit();
        controller.ChangeState(NameState.HitState);
    }
}

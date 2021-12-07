using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Jump Fall State", menuName = "State/Enemy/EnemyJumpFallState")]
public class EnemyJumpFallState : State
{
    bool isFalling;
    bool isLanding;
    float curTimeLanding;
    float waitLanding = 0.5f;
   
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
      
    }

    public override void EnterState()
    {
        base.EnterState();
       
        curTimeLanding = waitLanding;
        controller.animator.SetTrigger(AnimationName.FALL);
        isFalling = true;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale = controller.enemyJumpConfig.fallGravity;
    }

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);     
        if (isFalling)
        {
            if (controller.componentManager.entity.checkGround.isOnGround)
            {

                controller.animator.SetTrigger(AnimationName.END_FALL);
                isLanding = true;
                isFalling = false;
            }
        }

        if (isLanding)
        {
            curTimeLanding -= Time.deltaTime;
            if (curTimeLanding <= 0)
            {
                controller.ChangeState(controller.idleState);
            }
        }

    }

    public override TaskStatus OnInputIdle()
    {
        return TaskStatus.Failure;
    }

    public override TaskStatus OnInputChangeFacing()
    {
        return TaskStatus.Failure;
    }
    public override void ExitState()
    {
        base.ExitState();
        controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale = controller.enemyJumpConfig.originalGravity;
        isFalling = false;
        isLanding = false;
    }

    public override TaskStatus OnInputSkill(int skillId)
    {
        Debug.Log("InputSkill");
        return base.OnInputSkill(skillId);
    }

    
}

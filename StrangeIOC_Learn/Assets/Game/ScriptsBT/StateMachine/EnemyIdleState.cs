using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy Idle State", menuName = "State/Enemy/EnemyIdleState")]
public class EnemyIdleState : IdleState
{

    float idleTime;
    public override void EnterState()
    {
        if (controller.componentManager.entity.hasCheckGround && controller.jumpFallState != null)
        {
            if (!controller.componentManager.entity.checkGround.isOnGround)
            {
                controller.ChangeState(controller.jumpFallState);
                return;
            }         
        }
        controller.animator.SetBool(AnimationName.MOVE, false);
        base.EnterState();
      
        //if (controller.behaviorTree.enabled == false)
            //controller.behaviorTree.enabled = true;
        //idleTime = Random.Range(ConstantValueManager.instance.minIdleTime, ConstantValueManager.instance.maxIdleTime);
        //controller.componentManager.entity.isStopMove = true;
       
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);

        //idleTime -= deltaTime;
        //if (controller.componentManager.entity.health.HP.Value <= 0)
        //{
        //    if (controller.behaviorTree.enabled == true)
        //        controller.behaviorTree.enabled = false;
        //    CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = false; } });
        //    if (controller.componentManager.entity.isEnabled == true)
        //    {
        //        controller.componentManager.entity.isAttackDone = true;
        //        CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = false; controller.componentManager.entity.isSkillComplete = false; } });
        //    }
        //    controller.ChangeState(controller.dieState);
        //}
        //if (idleTime <= 0f)
        //{
        //    controller.ChangeState(controller.moveState); // chuyen sang patrol
        //    return;
        //}

    }
    public override TaskStatus OnInputIdle()
    {
        return TaskStatus.Success;
    }
    public override TaskStatus OnHit(bool isBlock)
    {
        
        controller.ChangeState(controller.beHitState);
        return TaskStatus.Success;

    }

    public override TaskStatus OnInputJump()
    {
        if (controller.componentManager.entity.checkGround.isOnGround)
            controller.ChangeState(controller.jumpState);
        else
            controller.ChangeState(controller.jumpFallState);
        return TaskStatus.Success;
    }
}

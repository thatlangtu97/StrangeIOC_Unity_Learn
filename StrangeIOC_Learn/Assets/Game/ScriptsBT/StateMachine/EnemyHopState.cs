using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Hop State", menuName = "State/Enemy/Hop")]
public class EnemyHopState : MoveState
{
    public float hopTime;
    public float delayHop;
    public float hopDuration;
    float curTime;
    bool hop;
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void EnterState()
    {
        base.EnterState();
        hop = false;
        curTime = hopTime;
        
        controller.animator.SetTrigger(AnimationName.MOVE);
        if (controller.componentManager.entity.health.HP.Value <= 0)
        {
            if (controller.behaviorTree.enabled == true)
                controller.behaviorTree.enabled = false;
            CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = false; } });
            if (controller.componentManager.entity.isEnabled == true)
            {
                controller.componentManager.entity.isAttackDone = true;
                CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = false; controller.componentManager.entity.isSkillComplete = false; } });
            }
            controller.ChangeState(controller.dieState);
        }
    }
    public override void UpdateState(float deltaTime)
    {
        

        if(curTime <= delayHop && !hop)
        {
            Hop();
        }

        if(curTime <= hopDuration)
        {
            StopHop();
        }

        curTime -= Time.deltaTime;
        if (controller.componentManager.entity.health.HP.Value <= 0)
        {
           
            if (controller.behaviorTree.enabled == true)
                controller.behaviorTree.enabled = false;
            CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = false; } });
            if (controller.componentManager.entity.isEnabled == true)
            {
                controller.componentManager.entity.isAttackDone = true;
                CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = false; controller.componentManager.entity.isSkillComplete = false; } });
            }
            controller.ChangeState(controller.dieState);
        }

        if (curTime <= 0)
        {
            ExitState();
        }
    }
    public override void ExitState()
    {
        controller.componentManager.entity.isStopMove = true;
        base.ExitState();
    }


    void ChangeDirection()
    {
        controller.characterDirection.ChangeDirection();
        controller.componentManager.entity.moveByDirection.direction = controller.transform.right;
        controller.componentManager.entity.checkWall.checkDirection = controller.transform.right;
    }
    public override TaskStatus OnHit(bool isBlock)
    {

        controller.ChangeState(controller.beHitState);
        return TaskStatus.Success;
    }
    public override TaskStatus OnInputMove()
    {
        controller.componentManager.entity.moveByDirection.direction = controller.transform.right;
        controller.componentManager.entity.checkWall.checkDirection = controller.transform.right;
        return TaskStatus.Success;
    }

    void Hop()
    {
        hop = true;
        controller.componentManager.entity.moveByDirection.speedScale = 1;
        controller.componentManager.entity.moveByDirection.isEnable = true;
        controller.componentManager.entity.isStopMove = false;
        controller.componentManager.entity.moveByDirection.direction = controller.transform.right;
        controller.componentManager.entity.checkWall.checkDirection = controller.transform.right;
    }

    void StopHop()
    {
        controller.componentManager.entity.moveByDirection.speedScale = 0;
        controller.componentManager.entity.isStopMove = true;
        controller.componentManager.entity.moveByDirection.direction = controller.transform.right;
        controller.componentManager.entity.checkWall.checkDirection = controller.transform.right;
    }
}

using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy Patrol State", menuName = "State/Enemy/Patrol")]
public class MeleeEnemyMoveState : MoveState
{

    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.entity.moveByDirection.isEnable = true;
        controller.componentManager.entity.moveByDirection.currSpeed = controller.componentManager.entity.moveByDirection.originalSpeed + Random.Range(-1.5f, 1.5f);
        controller.animator.SetBool(AnimationName.MOVE, true);
      
        controller.componentManager.entity.isStopMove = false;
        controller.componentManager.entity.moveByDirection.direction = controller.transform.right;
        controller.componentManager.entity.checkWall.checkDirection = controller.transform.right;
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
        //Debug.Log("MOveState: " + controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity);
        if (controller.componentManager.entity.health.HP.Value <= 0)
        {
            //if (controller.behaviorTree != null)
            //    Destroy(controller.behaviorTree);
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
        //base.UpdateState(deltaTime);
        //patrolTime -= deltaTime;
        //if (patrolTime <= 0f)
        //{
        //    controller.ChangeState(controller.idleState);
        //}
        ////check het duong di => change direction
        //if (controller.componentManager.entity.hasCheckWall)
        //{
        //    if (controller.componentManager.entity.checkWall.isHitWall)
        //    {
        //        //Change Direction
        //        ChangeDirection();
        //    }
        //}
        ////if(controller.componentManager.entity.hasCheckWalkable)
        ////{
        ////    if(!controller.componentManager.entity.checkWalkable.isWalkAble)
        ////    {
        ////        //Change Direction
        ////        ChangeDirection();
        ////    }
        ////}
        //if (controller.componentManager.entity.checkEnemyInSigh.enemy != null)
        //{
        //    controller.ChangeState(controller.chaseState);
        //    return;

        //}
        // check enemy in sight => ve chase
    }
    public override void ExitState()
    {
        controller.componentManager.entity.isStopMove = true;
        controller.animator.SetBool(AnimationName.MOVE, false);
        base.ExitState();
        //if (controller.componentManager.entity.health.HP.Value <= 0)
        //{


        //    //if (controller.behaviorTree != null)
        //    //    Destroy(controller.behaviorTree);
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
}

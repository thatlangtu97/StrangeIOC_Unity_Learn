using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy Be Hit State", menuName = "State/Enemy/Be Hit Block")]
public class EnemyHitBlockState : State
{
    public float blockTime = 0.2f;
    float curTime;
    public override void EnterState()
    {
        curTime = blockTime;
        base.EnterState();
        
       
       
        if (controller.ShieldHP > 0)
        {
            controller.animator.SetBool(AnimationName.MOVE, false); // if move anim is in play
            controller.animator.SetTrigger(AnimationName.BLOCK);
        }
        else
        {
            controller.animator.SetTrigger(AnimationName.HIT);
        }
        
        
        
        //Play sound Hit

        /*
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
        */

    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        
        curTime -= Time.deltaTime;
        if (curTime <= 0f && controller.componentManager.entity.health.HP.Value > 0)
        {
            controller.ChangeState(controller.idleState);
            return;
        }
        /*
        else if (controller.componentManager.entity.health.HP.Value <= 0)
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
            return;
        }*/
        //else if (controller.componentManager.entity.health.HP.Value <= 0)
        //{
        //    if(controller.currentState != controller.dieState)
        //        controller.ChangeState(controller.dieState);
        //}
        //if (controller.isHit == false)
        //{
        //    controller.ChangeState(controller.idleState);
        //    return;
        //}*/
    }
    public override void ExitState()
    {
        base.ExitState();
        if (controller.ShieldHP <= 0)
            controller.beHitState = null;
        // controller.animator.SetLayerWeight((int)LayerWeightID.HIT, 0f);
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
    public override TaskStatus OnHit(bool isBlock)
    {

        EnterState();
        return TaskStatus.Success;
    }
    public override TaskStatus OnInputIdle()
    {
        return TaskStatus.Failure;
    }
    public override TaskStatus OnInputSkill(int skillId)
    {
        return TaskStatus.Failure;
    }
    public override TaskStatus OnInputMove()
    {
        return TaskStatus.Failure;
    }
}

using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy Be Hit State", menuName = "State/Enemy/Be Hit")]
public class EnemyHitState : State
{
    float knockTime = 0.6f;
    public override void EnterState()
    {

        base.EnterState();

        knockTime = controller.beHitTime;
        int rd = Random.Range(0, 100);
        controller.animator.SetBool(AnimationName.MOVE, false); // if move anim is in play

        int randomHitAnim = Random.Range(0, controller.CountAnimHit);
        if (controller.CountAnimHit ==2)
            if(randomHitAnim==0)
                controller.animator.SetTrigger(AnimationName.HIT);
            else
                controller.animator.SetTrigger(AnimationName.HIT2);
        else
            controller.animator.SetTrigger(AnimationName.HIT);
        //Play sound Hit

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
        //if (rd > 50)
        //{
        //    controller.animator.SetTrigger(AnimationName.HIT);
        //}
        //else
        //{
        //    controller.animator.SetTrigger(AnimationName.HIT2);
        //}

    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        knockTime -= Time.deltaTime;
        if (knockTime <= 0f && controller.componentManager.entity.health.HP.Value > 0)
        {
            controller.ChangeState(controller.idleState);
            return;
        }
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
        }
        //else if (controller.componentManager.entity.health.HP.Value <= 0)
        //{
        //    if(controller.currentState != controller.dieState)
        //        controller.ChangeState(controller.dieState);
        //}
        //if (controller.isHit == false)
        //{
        //    controller.ChangeState(controller.idleState);
        //    return;
        //}
    }
    public override void ExitState()
    {
        base.ExitState();
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

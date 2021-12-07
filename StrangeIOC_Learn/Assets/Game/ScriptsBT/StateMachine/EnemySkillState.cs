using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy Skill State", menuName = "State/Enemy/Skill")]
public class EnemySkillState : SkillState
{
    public bool canBeHit;
    public bool canBeKnockDown;
    public override void EnterState()
    {

        base.EnterState();

        if (controller.componentManager.entity.health.HP.Value <= 0)
        {
            //if (controller.behaviorTree != null)
            //    Destroy(controller.behaviorTree);\
            if (controller.behaviorTree.enabled == true)
                controller.behaviorTree.enabled = false;

            controller.ChangeState(controller.dieState);
        }

        // controller.selectedSkill.CastSkill(controller.componentManager.entity);
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
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
            return;
        }
        if ((controller.componentManager.entity.isAttackComplete || controller.componentManager.entity.isSkillComplete) && controller.componentManager.entity.health.HP.Value >0)
        {
           
            controller.ChangeState(controller.idleState);
            return;
        }


    }
    public override void ExitState()
    {
        //Debug.Log("out");
        // controller.componentManager.entity.isAttackComplete = true;
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
        if (controller.componentManager.entity.isAttackComplete== false)
        {
            CleanUpBufferManager.instance.AddReactiveComponent(() =>
            {
                if (controller.componentManager.entity.isEnabled)
                    controller.componentManager.entity.isAttackComplete = true;
            },
                    () =>
                    {
                        if (controller.componentManager.entity.isEnabled) controller.componentManager.entity.isAttackComplete = false;
                    });
        }
        //if(controller.componentManager.entity.isSkillComplete == false)

        //    CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = false; } });

        base.ExitState();
    }
    public override TaskStatus OnHit(bool isBlock)
    {

      //  base.OnHit(priority);
      if(canBeHit)
      {
            if (controller.componentManager.entity.isAttackComplete == false)
            {
                CleanUpBufferManager.instance.AddReactiveComponent(() =>
                {
                    if (controller.componentManager.entity.isEnabled)
                        controller.componentManager.entity.isAttackComplete = true;
                },
                        () =>
                        {
                            if (controller.componentManager.entity.isEnabled) controller.componentManager.entity.isAttackComplete = false;
                        });
            }
            controller.ChangeState(controller.beHitState);
            return TaskStatus.Success;
        }
        return TaskStatus.Success;
    }
    public override TaskStatus OnInputSkill(int skillId)
    {
        //Debug.Log("skill fail");

        return TaskStatus.Failure;
    }
    public override TaskStatus OnInputIdle()
    {
        
        return TaskStatus.Success;
    }

    public override TaskStatus OnInputMove()
    {
       
        return TaskStatus.Success;
    }
    public override TaskStatus OnInputChangeFacing()
    {
        if(controller.isCanRotate)
        {
            //Debug.Log("Rotate");
            base.OnInputChangeFacing();
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }

    public TaskStatus ForceExitSkillState()
    {
        controller.ChangeState(controller.idleState);
        return TaskStatus.Success;
    }
}

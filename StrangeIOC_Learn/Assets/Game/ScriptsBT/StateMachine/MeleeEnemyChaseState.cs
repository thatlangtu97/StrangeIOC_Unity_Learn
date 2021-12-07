using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy Chase State Melee", menuName = "State/Enemy/MeleeChase")]
public class MeleeEnemyChaseState : ChaseState
{
    //float minRangeToCastSkill;
    //float rangeToEnemy = 0f;
    //float randomBonusRange = 0f;
    //bool isNotSet;
    //bool isIdle;
    //public override void InitState(StateMachineController controller)
    //{
    //    base.InitState(controller);
    //    randomBonusRange = Random.Range(-0.5f, 0.5f);

    //}
    //public override void EnterState()
    //{
    //    base.EnterState();
    //    isNotSet = true;
    //    float minRange = Mathf.Infinity;
    //    float rdSpeed = Random.Range(-0.5f, 0.5f);
    //    controller.componentManager.entity.moveByDirection.currSpeed = controller.componentManager.entity.moveByDirection.originalSpeed + rdSpeed;
    //    controller.animator.speed = controller.componentManager.entity.moveByDirection.currSpeed / controller.componentManager.entity.moveByDirection.originalSpeed;
    //    foreach (var skill in controller.componentManager.entity.skillContainer.allSkill)
    //    {
    //        float range = skill.GetRange();
    //        if (range < minRange)
    //        {
    //            minRange = range;
    //        }
    //    }
    //    minRangeToCastSkill = minRange + Random.Range(-0.2f, 0); // de enemy k dinh vao nhau

    //    CheckStopRange();

    //}
    ////public override void UpdateState(float deltaTime)
    ////{

    ////    base.UpdateState(deltaTime);

    ////    if (controller.componentManager.entity.checkEnemyInSigh.enemy == null)
    ////    {
    ////        controller.ChangeState(controller.idleState);
    ////        return;
    ////    }
    ////    else
    ////    {


    ////        CheckStopRange();


    ////        controller.selectedSkill = SelectSkillToCast();
    ////        if (controller.selectedSkill != null)
    ////        {
    ////            FaceToEnemy();
               
    ////            controller.ChangeState(controller.attackState);
               
    ////            return;
    ////        }
    ////    }
    ////}

    //void CheckStopRange()
    //{
    //    rangeToEnemy = CalculateRangeToEnemy();

    //    if ((minRangeToCastSkill >= Mathf.Abs(rangeToEnemy) && (!isIdle || isNotSet)) )
    //    {
    //        controller.animator.SetTrigger(AnimationName.IDLE);
    //        controller.animator.SetBool(AnimationName.MOVE, false);
    //        controller.componentManager.entity.isStopMove = true;
    //        isNotSet = false;
    //        isIdle = true;
    //        return;

    //        // controller.ChangeState(controller.idleState);
    //    }

    //    if (minRangeToCastSkill < Mathf.Abs(rangeToEnemy) && (isIdle || isNotSet))
    //    {
    //        controller.componentManager.entity.isStopMove = false;

    //        controller.animator.SetBool(AnimationName.MOVE, true);
    //        FaceToEnemy();
    //        controller.componentManager.entity.moveByDirection.direction = controller.transform.right;
    //        isNotSet = false;
    //        isIdle = false;
    //    }

    //}
    //public override void ExitState()
    //{
       
    //    base.ExitState();
    //    controller.animator.speed = 1f;
    //}
    //BaseSkillConfig SelectSkillToCast()
    //{
    //    BaseSkillConfig skillToCast = null;
    //    float highestCooldownSkill = 0f;
    //    foreach (var skill in controller.componentManager.entity.skillContainer.allSkill)
    //    {
    //        if (skill.currCooldown <= 0f && minRangeToCastSkill >= Mathf.Abs(rangeToEnemy)) //buffer them 1 chut
    //        {
    //            if (skill.cooldown >= highestCooldownSkill) // luon chon skill co cooldown lon nhat de cast
    //            {
    //                skillToCast = skill;
    //            }
    //        }
    //    }

    //    return skillToCast;
    //}
    //float CalculateRangeToEnemy()
    //{
    //    return controller.componentManager.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint.position.x - controller.centerPoint.position.x;
    //}
    //void FaceToEnemy()
    //{
    //    if (rangeToEnemy > 0f) //enemy dang o ben phai
    //    {
    //        if (!controller.characterDirection.isFaceRight) // neu k dang quay mat ben phai thi change direction
    //        {
    //            controller.characterDirection.ChangeDirection();
    //        }
    //    }
    //    else
    //    {
    //        if (controller.characterDirection.isFaceRight)
    //        {

    //            controller.characterDirection.ChangeDirection();
    //        }
    //    }
    //}
    //public override TaskStatus OnHit(bool isBlock)
    //{
        
    //    controller.ChangeState(controller.beHitState);
    //    return TaskStatus.Success;
    //}

}

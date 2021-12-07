using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Attack State", menuName = "State/Player/Attack")]
public class PlayerAttackState : SkillState
{    
    public ComboDataConfig comboSet;
    ComboDataConfig combo;
    public int skillIdToExecute = -1;
    public bool isProjectileCombo;
    bool buffer;
    int currCombo = 0;
    bool isAttack;
    int curComboExecuting;
    int curEvent;
    bool lockInput;
    public override void EnterState()
    {

        base.EnterState();
        curEvent = 0;
        if (controller.curCombo == 0)
        {
            currCombo = controller.curCombo;
            //Debug.Log("ResetCombo");
        }
        curComboExecuting = currCombo;
        if (currCombo > combo.combos.Count - 1)
        {
            currCombo = 0;
            controller.curCombo = currCombo;
            
            //Debug.Log(controller.attackSoundController.slash1[currCombo]);
        }
        
        EffectSoundController.instance.hitClip = controller.attackSoundController.slash1[currCombo];
        if (!controller.componentManager.entity.checkGround.isOnGround && controller.jumpAttackCharge>0)//neu o tren khong thi danh tren khong
        {
            controller.ChangeState(controller.airAttackState);
            return;
        }
        
        if (controller.componentManager.entity.checkGround.isOnGround)
        {
            
            //Debug.Log("attack" + Time.time);          
            controller.attackSoundController.PlaySoundCombo1(currCombo, 0);
            controller.componentManager.entity.isStopMove = true;
            controller.animator.SetBool(AnimationName.MOVE, false);
           // controller.animator.SetBool(AnimationName.IS_ON_ATTACK, true);
            skillIdToExecute = -1;
            if (/*buffer == false &&*/ isAttack == false)
            {
                //Debug.Log("curCombo: " + currCombo);
                if (!isProjectileCombo)
                    controller.forceOnEvent.SetForceOnGroundCombo(currCombo);
                else
                {
                    controller.shootEvent.SetGroundAngle();
                    controller.shootEvent.SetNumberOfProjectile(currCombo, false);
                }
                    
                StartEventOnCombo();
                combo.combos[currCombo].CastSkill(controller.componentManager.entity, null);
                lockInput = false;
                controller.buffer.SetTimerOnFirstAttackPress();            
                currCombo++;
                controller.curCombo = currCombo;
                // controller.animator.SetTrigger(animCombo[0]);
                isAttack = true;
            }

        }
        else// neu ko o tren mat dat thi ko danh dc
        {         
            buffer = false;
            //controller.ChangeState(controller.idleState);
            controller.ChangeState(controller.jumpFallState);
            return;
        }
    }

    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);      
        combo = Instantiate(comboSet);
        controller.groundComboData = combo;
    }

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        //Debug.Log("AttackState");
        UpdateEventOncombo();
        if (controller.curCombo == 0)
        {
            currCombo = controller.curCombo;
            curComboExecuting = currCombo;
        }

            
   
        if (controller.componentManager.entity.isAttackDone)
        {
            if (controller.characterDirection != null)
                controller.characterDirection.lockDirection = false;
            //Debug.Log("combo" + currCombo + " time " + Time.time);
            isAttack = false;
            controller.componentManager.entity.isAttackDone = false;
            if (buffer == true)
            {
                if (!controller.componentManager.entity.checkGround.isOnGround)
                {
                    if (controller.jumpAttackCharge > 0)
                    {
                        controller.ChangeState(controller.airAttackState);
                    }
                    
                    return;
                }
                if (currCombo <= combo.combos.Count - 1)
                {               
                    buffer = false;                  
                    if (!isProjectileCombo)
                        controller.forceOnEvent.SetForceOnGroundCombo(currCombo);
                    else
                        controller.shootEvent.SetNumberOfProjectile(currCombo, false);
                    StartEventOnCombo();
                    combo.combos[currCombo].CastSkill(controller.componentManager.entity, null);
                    lockInput = false;
                    curEvent = 0;
                    curComboExecuting++;
                    controller.buffer.SetTimerOnFirstAttackPress();
                    EffectSoundController.instance.hitClip = controller.attackSoundController.slash1[currCombo];
                    controller.attackSoundController.PlaySoundCombo1(currCombo, 0);
                    currCombo++;
                        
                    controller.curCombo = currCombo;                
                    isAttack = true;

                }
                else
                {                
                    buffer = false;                  
                    controller.ChangeState(controller.idleState);                   
                }

            }
            else//no next attack
            {                   
                buffer = false;            
            }
        }

        if(controller.componentManager.entity.isAnimationEnd && buffer)
        {
            buffer = false;
            isAttack = false;
            if(currCombo >= combo.combos.Count)
            {
                controller.curCombo = 0;
                controller.ChangeState(controller.idleState);        
                return;
            }              
            EnterState();
        }
        else if(controller.componentManager.entity.isAnimationEnd && !buffer)
        {
            buffer = false;
            isAttack = false;
            controller.curCombo = 0;
            controller.ChangeState(controller.idleState);
        }

    }
    public override void ExitState()
    {
        base.ExitState();
        //currCombo = 0;     
        curComboExecuting = 0;
        buffer = false;
        isAttack = false;
        //controller.componentManager.entity.groundCombo.attackBuffer = false;
        //controller.componentManager.entity.groundCombo.isAttacking = false;
        controller.componentManager.entity.isAttackDone = false;
        controller.componentManager.entity.isStopMove = false;
        if (controller.componentManager.entity.hasDash)
            controller.componentManager.entity.RemoveDash();
        //controller.animator.SetBool(AnimationName.IS_ON_ATTACK, false);
    }
    public override TaskStatus OnInputAttack()
    {
        base.OnInputAttack();

        //if (!isAttack) //call when not attacking
        //    controller.buffer.SetTimerOnFirstAttackPress();
        /*
        if (!controller.buffer.AllowToTakeInput())
        {
            return TaskStatus.Failure;
        }
        */
        //Debug.Log("InputAttack");
        if(lockInput)
            return TaskStatus.Success;
        if (isAttack )
        {
            buffer = true;
            lockInput = true;
        }
        else//recovery
        {
            lockInput = true;
            buffer = true;
            //Debug.Log("MissCombo");
            //EnterState();
        }
        
        return TaskStatus.Success;
    }
    public override TaskStatus OnInputJump()
    {
        base.OnInputJump();
        // controller.componentManager.entity.jump.isEnable = true;
        ExitState();
        controller.curCombo = 0;
        controller.ChangeState(controller.jumpState);
        //Debug.Log("JumpOnAttack");
        return TaskStatus.Success;
    }

    public override TaskStatus OnInputSkill(int skillID)
    {
        ////base.OnInputSkill(skillID);
        ////skillIdToExecute = skillID;
        //controller.ChangeState(controller.skillState);
        //controller.componentManager.entity.skillContainer.allSkill[skillID].CastSkill(controller.componentManager.entity);
        //controller.componentManager.entity.skillContainer.BehaviorTrees[skillID].EnableBehavior();
        //controller.componentManager.entity.skillContainer.allSkill[skillID].currCooldown = controller.componentManager.entity.skillContainer.allSkill[skillID].cooldown;
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            //Debug.Log("InputSkill");
            controller.componentManager.entity.isAttackDone = true;
            OnPlayerSkill(skillID);
        }
        return TaskStatus.Success;

    }
    public override TaskStatus OnInputMoveLeft()
    {
        base.OnInputMoveLeft();
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            if (controller.characterDirection.isFaceRight)
            {
                controller.characterDirection.ChangeDirection();
            }
        }
        return TaskStatus.Success;

    }
    public override TaskStatus OnInputMoveRight()
    {
        base.OnInputMoveRight();
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            if (!controller.characterDirection.isFaceRight)
            {
                controller.characterDirection.ChangeDirection();
            }
        }
        return TaskStatus.Success;
    }
    public override TaskStatus OnKnockDown()
    {
        base.OnKnockDown();
        //Debug.Log("PlayerAttackKnockDown");
        return TaskStatus.Success;
    }
    public override void OnForceExitState()
    {
        //if (Time.time - startTime > 0.1f)
        //{
        //    if (controller.componentManager.entity.checkGround.isOnGround)
        //    {
        //        controller.ChangeState(controller.idleState);
        //    }
        //    else
        //    {
        //        controller.ChangeState(controller.jumpFallState);
        //    }
        //    controller.componentManager.entity.isAttackComplete = false;
        //}
    }

    public override void OnDropDown()
    {
        controller.contactedPlatform = Physics2D.Raycast(controller.centerPoint.position, Vector2.down, 0.7f, controller.platFormCastMask);
        if (controller.contactedPlatform)
        {
            Physics2D.IgnoreCollision(controller.contactedPlatform.collider, controller.bodyCollider);
        }
    }

    void StartEventOnCombo()
    {
        combo.triggerConsumed = false;
        timeCounter.StartCount();
    }
    
    void UpdateEventOncombo()
    {
        timeCounter.UpdateTimeCount(Time.deltaTime);
        
        if (curComboExecuting > combo.combos.Count - 1)
            curComboExecuting = combo.combos.Count - 1;
        
        if (curEvent > combo.combos[curComboExecuting].eventOnCombo.Count - 1)
        {

            curEvent = 0;
            return;
        }
     
        if (combo.combos[curComboExecuting].eventOnCombo.Count == 0)
            return;
        if(timeCounter.elapseTime >= combo.combos[curComboExecuting].eventOnCombo[curEvent].TriggerAfterTime && !combo.triggerConsumed)
        {
            combo.combos[curComboExecuting].eventOnCombo[curEvent].TriggerEvent(controller.componentManager.entity);
            curEvent++;
            if (curEvent > combo.combos[curComboExecuting].eventOnCombo.Count - 1)
            combo.triggerConsumed = true;
            
        }
    }
}

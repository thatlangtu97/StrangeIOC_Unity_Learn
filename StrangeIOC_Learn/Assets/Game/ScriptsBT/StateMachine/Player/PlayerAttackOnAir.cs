using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.Examples;
using BehaviorDesigner.Runtime.Tasks;

[CreateAssetMenu(fileName = "Player Air Attack State", menuName = "State/Player/Air Attack")]
public class PlayerAttackOnAir : State
{
    public ComboDataConfig comboSet;
    [HideInInspector]
    public ComboDataConfig combo;
    public bool isProjectileCombo;
    //public bool isHoldingCombo;
    public bool isDeepAttack;
    public bool lateComboBuffer;
    bool isPlunging;
    //[SerializeField]
    int currCombo;
    bool island;
    private bool buffer;
    private bool isAttack;
    private bool isAnimationEnd;
    //private float holdAttackTime;
    private float duration;
    int curComboExecuting;
    int curEvent;
    public override void EnterState()
    {
        island = false;
        base.EnterState();
        CheckPlungingAttack();

        if (isDeepAttack)
            controller.eventOnAnimation.EarthStompFx = controller.shootEvent.airStompFx;
        duration = 0.3f;
        //holdAttackTime = combo.combos[0].holdDuration;
        if (controller.jumpAttackCharge == controller.maxJumpAttackCharge)
        {
            currCombo = 0;
            curComboExecuting = 0;
        }

        //if (controller.componentManager.entity.checkGround.isOnGround)//neu o tren khong thi danh tren khong
        //{

        //    controller.ChangeState(controller.attackState);
        //    return;
        //}

        if (combo.combos.Count != 1)
        {
            controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity = Vector2.zero;
        }

        if (controller.componentManager.entity.hasDash)
        {
            controller.componentManager.entity.RemoveDash();
        }

        controller.animator.SetBool(AnimationName.MOVE, false);
        controller.forceOnEvent.SetForceOnAirCombo(currCombo);
        if (isProjectileCombo)
        {
            controller.shootEvent.SetAirAngle();
            controller.shootEvent.SetNumberOfProjectile(currCombo, true);
        }
           
        StartEventOnCombo();
        combo.combos[currCombo].CastSkill(controller.componentManager.entity, null);
        curComboExecuting++;
        currCombo++;



        if (controller.jumpAttackCharge > 0)
        {
            controller.jumpAttackCharge--;
        }

        isAnimationEnd = false;
        isAttack = true;

    }

    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
        combo = Instantiate(comboSet);
        controller.airComboData = combo;
    }

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
       
        UpdateEventOncombo();
        if (duration >= 0)//platform handle
        {
            duration -= Time.deltaTime;
            return;
        }

        CheckPlungingAttack();


        if (isPlunging)
        {
            OnInputStopMove();
        }
        CheckLanding();

        if (controller.componentManager.entity.isAttackDone && !isPlunging)
        {
            NextEarlyCombo();
            //Debug.Log(buffer);
            //if (controller.characterDirection != null)
            //    controller.characterDirection.lockDirection = false;

            //isAttack = false;
            //controller.componentManager.entity.isAttackDone = false;
            //if (buffer == true)
            //{
            //    if (controller.componentManager.entity.checkGround.isOnGround)
            //    {
            //        controller.ChangeState(controller.attackState);
            //        //controller.animator.SetBool("IsAttackJump2", false);
            //        buffer = false;
            //        return;
            //    }
            //    else
            //    {
            //        if (currCombo <= combo.combos.Count - 1)
            //        {
            //            //controller.attackSoundController.PlaySoundCombo2(currCombo, 0);                     
            //            buffer = false;
            //            if (isProjectileCombo)
            //                controller.shootEvent.SetNumberOfProjectile(currCombo, true);
            //            controller.forceOnEvent.SetForceOnAirCombo(currCombo);
            //            Debug.Log("Cast: " + currCombo);
            //            combo.combos[currCombo].CastSkill(controller.componentManager.entity, null);
            //            StartEventOnCombo();
            //            //EffectSoundController.instance.hitClip = controller.attackSoundController.slash2[currCombo];
            //            curComboExecuting++;
            //            currCombo++;

            //            isAttack = true;

            //        }
            //        else
            //        {

            //            buffer = false;
            //            controller.ChangeState(controller.idleState);
            //        }
            //    }
            //}
            //else
            //{
            //    buffer = false;           
            //    //controller.ChangeState(controller.jumpFallState);             
            //}
            //return;
        }

       

        if (controller.componentManager.entity.checkGround.isOnGround && !isPlunging)
        {         
            if (buffer)
            {          
                controller.ChangeState(controller.attackState);
            }             
            else
            {
                controller.ChangeState(controller.jumpFallState);
            }

            return;
        }

        if (controller.componentManager.entity.isAttackDone && isPlunging)
        {
            HitStopController.instance.HitStop(2, 0.1f);
            controller.componentManager.entity.isAttackDone = false;
            //isPlunging = true;
        }


        if (controller.componentManager.entity.isAnimationEnd )
        {
            isAttack = false;
            isAnimationEnd = true;
            
            if (controller.componentManager.entity.checkGround.isOnGround)
            {
                currCombo = 0;
                controller.ChangeState(controller.idleState);              
            }
            else
            {
                if (buffer)
                    NextEarlyCombo();
                else                  
                controller.ChangeState(controller.jumpFallState);
            }
                
             
            return;
        }

    }

    public override void ExitState()
    {
        base.ExitState();
        isPlunging = false;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale = 3.5f;
        buffer = false;
        isAttack = false;
        controller.componentManager.entity.isAttackDone = false;
    }

    public override TaskStatus OnInputSkill(int skillId)
    {
        if (isPlunging)
            return TaskStatus.Success;
        //return TaskStatus.Failure;
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            //ExitState();
            OnPlayerSkill(skillId);
        }
        return TaskStatus.Success;
    }
    public override TaskStatus OnKnockDown()
    {
        base.OnKnockDown();
        return TaskStatus.Failure;
    }
    public override TaskStatus OnInputAttack()
    {
        //if (isHoldingCombo)
        //    buffer = true;
        if (isPlunging)
        {
            return TaskStatus.Success;
        }
            
        //if (isAttack)
        //{
            if (currCombo >= combo.combos.Count)
            {             
                return TaskStatus.Success;
            }
            buffer = true;
        //}

        return TaskStatus.Success;
    }
    public override TaskStatus OnInputMoveLeft()
    {
        base.OnInputMoveLeft();
        if (isPlunging)
            return TaskStatus.Success;
        if (controller.characterDirection.isFaceRight)
        {
            controller.characterDirection.ChangeDirection();
        }
        controller.componentManager.entity.moveByDirection.direction = Vector2.left;
        return TaskStatus.Success;

    }
    public override TaskStatus OnInputMoveRight()
    {
        base.OnInputMoveRight();
        if (isPlunging)
            return TaskStatus.Success;

        if (!controller.characterDirection.isFaceRight)
        {
            controller.characterDirection.ChangeDirection();
        }
        controller.componentManager.entity.moveByDirection.direction = Vector2.right;
        return TaskStatus.Success;
    }

    public override TaskStatus OnInputJump()
    {
        if (isPlunging)
            return TaskStatus.Success;

        if (!controller.componentManager.entity.checkGround.isOnGround && controller.doubleJumpCharge > 0)
        {
            //controller.componentManager.entity.jump.power = controller.componentManager.entity.jump.power / 2;
            controller.ChangeState(controller.doubleJumpState);
        }

        if (controller.componentManager.entity.checkGround.isOnGround)// for fast recovery after touching ground
        {
            controller.ChangeState(controller.jumpState);
        }
        return TaskStatus.Success;
    }

    public override TaskStatus OnInputDash()
    {
        if (isPlunging)
            return TaskStatus.Success;

        if (controller.rollCharge <= 0)
            return TaskStatus.Success;
        base.OnInputDash();
        return TaskStatus.Success;
    }

    public override TaskStatus OnInputStopMove()
    {
        base.OnInputStopMove();
        controller.componentManager.entity.moveByDirection.direction = Vector2.zero;
        return TaskStatus.Success;
    }

    void CheckLanding()
    {
        if (controller.componentManager.entity.checkGround.isOnGround /*&& currCombo > combo.combos.Count - 1*/)
        {
            //if(isHoldingCombo)
            //{
            //    if (currCombo > 1)
            //        return;
            //}
            controller.animator.SetTrigger(AnimationName.END_JUMP_ATTACK);
            //Debug.Log("Land");
            if (!island)
            {
                if (controller.movementSoundController != null)
                    controller.movementSoundController.PlaySound(AudioName.JumpEnd, false);
                island = true;
            }
        }
    }

    void NextEarlyCombo()
    {
        if (controller.characterDirection != null)
            controller.characterDirection.lockDirection = false;

        isAttack = false;
        controller.componentManager.entity.isAttackDone = false;
        if (buffer == true)
        {
            if (controller.componentManager.entity.checkGround.isOnGround)
            {
                Debug.Log("Attack");
                controller.ChangeState(controller.attackState);
                buffer = false;
                return;
            }
            else
            {
                if (currCombo <= combo.combos.Count - 1)
                {
                    //controller.attackSoundController.PlaySoundCombo2(currCombo, 0);                     
                    buffer = false;
                    if (isProjectileCombo)
                        controller.shootEvent.SetNumberOfProjectile(currCombo, true);
                    controller.forceOnEvent.SetForceOnAirCombo(currCombo);               
                    combo.combos[currCombo].CastSkill(controller.componentManager.entity, null);                  
                    if (controller.jumpAttackCharge >0)
                    controller.jumpAttackCharge--;
                    StartEventOnCombo();
                    //EffectSoundController.instance.hitClip = controller.attackSoundController.slash2[currCombo];
                    curComboExecuting++;
                    currCombo++;
                    isAnimationEnd = false;
                    isAttack = true;

                }
                else
                {

                    buffer = false;
                    controller.ChangeState(controller.idleState);
                }
            }
        }
        else
        {
            buffer = false;
            if(lateComboBuffer)
            controller.ChangeState(controller.jumpFallState);             
        }
        return;
    }

    void NextLateCombo()
    {
        if (controller.characterDirection != null)
            controller.characterDirection.lockDirection = false;

        isAttack = false;
        controller.componentManager.entity.isAttackDone = false;
        if (buffer == true)
        {
            if (controller.componentManager.entity.checkGround.isOnGround)
            {
                controller.ChangeState(controller.attackState);
                buffer = false;
                return;
            }
            else
            {
                if (currCombo <= combo.combos.Count - 1)
                {
                    //controller.attackSoundController.PlaySoundCombo2(currCombo, 0);                     
                    buffer = false;
                    if (isProjectileCombo)
                        controller.shootEvent.SetNumberOfProjectile(currCombo, true);
                    controller.forceOnEvent.SetForceOnAirCombo(currCombo);                  
                    combo.combos[currCombo].CastSkill(controller.componentManager.entity, null);
                    if (controller.jumpAttackCharge > 0)
                        controller.jumpAttackCharge--;
                    StartEventOnCombo();
                    //EffectSoundController.instance.hitClip = controller.attackSoundController.slash2[currCombo];
                    curComboExecuting++;
                    currCombo++;
                    isAnimationEnd = false;
                    isAttack = true;

                }
                else
                {

                    buffer = false;
                    controller.ChangeState(controller.idleState);
                }
            }
        }
        else
        {
            buffer = false;
            //controller.ChangeState(controller.jumpFallState);             
        }
        return;
    }

    void CheckPlungingAttack()
    {
        if (!isDeepAttack)
            return;

        if (currCombo > combo.combos.Count - 1 && combo.combos.Count != 1)
        {
            isPlunging = true;
        }
        else
            isPlunging = false;
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
        if (timeCounter.elapseTime >= combo.combos[curComboExecuting].eventOnCombo[curEvent].TriggerAfterTime && !combo.triggerConsumed)
        {
            combo.combos[curComboExecuting].eventOnCombo[curEvent].TriggerEvent(controller.componentManager.entity);
            curEvent++;
            if (curEvent > combo.combos[curComboExecuting].eventOnCombo.Count - 1)
                combo.triggerConsumed = true;

        }
    }
}
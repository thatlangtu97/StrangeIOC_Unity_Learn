
using UnityEngine;
using Spine.Unity.Examples;
using BehaviorDesigner.Runtime.Tasks;

public class PlayerAttackOnAirBase : State
{
    public ComboDataConfig comboSet;
    [HideInInspector]
    public ComboDataConfig combo;
    //public bool isProjectileCombo;
    public bool isHoldingCombo;
    //public bool isPlungingCombo;
    //public bool isAutoCombo;
    bool isPlunging;
    //[SerializeField]
    [HideInInspector]
    public int currCombo;
    bool releaseAttack;
    bool island;
    private bool buffer;
    private bool isAttack;
    private float holdAttackTime;
    public float minDuration;
    float duration;
    int curComboExecuting;
    int curEvent;
    public override void EnterState()
    {
        island = false;
        base.EnterState();
        //CheckPlungingAttack();

        duration = minDuration;
        holdAttackTime = combo.combos[0].holdDuration;
 
        if (controller.jumpAttackCharge == controller.maxJumpAttackCharge)
        {
            currCombo = 0;
        }

        if (controller.componentManager.entity.checkGround.isOnGround)
        {

            controller.ChangeState(controller.attackState);
            return;
        }

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
        //if (isProjectileCombo)
        //    controller.shootEvent.SetNumberOfProjectile(currCombo, true);

        StartEventOnCombo();
        combo.combos[currCombo].CastSkill(controller.componentManager.entity, null);
        currCombo++;

        if (controller.jumpAttackCharge > 0)
        {
            controller.jumpAttackCharge--;
        }
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
        CheckHoldingDownAttack();
        if (duration >= 0)
        {
            duration -= Time.deltaTime;
            return;
        }
        //CheckPlungingAttack();
        //if (isPlunging)
        //{
        //    OnInputStopMove();
        //}
        CheckLanding();

        //if (controller.componentManager.entity.isAttackDone /*&& !isPlunging*/)
        //{
        //    if (controller.characterDirection != null)
        //        controller.characterDirection.lockDirection = false;

        //    isAttack = false;
        //    controller.componentManager.entity.isAttackDone = false;
        //    if (buffer == true)
        //    {
        //        if (controller.componentManager.entity.checkGround.isOnGround)
        //        {
        //            controller.ChangeState(controller.attackState);                 
        //            buffer = false;
        //            return;
        //        }
        //        else
        //        {
        //            if (currCombo <= combo.combos.Count - 1)
        //            {

        //                buffer = false;
        //                if (isProjectileCombo)
        //                    controller.shootEvent.SetNumberOfProjectile(currCombo, true);
        //                controller.forceOnEvent.SetForceOnAirCombo(currCombo);

        //                StartEventOnCombo();
        //                combo.combos[currCombo].CastSkill(controller.componentManager.entity, null);
        //                curComboExecuting++;
        //                currCombo++;
        //                isAttack = true;
        //            }
        //            else
        //            {
        //                if(!isAutoCombo)
        //                {
        //                    buffer = false;
        //                    Debug.Log("Endd");
        //                    controller.ChangeState(controller.idleState);                         
        //                }                 
        //            }
        //        }
        //    }
        //    else
        //    {
        //        buffer = false;
        //        if(!isAutoCombo)
        //        {
        //            Debug.Log("F");
        //            controller.ChangeState(controller.jumpFallState);             
        //        }           
        //    }
        //    return;
        //}


        //if (controller.componentManager.entity.isAttackDone && isPlunging)
        //{
        //    HitStopController.instance.HitStop(2, 0.1f);
        //    controller.componentManager.entity.isAttackDone = false;
        //    isPlunging = true;
        //}

        if (controller.componentManager.entity.isAnimationEnd)
        {
            controller.componentManager.entity.moveByDirection.speedScale = 1;
            if (controller.componentManager.entity.checkGround.isOnGround)
            {              
                controller.ChangeState(controller.idleState);
                currCombo = 0;
            }
            else
            {             
                controller.ChangeState(controller.jumpFallState);
            }
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        releaseAttack = false;
        curComboExecuting = 0;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale = 3.5f;
        buffer = false;
        isAttack = false;
        controller.componentManager.entity.isAttackDone = false;
    }

    public override TaskStatus OnInputSkill(int skillId)
    {
        if (isPlunging)
            return TaskStatus.Success;
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            ExitState();
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
        if (isHoldingCombo)
            buffer = true;
        //if (isPlunging)
        //    return TaskStatus.Success;
        if (isAttack)
        {
            if (currCombo >= combo.combos.Count - 1)
                return TaskStatus.Success;
            buffer = true;
        }

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
            
            controller.ChangeState(controller.doubleJumpState);
        }

        if (controller.componentManager.entity.checkGround.isOnGround)
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
        ExitState();
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
        if (controller.componentManager.entity.checkGround.isOnGround)
        {
            //if (isHoldingCombo)
            //{
            if (currCombo > 1)
                return;
            //}
            controller.animator.SetTrigger(AnimationName.END_JUMP_ATTACK);          
            if (!island)
            {
                if (controller.movementSoundController != null)
                    controller.movementSoundController.PlaySound(AudioName.JumpEnd, false);
                island = true;
            }
        }
    }
    void CheckHoldingDownAttack()
    {
        if (isHoldingCombo)
        {
            if (holdAttackTime > 0)
            {
                if (InputManager.instance.isHoldAttack)
                {
                    holdAttackTime -= Time.deltaTime;
                }
                else
                {
                    holdAttackTime = 10;
                    buffer = false;
                    releaseAttack = true;
                }
            }
            else
            {
                if (currCombo <= combo.combos.Count - 1)
                {
                    buffer = false;              
                    combo.combos[currCombo].CastSkill(controller.componentManager.entity, null);
                    StartEventOnCombo();                  
                    curComboExecuting++;
                    currCombo++;
                    controller.componentManager.entity.isAttackDone = false;
                    duration = 0;
                }
            }

            if (buffer && releaseAttack)
            {
                if (currCombo <= combo.combos.Count - 1)
                {
                    buffer = false;
                    Debug.Log("Slam");
                    Debug.Log(currCombo);
                    combo.combos[currCombo].CastSkill(controller.componentManager.entity, null);
                    StartEventOnCombo();
                    curComboExecuting++;
                    currCombo++;
                    controller.componentManager.entity.isAttackDone = false;
                    duration = 0;
                    return;
                }
            }
        }
    }
    //void CheckPlungingAttack()
    //{
    //    if (!isPlungingCombo)
    //        return;
    //    if (currCombo > combo.combos.Count - 1 && combo.combos.Count != 1)
    //    {
    //        isPlunging = true;
    //    }
    //    else
    //        isPlunging = false;
    //}

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

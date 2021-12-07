using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player RangeAttack State", menuName = "State/Player/RangeAttack")]

public class PlayerRangeAttackState : SkillState
{ 
    public ComboDataConfig combo;
    public int skillIdToExecute = -1;
    public float floatingTime;
    //public float fireRate;


    bool singleShotAction;
    bool rapidShotAction;
    float floatingDuration;
    bool isFloating;
    int maxFloatingCharge;
    float startTime;
    bool buffer;
    int currCombo = 0; 
    bool isAttack;

    public override void EnterState()
    {

        base.EnterState();
        SetkShootAction();
        if (controller.curCombo == 0)
        {
            currCombo = controller.curCombo;
        }

        if (currCombo > combo.combos.Count - 1)
        {
            currCombo = 0;
            controller.curCombo = currCombo;          
        }

        if(singleShotAction)
        {
            currCombo = 0;
            controller.curCombo = 0;
        }       

        EffectSoundController.instance.hitClip = controller.attackSoundController.slash1[currCombo];
          
        startTime = Time.time;
        controller.attackSoundController.PlaySoundCombo1(currCombo, 0);
        controller.componentManager.entity.isStopMove = true;
        controller.animator.SetBool(AnimationName.MOVE, false);        
        skillIdToExecute = -1;
        if (buffer == false && isAttack == false)
        {
            //Debug.Log("curCombo: " + currCombo);
            controller.forceOnEvent.SetForceOnGroundCombo(currCombo);
            StopInMidAir();
            combo.combos[currCombo].CastSkill(controller.componentManager.entity, null);
            controller.buffer.SetTimerOnFirstAttackPress();
            currCombo++;
            controller.curCombo = currCombo;      
            isAttack = true;
        }


        if (!isAttack && controller.componentManager.entity.checkGround.isOnGround) // neu o tren khong ma dung ban
        {
            buffer = false;
            controller.ChangeState(controller.idleState);
            //controller.ChangeState(controller.jumpFallState);
        }
    }

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);

        if (singleShotAction)
        {
            currCombo = 0;
            controller.curCombo = 0;
        }

        if (floatingDuration >=0)
        {
            floatingDuration -= Time.deltaTime;
        }
        else if(floatingDuration < 0 )
        {
            controller.componentManager.entity.rigidbodyContainer.rigidbody.isKinematic = false;
            isFloating = false;
        }

        if (controller.curCombo == 0)
            currCombo = controller.curCombo;

        if (controller.componentManager.entity.isAttackDone && rapidShotAction)
        {
           
            if (controller.characterDirection != null)
                controller.characterDirection.lockDirection = false;
            isAttack = false;
            controller.componentManager.entity.isAttackDone = false;

            if (buffer == true)
            {
                if (currCombo >= combo.combos.Count - 1)//neu co buffer on combo 1 hoac 2 thi choi combo 2
                    currCombo = 1;
                
                if (currCombo <= combo.combos.Count - 1)
                {              
                    buffer = false;
                    //Debug.Log("curCombo: " + currCombo);
                    controller.forceOnEvent.SetForceOnGroundCombo(currCombo);
                    StopInMidAir();                 
                    combo.combos[currCombo].CastSkill(controller.componentManager.entity, null);
                    controller.buffer.SetTimerOnFirstAttackPress();
                    EffectSoundController.instance.hitClip = controller.attackSoundController.slash1[currCombo];
                    controller.attackSoundController.PlaySoundCombo1(currCombo, 0);
                    currCombo++;

                    controller.curCombo = currCombo;

                    // controller.animator.SetTrigger();
                    isAttack = true;

                }
                else
                {
                    // currCombo = 0;
                    //Debug.Log(" no combo"+Time.time );
                    buffer = false;
                    //currCombo = 0;
                    controller.ChangeState(controller.idleState);
                    //ExitState();
                }

            }
            else//no next attack
            {
                Debug.Log("BowRecover3");
                if(currCombo >=2)
                combo.combos[combo.combos.Count - 1].CastSkill(controller.componentManager.entity, null); // thu cung neu sau attack 2 ko co buffer            
                buffer = false;
               
            }
        }


        if (controller.componentManager.entity.isAnimationEnd) // animation end only on combo 3 hoac 1
        {
            buffer = false;
            controller.curCombo = 0;
            //controcurrCombo = 0;
            controller.ChangeState(controller.idleState);
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        //currCombo = 0;
        maxFloatingCharge = 3;
        buffer = false;
        isAttack = false;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.isKinematic = false;
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
        if (singleShotAction && isAttack)
        {
            
            return TaskStatus.Success;
        }
           

        if (isAttack)
        {
            buffer = true;
        }
        else
        {
            buffer = false;          
            EnterState();
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

    public override TaskStatus OnInputStopMove()
    {
        base.OnInputStopMove();
        controller.componentManager.entity.moveByDirection.direction = Vector2.zero;
        return TaskStatus.Success;
    }
    public override void OnForceExitState()
    {
        if (Time.time - startTime > 0.1f)
        {
            if (controller.componentManager.entity.checkGround.isOnGround)
            {
                controller.ChangeState(controller.idleState);
            }
            else
            {
                controller.ChangeState(controller.jumpFallState);
            }
            controller.componentManager.entity.isAttackComplete = false;
        }
    }

    void StopInMidAir()
    {
        if (isFloating)
            return;
        maxFloatingCharge--;
        if (maxFloatingCharge == 0)
        {
            controller.componentManager.entity.rigidbodyContainer.rigidbody.isKinematic = false;
            isFloating = false;
            return;
        }   
        isFloating = true;
        floatingDuration = floatingTime;
        startTime = Time.time;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity = Vector2.zero;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.isKinematic = true;
    }

    void SetkShootAction()
    {
        if (controller.componentManager.entity.attackPower.speed.Value > 1f)
        {
            //Rapid shot action
            rapidShotAction = true;
            singleShotAction = false;
        }
        else
        {
            rapidShotAction = false;
            singleShotAction = true;
        }    
    }
    
}

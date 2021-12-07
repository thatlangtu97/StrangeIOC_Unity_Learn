using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Dash State", menuName = "State/Player/Dash")]
public class PlayerDashState : State
{
    public GameObject dummy;
    public float addOnCoolDown;
    public bool hasDashAttack;
    public bool useDashEffect;

    //private float startTimeUsingAddon;
    private float minimumCancelTime;

    private int skillId = -1;
    private float curDashPower;
    private bool bufferAttack = false; //detect any action in transition phase
    private bool bufferDash = false;
    private bool isMove = false;
    private bool isAirRoll = false;
    private bool isRolling = false;
    public bool multiRoll = false;
    float transitonTime = 0.22f;
    float curTranstionTime;
    bool endDash;
    public override void EnterState()
    {
        base.EnterState();
        controller.suspendAnimationEvent = true;
        curTranstionTime = transitonTime;
        controller.componentManager.entity.postEvent(EventID.GAME_EVENT, new EventPassiveContainer(EventPassive.ON_DASH, null));
        minimumCancelTime = 0.2f;
        controller.jumpAttackCharge = controller.maxJumpAttackCharge;
        if(useDashEffect)
        {
            controller.addOn.dashShadow.SetActive(true);
           
        }
       
        //PlantAddOn();
        if (dummy != null)
            ObjectPool.Spawn(dummy, controller.centerPoint.position , dummy.transform.rotation);
        controller.componentManager.entity.isStopMove = true;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity = Vector2.zero;
        skillId = -1;
        bufferAttack = false;
        bufferDash = false;
        isRolling = true;
       
        if (controller.componentManager.entity.health.HP.Value < 0)
            return;
         

        controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale = controller.dashGravity;
        curDashPower = controller.dashPower;
      
        if (controller.componentManager.entity.checkGround.isOnGround )
        {
            //Debug.Log("RollOnGround");
            controller.animator.SetTrigger("Skill204");
            isAirRoll = false;
        }
        else
        {
            controller.rollCharge--;
            controller.forceOnEvent.SetForceUp();
            controller.animator.SetTrigger("RollOnAir");
            isAirRoll = true;
        }

        /*
        else if (!controller.componentManager.entity.checkGround.isOnGround && controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y >= 0f)
        {
            //Debug.Log("RollOnAir: " + controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y);
            controller.rollCharge--;
            controller.forceOnEvent.SetForceUp();
            controller.animator.SetTrigger("RollOnAir");
            isAirRoll = true;
        }
        else if (!controller.componentManager.entity.checkGround.isOnGround && controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y < 0f)
        {
            //Debug.Log("RollOnGround falling: " + controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y);
            controller.animator.SetTrigger("Skill204");
            controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale = 2.5f;
            isAirRoll = false;          
        }
        else if (controller.rollOnSkillAir)
        {
            //Debug.Log("RollOnAirSkill");
            controller.rollCharge--;
            controller.forceOnEvent.SetForceUp();
            controller.animator.SetTrigger("RollOnAir");
            isAirRoll = true;
            controller.rollOnSkillAir = false;
        }
        else
        {
            Debug.Log("Error");
        }*/

        //curDashPower = controller.dashPower;

        if (controller.componentManager.entity.hasDash)
        {
            controller.componentManager.entity.ReplaceDash(controller.dashDuration, controller.dashDuration, curDashPower, curDashPower);
        }
        else
        {
            controller.componentManager.entity.AddDash(controller.dashDuration, controller.dashDuration, curDashPower, curDashPower);
        }

        controller.bodyEffect.immune = true;
        if (controller.movementSoundController != null)
            controller.movementSoundController.PlaySound(AudioName.Dash, false);
       
    }

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        minimumCancelTime -= Time.deltaTime; //backward dash 
        //Debug.Log("PlayerDashState");
        if (controller.componentManager.entity.health.HP.Value <= 0)
        {
            controller.componentManager.entity.isSkillComplete = true;
            ExitState();
            return;
        }

        EndDashTransition();

        if (controller.componentManager.entity.isRollComplete && isAirRoll && isRolling)//platform
        {
            controller.bodyEffect.immune = false;
            isRolling = false;
            controller.ChangeState(controller.jumpFallState);
            return;
        }

        //handle ground roll
        if (controller.componentManager.entity.isRollComplete && !isAirRoll && isRolling)
        {       
            isRolling = false;
            controller.componentManager.entity.isStopMove = false;
            controller.bodyEffect.immune = false;
            OnFinishDash();
            return;
        }         
        

        if(controller.componentManager.entity.isAttackDone)//fix bug when clicking multiple button
        {
            controller.componentManager.entity.isAttackDone = false;
            controller.ChangeState(controller.idleState);
            controller.curCombo = 0;
        }      
    }


    void OnFinishDash()
    {
        //Debug.Log("OnFinishDash");
        if (bufferDash)
        {
            bufferDash = false;     
            base.OnInputDash();
            return;
        }
       
        if (isMove)
            controller.animator.SetTrigger(AnimationName.ROLL_TO_MOVE);
        else
            controller.animator.SetTrigger(AnimationName.ROLL_TO_IDLE);

        endDash = true;
        //ExitState();
        //controller.StartCoroutine(ChangeStateToMove());
        return;

    }

    public override void ExitState()
    {
        base.ExitState();
        endDash = false;
        controller.suspendAnimationEvent = false;
       
        if (useDashEffect)
        {
          
            controller.addOn.dashShadow.SetActive(false);
        } 
        controller.componentManager.entity.isStopMove = false;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale = 3.5f;
        bufferAttack = false;
        skillId = -1;
        controller.bodyEffect.immune = false;
        //controller.StopAllCoroutines();
        //controller.GetComponentInChildren<Detectable>().DisableAllImmune();
        if (controller.componentManager.entity.hasDash)
        {
            controller.componentManager.entity.RemoveDash();
        }
    }
    
    
    public override TaskStatus OnInputAttack()
    {
        base.OnInputAttack();
        bufferAttack = true;
        //Debug.Log("AttackOnRoll");
        skillId = -1;
        if (hasDashAttack && !isAirRoll)
        {
            controller.ChangeState(controller.dashAttack);
            return TaskStatus.Success;
        }


        controller.ChangeState(controller.attackState);
        return TaskStatus.Success;
    }

    public override TaskStatus OnInputMoveLeft()//cancel dash
    {
        isMove = true;
        
        if(minimumCancelTime >0)
        {
            if (controller.characterDirection.isFaceRight) 
            {
                controller.componentManager.entity.moveByDirection.direction = Vector2.left;
                controller.characterDirection.ChangeDirection();
            }
            return TaskStatus.Success;
        }
        
        if (controller.characterDirection.isFaceRight) // chi co dash ben phai ma input left moi cancel dash
        {
            controller.characterDirection.ChangeDirection();
            controller.componentManager.entity.moveByDirection.direction = Vector2.left;

            if (controller.componentManager.entity.checkGround.isOnGround)
            {                    
                if (isRolling)
                {
                    controller.ChangeState(controller.moveState);
                    //Debug.Log("Cancel");
                }             
            }
            else
            {              
                controller.ChangeState(controller.jumpFallState);
            }
        }
        else
            controller.componentManager.entity.moveByDirection.direction = Vector2.left;

        return TaskStatus.Success;
    }

    public override TaskStatus OnInputMoveRight()//cancel dash
    {
        isMove = true;
        
        if (minimumCancelTime > 0)
        {
            if (!controller.characterDirection.isFaceRight)
            {
                controller.componentManager.entity.moveByDirection.direction = Vector2.right;
                controller.characterDirection.ChangeDirection();
            }
            return TaskStatus.Success;
        }
        
        if (!controller.characterDirection.isFaceRight) // chi co dash ben trai ma input righ moi cancel dash
        {
            controller.characterDirection.ChangeDirection();
            controller.componentManager.entity.moveByDirection.direction = Vector2.right;
         
            if (controller.componentManager.entity.checkGround.isOnGround)
            {                 
                if (isRolling)
                {
                    controller.ChangeState(controller.moveState);
                    //Debug.Log("Cancel");
                }
            }
            else
            {           
                controller.ChangeState(controller.jumpFallState);
            }               
        }
        else
            controller.componentManager.entity.moveByDirection.direction = Vector2.right;


        return TaskStatus.Success;
    }
    
    public override TaskStatus OnInputSkill(int _skillId)
    {
        if (_skillId == 0)//roll cannt cancel roll
        {
            this.skillId = -1;
            return TaskStatus.Success;
        }
        //Debug.Log("CallSkill: " + _skillId);
        this.skillId = _skillId;
       
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            //Debug.Log("SkillOnRoll");          
            OnPlayerSkill(_skillId); 
        }
        return TaskStatus.Success;
    }
    
    public override TaskStatus OnInputJump()//canel dash
    {    
        if (controller.componentManager.entity.checkGround.isOnGround)
        {
            bufferAttack = true;
            //controller.componentManager.entity.isStopMove = false;
           
            controller.ChangeState(controller.jumpState);
            //if (controller.componentManager.entity.hasDash)
            //{
            //    controller.componentManager.entity.RemoveDash();
            //}
            return TaskStatus.Success;
        }

        if (!controller.componentManager.entity.checkGround.isOnGround && controller.doubleJumpCharge > 0)
        {
            bufferAttack = true;
            //controller.componentManager.entity.isStopMove = false;
            //Debug.Log("DoubleJumpOnRoll");
            controller.ChangeState(controller.doubleJumpState);
            return TaskStatus.Success;
        }
        return TaskStatus.Success;
    }
    
    public override TaskStatus OnInputDash()
    {
        //base.OnInputDash();   
        //Debug.Log(controller.rollCharge);
        if(multiRoll)
        {
            if (!controller.componentManager.entity.checkGround.isOnGround)
            {
                if (controller.rollCharge <= 0)
                {
                    Debug.Log("Out of roll");
                    return TaskStatus.Success;
                }
            }
            bufferDash = true;
            Debug.Log("MulTiRoll");
        }
        return TaskStatus.Success;
    }

    public override TaskStatus OnInputStopMove()
    {
        isMove = false;
        controller.componentManager.entity.isStopMove = true;
        controller.componentManager.entity.moveByDirection.direction = Vector2.zero;
        return TaskStatus.Success;
    }

    IEnumerator ChangeToIdleAfterEndFall() // handle on air
    {
        yield return new WaitForSeconds(0.3f);
        if (!bufferAttack && skillId == -1)
        {
            if (!isMove)
                controller.ChangeState(controller.idleState);
            else
                controller.ChangeState(controller.moveState);
        }                         
    }

    IEnumerator ChangeStateToMove() // recoveryonGround
    {
        yield return new WaitForSeconds(0.22f);

        if (!bufferAttack && skillId == -1)
        {

            if (isMove)
                controller.ChangeState(controller.moveState);
            else
                controller.ChangeState(controller.idleState);
        }
        else
        {
            controller.ChangeState(controller.idleState);//insurance
        }
    }

    void EndDashTransition()
    {
        if (!endDash)
            return;
               
        if (curTranstionTime >= 0)
        {
            curTranstionTime -= Time.deltaTime;
            return;
        }
       
        if (!bufferAttack && skillId == -1)
        {
            
            if (isMove)
                controller.ChangeState(controller.moveState);
            else
                controller.ChangeState(controller.idleState);
           
        }
        else
        {
           
            controller.ChangeState(controller.idleState);//insurance
        }
    }

    public override void OnDropDown()
    {
        controller.contactedPlatform = Physics2D.Raycast(controller.centerPoint.position, Vector2.down, 0.7f, controller.platFormCastMask);
        if (controller.contactedPlatform)
        {
            Physics2D.IgnoreCollision(controller.contactedPlatform.collider, controller.bodyCollider);
          
        }

    }

    //void PlantAddOn()
    //{
    //    if (!controller.addOn.bombDodge)
    //        return;

    //    if ((Time.time - startTimeUsingAddon) >= addOnCoolDown)
    //    {
    //        for (int i =0; i< controller.addOn.grenade.Count; i++)
    //        {
    //            controller.shootEvent.DropBomb(controller.addOn.grenade[i]);
    //        }          
    //        startTimeUsingAddon = Time.time;     
    //    }
    //}
}
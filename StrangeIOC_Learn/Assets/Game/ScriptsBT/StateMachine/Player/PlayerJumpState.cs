using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Jump State", menuName = "State/Player/Jump")]
public class PlayerJumpState : State
{
   
    //bool onAir;
    public override void EnterState()
    {
        base.EnterState();
        //controller.componentManager.entity.checkGround.isEnable = false;
        //onAir = false;
        //Debug.Log("JumpState");
        if (controller.attackSoundController != null)
        {
            controller.attackSoundController.StopSound();
        }

        //controller.doubleJumpCharge = HeroCreator.instance.jumpCharge;
        controller.doubleJumpCharge = 1;
        controller.jumpAttackCharge = controller.maxJumpAttackCharge;
        controller.rollCharge = 1;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale = 3.5f;
        controller.componentManager.entity.jump.power = controller.componentManager.entity.jump.originalPower;
        controller.componentManager.entity.isStopMove = false;
        //Debug.Log("Enter jump: ");
        controller.componentManager.entity.jump.isEnable = true;
        
        controller.animator.SetTrigger(AnimationName.JUMP);
        if(controller.movementSoundController != null)
        {
            controller.movementSoundController.PlaySound(AudioName.JumpStart, false);
        }
       
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        // controller.componentManager.entity.jump.power = true;
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            //Debug.Log("On air: " + onAir);
            /*
            if (!onAir)
            {
                if (!controller.componentManager.entity.checkGround.isOnGround && controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y > 0f)
                {
                    onAir = true;

                }
            }*/
            /*
            if (controller.componentManager.entity.checkGround.isOnGround && controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y >= 0f && onAir)
            {
                if (controller.componentManager.entity.health.HP.Value > 0)
                {
                    controller.ChangeState(controller.idleState);
                    return;
                }
            }
            /*else*/
            /*
            if(controller.componentManager.entity.checkGround.isOnGround && controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y != 0f)
            {
                controller.ChangeState(controller.jumpFallState);
                return;
            }
            
            /*
            if (controller.componentManager.entity.checkGround.isOnGround == false && controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y <= 0f )
            {
                controller.ChangeState(controller.idleState);               
                return;
            }         
            */

            controller.componentManager.entity.checkGround.isOnGround = false; // to pass through platform
            if (controller.componentManager.entity.checkGround.isOnGround == false && controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y < 0f)
            {
                if (controller.componentManager.entity.health.HP.Value > 0)
                {
                    controller.ChangeState(controller.jumpFallState);
                    //Debug.Log("change to jumpfall");
                    return;
                }
            }
        }
        else
        {
            controller.ChangeState(controller.dieState);
        }

    }

    
    public override void ExitState()
    {
        base.ExitState();
        //Debug.Log("CheckAgain");
        //controller.componentManager.entity.checkGround.isEnable = true;
    }
    /*
    public override TaskStatus OnInputDash()
    {
        base.OnInputDash();
        //Debug.Log("???D");
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            controller.componentManager.entity.dash.power = controller.componentManager.entity.dash.maxPower;
            controller.componentManager.entity.dash.duration = controller.componentManager.entity.dash.maxDuration;
            controller.ChangeState(controller.dashState);
        }
        return TaskStatus.Success;
    }*/
    public override TaskStatus OnInputJump()
    {
        //base.OnInputJump();
         controller.componentManager.entity.jump.isEnable = true;
        if (controller.componentManager.entity.health.HP.Value > 0)
        {             
            if (controller.doubleJumpCharge > 0)
                controller.ChangeState(controller.doubleJumpState);
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
            controller.componentManager.entity.moveByDirection.direction = Vector2.left;        
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
            controller.componentManager.entity.moveByDirection.direction = Vector2.right;   
        }
        return TaskStatus.Success;
    }
    public override TaskStatus OnInputAttack()
    {
        base.OnInputAttack();
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            if (controller.jumpAttackCharge > 0)
            {
                controller.ChangeState(controller.airAttackState);
            }
        }
        return TaskStatus.Success;
    }
    public override TaskStatus OnInputSkill(int skillID)
    {
        ////base.OnInputSkill(skillID);
        //controller.componentManager.entity.skillContainer.allSkill[skillID].CastSkill(controller.componentManager.entity);
        //controller.componentManager.entity.skillContainer.BehaviorTrees[skillID].EnableBehavior();
        //controller.componentManager.entity.skillContainer.allSkill[skillID].currCooldown = controller.componentManager.entity.skillContainer.allSkill[skillID].cooldown;
        //controller.ChangeState(controller.skillState);
        if(skillID == 0)
        {
            
        }
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            OnPlayerSkill(skillID);
        }
        return TaskStatus.Success;
    }

    public override TaskStatus OnInputDash()
    {
        if (controller.rollCharge <= 0)
            return TaskStatus.Success;
        base.OnInputDash();
        return TaskStatus.Success;
    }

}

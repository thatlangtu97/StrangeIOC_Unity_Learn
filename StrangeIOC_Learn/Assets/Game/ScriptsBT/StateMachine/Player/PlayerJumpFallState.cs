using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Player JumpFall State", menuName = "State/Player/JumpFall")]
public class PlayerJumpFallState : State
{
    bool isMove;
    bool isEnd;
    
    float time;
    public override void EnterState()
    {
        base.EnterState();   
        controller.componentManager.entity.rigidbodyContainer.rigidbody.drag = 1;
        controller.animator.SetTrigger(AnimationName.FALL);
        isEnd = false;
        isMove = false;
        controller.componentManager.entity.isStopMove = false;
        //controller.StartCoroutine(PassingThroughPlatForm(0.1f));
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);

        if (controller.componentManager.entity.checkGround.isOnGround == true && controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y >= 0f && !isEnd) //  o tren mat dat va het roi
        {
            controller.rollCharge = 1;
            isEnd = true;
           
            if(controller.buffer.JumpBuffered())
            {
                controller.componentManager.entity.moveByDirection.direction = Vector2.zero;
                controller.ChangeState(controller.jumpState);
                //Debug.Log("Jump early: " + (Time.time - InputManager.instance.buffer.jumpPressTime));
                return;
            }

            if (controller.componentManager.entity.health.HP.Value > 0)
            {
                if (!isMove)
                {
                    controller.animator.SetTrigger(AnimationName.END_FALL);       
                }
                else
                {
                    controller.animator.SetTrigger(AnimationName.END_FALL_MOVE);                 
                }
            }
            else
            {
                controller.ChangeState(controller.dieState);
            }
            if (controller.movementSoundController != null)
                controller.movementSoundController.PlaySound(AudioName.JumpEnd, false);
            controller.StartCoroutine(ChangeToIdleAfterEndFall());
            return;
        }
        //TODO check ground == false => chuyen sang fall state

    }
    IEnumerator ChangeToIdleAfterEndFall()
    {    
        if (!isMove)
            controller.componentManager.entity.isStopMove = true;
        yield return new WaitForSeconds(0.3f);
        if (isEnd)
        {
            if (controller.componentManager.entity.health.HP.Value > 0)
            {
                if (!isMove)
                {

                    controller.ChangeState(controller.idleState);
                }
                else
                    controller.ChangeState(controller.moveState);
            }
        }



    }
    public override void ExitState()
    {

        base.ExitState();     
        controller.componentManager.entity.rigidbodyContainer.rigidbody.drag = 0;
        //controller.StopAllCoroutines();
        isEnd = false;
    }

    IEnumerator PassingThroughPlatForm(float time)
    {
        yield return new WaitForSeconds(time);
        if(controller.contactedPlatform.collider !=null)
        Physics2D.IgnoreCollision(controller.contactedPlatform.collider, controller.bodyCollider, false);
        
    }

    public override TaskStatus OnInputDash()
    {
        if (controller.rollCharge <= 0)
            return TaskStatus.Success;
        //Debug.Log("RollOnFall");
        base.OnInputDash();
        return TaskStatus.Success;

    }
    public override TaskStatus OnInputJump()
    {
        base.OnInputJump(); // buffer
        if (!isEnd)
        {
            if (controller.doubleJumpCharge > 0 && !controller.componentManager.entity.checkGround.isOnGround)
            {
                controller.ChangeState(controller.doubleJumpState);
                //Debug.Log("Jump");
            }
           
            return TaskStatus.Success;
        }
        
        if (controller.componentManager.entity.checkGround.isOnGround)
        {
            controller.ChangeState(controller.jumpState);
            //Debug.Log("Jump");
            return TaskStatus.Success;
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
            var xForce = Mathf.Abs(controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.x) * Vector2.left.x;
            controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity = new Vector2(xForce,
                controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y);
            controller.componentManager.entity.moveByDirection.direction = Vector2.left;
            isMove = true;
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

            var xForce = Mathf.Abs(controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.x) * Vector2.right.x;
            controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity = new Vector2(xForce,
                controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y);
            controller.componentManager.entity.moveByDirection.direction = Vector2.right;
            isMove = true;
        }
        return TaskStatus.Success;
    }
    public override TaskStatus OnInputStopMove()
    {
        isMove = false;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity = new Vector2(0,
               controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y);
        controller.componentManager.entity.moveByDirection.direction = Vector2.zero;
        //controller.componentManager.entity.isStopMove = true;
        return TaskStatus.Success;
    }
    public override TaskStatus OnInputAttack()
    {
        if (!isEnd)
        {
            if (controller.jumpAttackCharge > 0)
            {
               controller.ChangeState(controller.airAttackState);
            }

            return TaskStatus.Success;
        }
        else
        {
            isEnd = false;
            if (controller.componentManager.entity.health.HP.Value > 0)
            {
                controller.ChangeState(controller.attackState);
            }
            return TaskStatus.Success;
        }


    }
    public override TaskStatus OnInputSkill(int skillID)
    {
        // //base.OnInputSkill(skillID);
        //controller.componentManager.entity.skillContainer.allSkill[skillID].CastSkill(controller.componentManager.entity);
        //controller.componentManager.entity.skillContainer.BehaviorTrees[skillID].EnableBehavior();
        //controller.componentManager.entity.skillContainer.allSkill[skillID].currCooldown = controller.componentManager.entity.skillContainer.allSkill[skillID].cooldown;
     
        //Debug.Log("RollOnJumpFall");
        //controller.ChangeState(controller.skillState);
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            OnPlayerSkill(skillID);
        }
        return TaskStatus.Success;
    }

    
}

using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Double Jump Fall State", menuName = "State/Player/Double Jump Fall")]
public class PlayerDoubleJumpFallState : State
{
    bool isMove;
    bool isEnd;
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(AnimationName.FALL);
        isEnd = false;
        isMove = false;
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        if (controller.componentManager.entity.checkGround.isOnGround == true && controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y >= 0f &&!isEnd) //  o tren mat dat va het roi
        {
         
            isEnd = true;
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
        if(isEnd)
        {
            if (controller.componentManager.entity.health.HP.Value > 0)
            {
                if (!isMove)
                    controller.ChangeState(controller.idleState);
                else
                    controller.ChangeState(controller.moveState);
            }
        }
     
    }
    public override void ExitState()
    {
        base.ExitState();
        isEnd = false;
    }
    
    public override TaskStatus OnInputDash()
    {
        base.OnInputDash();
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            controller.componentManager.entity.dash.power = controller.componentManager.entity.dash.maxPower;
            controller.componentManager.entity.dash.duration = controller.componentManager.entity.dash.maxDuration;
            controller.ChangeState(controller.dashState);
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
        return TaskStatus.Success;
    }


    public override TaskStatus OnInputAttack()
    {
        if (!isEnd)
        {
            if (controller.componentManager.entity.health.HP.Value > 0)
            {
                if (controller.jumpAttackCharge > 0)
                {
                    controller.ChangeState(controller.airAttackState);
                }
            }
           // controller.ChangeState(controller.airAttackState);
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
        ////base.OnInputSkill(skillID);
        //controller.componentManager.entity.skillContainer.allSkill[skillID].CastSkill(controller.componentManager.entity);
        //controller.componentManager.entity.skillContainer.BehaviorTrees[skillID].EnableBehavior();
        //controller.ChangeState(controller.skillState);
        //controller.componentManager.entity.skillContainer.allSkill[skillID].currCooldown = controller.componentManager.entity.skillContainer.allSkill[skillID].cooldown;
        OnPlayerSkill(skillID);
        return TaskStatus.Success;
    }
}


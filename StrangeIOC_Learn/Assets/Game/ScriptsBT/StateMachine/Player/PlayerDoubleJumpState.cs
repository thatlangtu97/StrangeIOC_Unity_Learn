using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Double Jump State", menuName = "State/Player/Double Jump")]
public class PlayerDoubleJumpState : State
{

    public float minWaitTime = 0.1f; // vi system thuc hien jump cham 1 frame nen onupdatestate se cho ra ket qua sai
    public override void EnterState()
    {
        base.EnterState();
        
        controller.doubleJumpCharge --;
        //controller.forceOnEvent.disable = true;
        controller.jumpAttackCharge = controller.maxJumpAttackCharge;
        controller.componentManager.entity.isStopMove = false;
        controller.componentManager.entity.jump.isEnable = true;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale = 3.5f;
        controller.animator.SetTrigger(AnimationName.DOUBLE_JUMP);
        //Debug.Log("Double jump state");
        minWaitTime = 0.5f;
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);

        
        if (minWaitTime >= 0f)
        {
            minWaitTime -= Time.deltaTime;
            return;
        }

        if (controller.componentManager.entity.checkGround.isOnGround)
        {
            if (controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y == 0f)
            {
               
                controller.ChangeState(controller.idleState);
                return;
            }


        }
        else if (controller.componentManager.entity.checkGround.isOnGround == false)
        {
            if (controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y < 0f)
            {
                //controller.doubleJumpCharge = 1;
                controller.ChangeState(controller.jumpFallState); //double jumpfall state is useless???
                return;
            }

        }


        //TO DO check ground && move.y >=0f chuyen ve idle state
        //else ground = false &&move.y<0f => JumpFall

    }
    public override void ExitState()
    {
        base.ExitState();
    }

    public override TaskStatus OnInputDash()
    {
        if (controller.rollCharge <= 0)
        {
            //Debug.Log("OutOfRoll");
            return TaskStatus.Success;
        }
        //Debug.Log("RollOnDoubleJump");
        base.OnInputDash();
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
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            if (controller.jumpAttackCharge > 0)
            {
                // controller.ChangeState(controller.airAttackState);
                base.OnInputAttack();
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
        //Debug.Log("RollOnDoubleJump");
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            OnPlayerSkill(skillID);
        }
        return TaskStatus.Success;
    }

    public override TaskStatus OnInputJump()
    {
        return TaskStatus.Success;
    }
}

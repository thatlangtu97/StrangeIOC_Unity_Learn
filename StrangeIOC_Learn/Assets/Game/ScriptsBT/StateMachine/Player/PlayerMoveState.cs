using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Move State", menuName = "State/Player/Move")]
public class PlayerMoveState : MoveState
{
    public override void EnterState()
    {
        base.EnterState();
     
        controller.componentManager.entity.isStopMove = false;
        controller.doubleJumpCharge = 1;
        if (controller.characterDirection != null)
            controller.characterDirection.lockDirection = false;

        controller.animator.SetBool(AnimationName.MOVE, true);
        
        if (controller.movementSoundController != null)
            controller.movementSoundController.PlaySound(AudioName.Move, false);
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        
        if(controller.componentManager.entity.moveByDirection.direction.x > 0 && !controller.characterDirection.isFaceRight)
        {
            controller.characterDirection.ChangeDirection();
        }

        if (controller.componentManager.entity.moveByDirection.direction.x < 0  && controller.characterDirection.isFaceRight)
        {
            controller.characterDirection.ChangeDirection();
        }

        if (controller.componentManager.entity.checkGround.isOnGround == false && controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y < 0f ) // k o tren mat dat va dang roi
        {
            if (controller.componentManager.entity.health.HP.Value > 0)
            {
                controller.ChangeState(controller.jumpFallState);
                return;
            }
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        controller.animator.SetBool(AnimationName.MOVE, false);
        if (controller.movementSoundController != null)
        {
            controller.movementSoundController.StopSound();
            //Debug.Log("stop");
        }
            
    }

    public override TaskStatus OnInputDash()
    {
        base.OnInputDash();
        return TaskStatus.Success;
    }
    public override TaskStatus OnInputJump()
    {
        base.OnInputJump();
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            controller.ChangeState(controller.jumpState);
        }
        return TaskStatus.Success;
    }
    public override TaskStatus OnInputMoveLeft()
    {
        base.OnInputMoveLeft();
        if (controller.characterDirection.isFaceRight)
        {
            if (controller.componentManager.entity.health.HP.Value > 0)
            {
                controller.characterDirection.ChangeDirection();
                controller.componentManager.entity.moveByDirection.direction = Vector2.left;
            }
        }
        return TaskStatus.Success;

    }
    public override TaskStatus OnInputMoveRight()
    {
        base.OnInputMoveRight();
        if (!controller.characterDirection.isFaceRight)
        {
            if (controller.componentManager.entity.health.HP.Value > 0)
            {
                controller.characterDirection.ChangeDirection();
                controller.componentManager.entity.moveByDirection.direction = Vector2.right;
            }
        }
        return TaskStatus.Success;
       
    }
    public override TaskStatus OnInputStopMove()
    {
        base.OnInputStopMove();
        controller.componentManager.entity.isStopMove = true;
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            controller.ChangeState(controller.idleState);
        }
        return TaskStatus.Success;
    }
    public override TaskStatus OnInputSkill(int skillID)
    {      
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            OnPlayerSkill(skillID);
        }
        return TaskStatus.Success;
    }

    public override TaskStatus OnInputAttack()
    {
        base.OnInputAttack();
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            controller.ChangeState(controller.attackState);
        }
        return TaskStatus.Success;
    }

    public override void OnDropDown()
    {
        controller.contactedPlatform = Physics2D.Raycast(controller.centerPoint.position, Vector2.down, 0.7f, controller.platFormCastMask);
        if (controller.contactedPlatform)
        {
            Physics2D.IgnoreCollision(controller.contactedPlatform.collider, controller.bodyCollider);
        }
    }
}

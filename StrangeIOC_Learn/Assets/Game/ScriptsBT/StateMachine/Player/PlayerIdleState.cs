using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Idle State", menuName = "State/Player/Idle")]
public class PlayerIdleState : IdleState
{    
    public override void EnterState()
    {
        base.EnterState();
        controller.doubleJumpCharge = 1;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.isKinematic = false;
        controller.componentManager.entity.isStopMove = true;
        controller.componentManager.entity.moveByDirection.direction = Vector2.zero;
        controller.animator.SetTrigger(AnimationName.IDLE);
        controller.movementSoundController.StopSound();
    }
    public override void UpdateState(float deltaTime)
    {
        
        base.UpdateState(deltaTime);
      //  Debug.Log(controller.componentManager.entity);
        if (controller.componentManager.entity.checkGround.isOnGround == false && controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y < 0f) // k o tren mat dat va dang roi
        {
            controller.ChangeState(controller.jumpFallState);
            return;
        }
        //TODO check ground == false => chuyen sang fall state

    }
    public override void ExitState()
    {
        base.ExitState();       
    }

    public override TaskStatus OnInputDash()
    {
        base.OnInputDash();
        return TaskStatus.Success;
    }

    public override TaskStatus OnInputJump()
    {
        //base.OnInputJump();
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            if (controller.componentManager.entity.checkGround.isOnGround)
            {
                controller.ChangeState(controller.jumpState);
            }
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
            controller.ChangeState(controller.moveState);
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
            controller.ChangeState(controller.moveState);
            controller.componentManager.entity.moveByDirection.direction = Vector2.right;
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
        if(controller.contactedPlatform)
        {
            Physics2D.IgnoreCollision(controller.contactedPlatform.collider, controller.bodyCollider);
        }
    }

}

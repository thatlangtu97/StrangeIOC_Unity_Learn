using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Dash Fall State", menuName = "State/Player/DashFall")]
public class PlayerDashFallState : State
{
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(AnimationName.FALL);

    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        if (controller.componentManager.entity.checkGround.isOnGround == true && controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y >= 0f) //  o tren mat dat va het roi
        {
            controller.animator.SetTrigger(AnimationName.END_FALL);
            controller.ChangeState(controller.idleState);
            return;
        }
        //TODO check ground == false => chuyen sang fall state

    }
    public override void ExitState()
    {
        base.ExitState();
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
    public override TaskStatus OnInputStopMove()
    {
        base.OnInputStopMove();
        controller.componentManager.entity.isStopMove = true;
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
}

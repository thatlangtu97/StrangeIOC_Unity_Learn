using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Jump State", menuName = "State/Enemy/EnemyJumpState")]
public class EnemyJumpState : State
{
    public float onAirDuration;
    bool isFalling;
    bool isLanding;
    bool jump;
    float waitBeforeJumpTime;
    RaycastHit2D hit;
    float curTimeLanding;
    float waitLanding = 0.5f;

    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }

    public override void EnterState()
    {
        base.EnterState();

        waitBeforeJumpTime = controller.enemyJumpConfig.waitForAnim;
        curTimeLanding = waitLanding;
        controller.animator.SetTrigger(AnimationName.JUMP);
        isFalling = false;
        isLanding = false;
        jump = false;
        //if(controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y <= 0)
    }

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        waitBeforeJumpTime -= Time.deltaTime;
        /*
        if (controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale ==0)
        {
            return;
        }
        */
        if (waitBeforeJumpTime <= 0 && !jump && !isFalling)
        {
            controller.componentManager.entity.jump.power = controller.enemyJumpConfig.jumpForce;
            controller.componentManager.entity.jump.isEnable = true;
            jump = true;
            if (controller.componentManager.entity.hasDash)
            {

                controller.componentManager.entity.ReplaceDash(controller.enemyJumpConfig.moveDuration, controller.enemyJumpConfig.moveDuration, controller.enemyJumpConfig.movePower, controller.enemyJumpConfig.movePower);
            }
            else
            {
                controller.componentManager.entity.AddDash(controller.enemyJumpConfig.moveDuration, controller.enemyJumpConfig.moveDuration, controller.enemyJumpConfig.movePower, controller.enemyJumpConfig.movePower);
            }
        }


        if (controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y <= -5 && !isFalling)
        {
            //controller.animator.SetTrigger(AnimationName.FALL);
            //isFalling = true;
            controller.ChangeState(controller.jumpFallState);
            return;
        }

        /*
        if(isFalling)
        {
            if(controller.componentManager.entity.checkGround.isOnGround)
            {
                Debug.Log("Land");
                controller.animator.SetTrigger(AnimationName.END_FALL);
                isLanding = true;
                isFalling = false;
            }
        }

        if(isLanding)
        {
            curTimeLanding -= Time.deltaTime;
            if(curTimeLanding <=0)
            {
                controller.ChangeState(controller.idleState);
            }
        }
        */
    }

    public override TaskStatus OnInputIdle()
    {
        return TaskStatus.Failure;
    }

    public override TaskStatus OnInputChangeFacing()
    {
        return TaskStatus.Failure;
    }
    public override void ExitState()
    {
        base.ExitState();
        isFalling = false;
        isLanding = false;
    }

    public override TaskStatus OnInputSkill(int skillId)
    {
        return base.OnInputSkill(skillId);
    }
}

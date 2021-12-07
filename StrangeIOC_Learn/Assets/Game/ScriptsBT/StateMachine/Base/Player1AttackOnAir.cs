using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[CreateAssetMenu(fileName = "Player1 Air Attack State", menuName = "State/Player/Player1 Air Attack")]
public class Player1AttackOnAir : PlayerAttackOnAirBase
{
    public float rate;
    public float speedScale;
    public float startGravity;
    bool endCombo;
    //public float delayInput;
    float startTime;
    public float recoveryTime;
    float originalSpeedScale;
    public override void EnterState()
    {
        startTime = 0;
        startTime = Time.time;
        endCombo = false;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale = startGravity;
        originalSpeedScale = controller.componentManager.entity.moveByDirection.speedScale;
        if (speedScale != 0)
            controller.componentManager.entity.moveByDirection.speedScale = speedScale;    
        base.EnterState();
    }

    public override void UpdateState(float deltaTime)
    {
        IncreaseGravityOverTime();

        if(controller.componentManager.entity.isAttackDone && currCombo >= combo.combos.Count-1)
        {
            startTime = Time.time;
            controller.componentManager.entity.moveByDirection.speedScale = 1;
            //controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity = new Vector2(0, controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y);
            controller.componentManager.entity.isStopMove = true;
            endCombo = true;
            controller.componentManager.entity.isAttackDone = false;
            if (controller.componentManager.entity.checkGround.isOnGround && !endCombo)
            {

                controller.componentManager.entity.moveByDirection.speedScale = 1;
                endCombo = true;
            }
        }

       

        base.UpdateState(deltaTime);

    }

    public override void ExitState()
    {
        base.ExitState();
        controller.componentManager.entity.moveByDirection.speedScale = originalSpeedScale;
        endCombo = false;
    }

    public override TaskStatus OnInputMoveLeft()
    {
        if (endCombo)
            return TaskStatus.Success;
        return base.OnInputMoveLeft();
    }

    public override TaskStatus OnInputMoveRight()
    {
        if (endCombo)
            return TaskStatus.Success;
        return base.OnInputMoveRight();
    }

    void IncreaseGravityOverTime()
    {
        if (speedScale == 0)
            return;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale += Time.deltaTime * rate;
    }

    public override TaskStatus OnInputAttack()
    {
        //if (Time.time - startTime < delayInput)
        //{
        //    return TaskStatus.Success;
        //}
        if (endCombo && Time.time - startTime > recoveryTime)
        {
            controller.ChangeState(controller.attackState);
            return TaskStatus.Success;
        }

        return base.OnInputAttack();
    }
}

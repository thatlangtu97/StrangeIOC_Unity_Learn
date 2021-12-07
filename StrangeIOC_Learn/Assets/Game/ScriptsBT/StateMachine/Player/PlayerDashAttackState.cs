using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[CreateAssetMenu(fileName = "Player Dash Attack State", menuName = "State/Player/DashAttack")]
public class PlayerDashAttackState : State
{
    public ComboDataConfig comboSet;
    protected ComboDataConfig dashComboAttack;
    public float delayInput;
    public bool notMove;
    public bool isDiveAttack;
    float startTime;
    int curComboExecuting;
    int curEvent;
    bool bufferAttack;
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
        dashComboAttack = Instantiate(comboSet);
        controller.dashComboData = dashComboAttack;
    }
    public override void EnterState()
    {
        bufferAttack = false;
        startTime = 0;
        startTime = Time.time;
        if(!notMove)
        {
            if (controller.componentManager.entity.hasDash)
            {
                controller.componentManager.entity.ReplaceDash(controller.dashDuration, controller.dashDuration, controller.dashPower, controller.dashPower);
            }
            else
            {
                controller.componentManager.entity.AddDash(controller.dashDuration, controller.dashDuration, controller.dashPower, controller.dashPower);
            }
        }
        if(isDiveAttack)
        controller.eventOnAnimation.EarthStompFx = controller.shootEvent.dashAttackFx;
        StartEventOnCombo();
        dashComboAttack.combos[0].CastSkill(controller.componentManager.entity, null);
    }

    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        UpdateEventOncombo();
        if(controller.componentManager.entity.isAnimationEnd && !bufferAttack)
        {         
            controller.ChangeState(controller.idleState);
        }
    }

    public override void ExitState()
    {
        startTime = 0;
        curComboExecuting = 0;
        if (controller.componentManager.entity.hasDash)
            controller.componentManager.entity.RemoveDash();
    }

    public override TaskStatus OnInputDash()
    {
        base.OnInputDash();
        
        return TaskStatus.Success;
    }

    public override TaskStatus OnInputAttack()
    {
        if (Time.time - startTime < delayInput && !controller.componentManager.entity.isAnimationEnd)
        {
            return TaskStatus.Success;
        }
        bufferAttack = true;
        
        controller.ChangeState(controller.attackState);
        return TaskStatus.Failure;
    }

    public override TaskStatus OnInputJump()
    {
       
        controller.ChangeState(controller.jumpState);
        return TaskStatus.Failure;
    }

    

    public override TaskStatus OnInputMoveLeft()
    {
        if (Time.time - startTime < delayInput )
        {
            return TaskStatus.Success;
        }
        else if(!controller.characterDirection.isFaceRight)
            return TaskStatus.Success;

        controller.componentManager.entity.moveByDirection.direction = Vector2.left;
        controller.ChangeState(controller.moveState);
        return TaskStatus.Success;
    }

    public override TaskStatus OnInputMoveRight()
    {
        if (Time.time - startTime < delayInput)
        {
            return TaskStatus.Success;
        }
        else if (controller.characterDirection.isFaceRight)
            return TaskStatus.Success;

        controller.componentManager.entity.moveByDirection.direction = Vector2.right;
        controller.ChangeState(controller.moveState);
        return TaskStatus.Success;
    }

    public override TaskStatus OnInputSkill(int skillId)
    {
        if (Time.time - startTime < delayInput)
        {
            return TaskStatus.Success;
        }
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            OnPlayerSkill(skillId);
        }
        return TaskStatus.Success;
    }

    void StartEventOnCombo()
    {
        dashComboAttack.triggerConsumed = false;
        timeCounter.StartCount();
    }

    void UpdateEventOncombo()
    {
        timeCounter.UpdateTimeCount(Time.deltaTime);
        if (curComboExecuting > dashComboAttack.combos.Count - 1)
            curComboExecuting = dashComboAttack.combos.Count - 1;
        if (curEvent > dashComboAttack.combos[curComboExecuting].eventOnCombo.Count - 1)
        {
            curEvent = 0;
            return;
        }

        if (dashComboAttack.combos[curComboExecuting].eventOnCombo.Count == 0)
            return;
        if (timeCounter.elapseTime >= dashComboAttack.combos[curComboExecuting].eventOnCombo[curEvent].TriggerAfterTime && !dashComboAttack.triggerConsumed)
        {
            dashComboAttack.combos[curComboExecuting].eventOnCombo[curEvent].TriggerEvent(controller.componentManager.entity);
            curEvent++;
            if (curEvent > dashComboAttack.combos[curComboExecuting].eventOnCombo.Count - 1)
                dashComboAttack.triggerConsumed = true;

        }
    }
}

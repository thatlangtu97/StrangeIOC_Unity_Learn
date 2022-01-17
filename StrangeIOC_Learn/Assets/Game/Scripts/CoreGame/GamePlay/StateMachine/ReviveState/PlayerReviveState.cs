using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerReviveState", menuName = "State/Player/PlayerReviveState")]
public class PlayerReviveState : State
{
    float countTime;

    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
        countTime = 0;
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (countTime < eventCollectionData[idState].durationAnimation)
        {
            countTime += Time.deltaTime;
        }
        else
        {
            controller.componentManager.properties.Heal = 100;
            if (controller.componentManager.checkGround() == true)
            {
                if (controller.componentManager.speedMove != 0)
                {
                    controller.ChangeState(NameState.MoveState,true);
                }
                else
                {
                    controller.ChangeState(NameState.IdleState,true);
                }
            }
        }
    }
    
}

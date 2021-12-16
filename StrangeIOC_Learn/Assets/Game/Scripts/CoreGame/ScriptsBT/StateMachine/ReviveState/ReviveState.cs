using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ReviveState", menuName = "State/ReviveState")]
public class ReviveState : State
{
    public float duration = 1f;
    float countTime;

    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(AnimationTriger.REVIVE);
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
        countTime = duration;
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (countTime >= 0)
        {
            countTime -= Time.deltaTime;
        }
        else
        {
            controller.componentManager.properties.Heal = 100;
            if (controller.componentManager.checkGround() == true)
            {
                if (controller.componentManager.speedMove != 0)
                {
                    controller.ChangeState(controller.moveState,true);
                }
                else
                {
                    controller.ChangeState(controller.idleState,true);
                }
            }
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DashState", menuName = "State/DashState")]
public class DashState : State
{
    public float duration = 0.4f;
    public float speedDash = 10f;
    float coutTimeDash = 0;
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(AnimationTriger.DASH);
        coutTimeDash = duration;
        
    }
    public override void UpdateState()
    {
        base.UpdateState();
        
        if (coutTimeDash >= 0)
        {
                controller.transform.position += new Vector3(controller.transform.localScale.x, 0f, 0f) * Time.deltaTime * speedDash;
                coutTimeDash -= Time.deltaTime;
        }
        else
        {
            if (controller.componentManager.speedMove != 0)
            {
                controller.ChangeState(controller.moveState);
            }
            else
            {
                controller.ChangeState(controller.idleState);
            }
        }
        
    }
    public override void ExitState()
    {
        base.ExitState();
        coutTimeDash = -1;
    }
}

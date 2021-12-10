using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DashState", menuName = "State/DashState")]
public class DashState : State
{
    public float duration = 0.4f;
    public float speedDash = 10f;
    float coutTime = 0;
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(AnimationTriger.DASH);
        coutTime = duration;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (coutTime < 0)
        {
            controller.ChangeState(controller.idleState);
        }
        else
        {
            if (coutTime > 0)
                controller.transform.position += new Vector3(controller.transform.localScale.x, 0f, 0f) * Time.deltaTime * speedDash;
        }
        coutTime -= Time.deltaTime;
    }
    public override void ExitState()
    {
        base.ExitState();
        coutTime = 0;
    }
}

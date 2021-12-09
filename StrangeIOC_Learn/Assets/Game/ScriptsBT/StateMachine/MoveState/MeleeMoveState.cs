using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MeleeMoveState", menuName = "State/MeleeMoveState")]
public class MeleeMoveState : State
{
    public float speed;
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(AnimationTriger.MOVE);
    }
    public override void UpdateState()
    {
        base.UpdateState();
        controller.transform.position += new Vector3(controller.transform.localScale.x, 0f, 0f) * Time.deltaTime * controller.componentManager.speedMove * controller.componentManager.timeScale;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MeleeMoveState", menuName = "State/MeleeMoveState")]
public class MeleeMoveState : State
{
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void EnterState()
    {
        base.EnterState();

        //controller.animator.SetBool(AnimationName.MOVE, true);
        //controller.animator.SetTrigger(AnimationName.MOVE);
        controller.animator.Play(AnimationName.MOVE);
    }
}

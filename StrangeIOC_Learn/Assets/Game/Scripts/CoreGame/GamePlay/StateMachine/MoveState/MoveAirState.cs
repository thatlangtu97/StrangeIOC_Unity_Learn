using System.Collections;
using System.Collections.Generic;
using DesperateDevs.Logging;
using UnityEngine;
[CreateAssetMenu(fileName = "MoveAirState", menuName = "CoreGame/State/MoveAirState")]
public class MoveAirState : State
{

    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        controller.componentManager.Rotate();
        controller.componentManager.rgbody2D.velocity = controller.componentManager.vectorSpeed.normalized* Mathf.Abs(controller.componentManager.speedMove) ;
        if (controller.componentManager.speedMove == 0f)
        {
            controller.ChangeState(NameState.IdleState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        controller.ChangeState(NameState.AttackState);
    }
    public override void OnHit()
    {
        base.OnHit();
        controller.ChangeState(NameState.HitState);
    }
    public override void OnInputSkill(int idSkill)
    {
        base.OnInputSkill(idSkill);
        controller.ChangeState(NameState.SkillState);
    }
}

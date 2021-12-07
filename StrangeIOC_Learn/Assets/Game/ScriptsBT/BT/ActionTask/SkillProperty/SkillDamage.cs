using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class SkillDamage : BaseSkill
{
    public AttackInfo info;


    public override TaskStatus OnUpdate()
    {
        CleanUpBufferManager.instance.AddReactiveComponent(() => { entity.AddAttackStart(info); }, () => { entity.RemoveAttackStart(); });
        return TaskStatus.Success;
    }
}

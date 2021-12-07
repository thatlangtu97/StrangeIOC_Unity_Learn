using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSkillStateTask : Action
{
    public SharedComponentManager component;
    public override void OnStart()
    {
        CleanUpBufferManager.instance.AddReactiveComponent(() => { if (component.Value.entity.isEnabled) { component.Value.entity.isAttackComplete = true; component.Value.entity.isSkillComplete = true; } }, () => { if (component.Value.entity.isEnabled) { component.Value.entity.isAttackComplete = false; component.Value.entity.isSkillComplete = false; } });
    }
}

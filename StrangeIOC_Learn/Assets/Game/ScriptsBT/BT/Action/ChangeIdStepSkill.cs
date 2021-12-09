using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class ChangeIdStepSkill : Action
{
    public SharedComponentManager componentManager;
    public int idStepSkill;

    public override void OnStart()
    {
        base.OnStart();
        componentManager.Value.nextIdSkill = idStepSkill;

    }
}

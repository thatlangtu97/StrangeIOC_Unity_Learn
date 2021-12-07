using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmuneHit : Action
{
    public SharedComponentManager component;
    public bool isImmune;
    public override void OnStart()
    {
        base.OnStart();
        component.Value.entity.stateMachineContainer.stateMachine.immuneHit = isImmune;
    }
}
public class ImmuneKnock : Action
{
    public SharedComponentManager component;
    public bool isImmune;
    public override void OnStart()
    {
        base.OnStart();
        component.Value.entity.stateMachineContainer.stateMachine.immuneKnock = isImmune;
        component.Value.GetComponentInChildren<Detectable>().immuneToForce = isImmune;
    }
}
public class CanRotateOnSkill : Action
{
    public SharedComponentManager component;
    public bool canRotate;
    public override void OnStart()
    {
        base.OnStart();
        component.Value.entity.stateMachineContainer.stateMachine.isCanRotate = canRotate;
    }
}
public class ChangeBlock : Action
{
    public SharedComponentManager component;
    public int chance;
    public override void OnStart()
    {
        base.OnStart();
        component.Value.entity.blockPhysicDamage.blockChance = chance;
    }
}

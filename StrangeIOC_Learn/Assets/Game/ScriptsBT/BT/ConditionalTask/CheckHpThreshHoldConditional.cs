using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHpThreshHoldConditional : Conditional
{
    public SharedFloat hpThreshHold;
    public SharedComponentManager component;
    public SharedInt numberOfSummon;
    public int increasedNumberOfSummon;
    public int maxSummon;
    public override TaskStatus OnUpdate()
    {
        if((float)component.Value.entity.health.HP.Value<= (float) component.Value.entity.health.MaxHP*hpThreshHold.Value)
        {
            if(numberOfSummon.Value < maxSummon)
            numberOfSummon.Value += increasedNumberOfSummon;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}

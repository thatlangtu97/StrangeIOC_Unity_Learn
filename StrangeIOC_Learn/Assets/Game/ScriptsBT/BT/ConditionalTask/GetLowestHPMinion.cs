using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLowestHPMinion : Conditional
{
    public SharedComponentManager componentManager;
    public SharedGameObjectList minions;
    public SharedGameObject targetMinion;
    public override TaskStatus OnUpdate()
    {
        if(minions.Value.Count == 0)
        {
            return TaskStatus.Failure;
        }
        targetMinion.Value = minions.Value[0];
        //Debug.Log(minions.Value[0]);
        float hp = targetMinion.Value.GetComponent<StateMachineController>().componentManager.entity.health.HP.Value/ targetMinion.Value.GetComponent<StateMachineController>().componentManager.entity.health.HP.Value;
        for (int i = 1; i< minions.Value.Count; i++)
        {
            float otherHP = minions.Value[i].GetComponent<StateMachineController>().componentManager.entity.health.HP.Value / minions.Value[i].GetComponent<StateMachineController>().componentManager.entity.health.MaxHP;
            if (otherHP < hp)
            {
                targetMinion.Value = minions.Value[i];
                hp = otherHP;
            }
        }

        if(hp >= 1)
        {
            return TaskStatus.Failure;
        }
        else
        {
            return TaskStatus.Success;
        }

    }
}
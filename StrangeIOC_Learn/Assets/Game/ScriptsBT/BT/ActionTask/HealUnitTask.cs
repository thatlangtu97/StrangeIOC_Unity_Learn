using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealUnitTask : Action
{
    // Start is called before the first frame update
    public SharedComponentManager componentManager;
    public SharedFloat healMultiple;
    public bool isHealByPercentHp;
    public SharedGameObject targetUnit;
    public override void OnStart()
    {
        float healAmount = 0f;
        if (isHealByPercentHp)
        {
            healAmount = healMultiple.Value * targetUnit.Value.GetComponent<StateMachineController>().componentManager.entity.health.MaxHP;
        }
        else
        {
            healAmount = healMultiple.Value * componentManager.Value.entity.attackPower.atk;
        }
        //Debug.Log("heal" + targetUnit.Value.GetComponent<StateMachineController>().componentManager);
        var tempEntity = CleanUpBufferManager.instance.CreateTempEntity();
        CleanUpBufferManager.instance.AddReactiveComponent(() => { tempEntity.AddHeal(healAmount, targetUnit.Value.GetComponent<StateMachineController>().componentManager.entity); }, () => { tempEntity.Destroy(); });
    }
}

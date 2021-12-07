using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAllyTask : Action
{
    // Start is called before the first frame update
    public LayerMask whatIsAllies;
    public SharedFloat radius;
    public SharedComponentManager componentManager;
    public SharedFloat healMultiple;
    public bool isHealByPercentHp;
    public override void OnStart()
    {
        Vector2 castPoint = componentManager.Value.entity.centerPoint.centerPoint.position;
        var cols = Physics2D.OverlapCircleAll(castPoint, radius.Value, whatIsAllies);
        foreach (var col in cols)
        {
            float healAmount = 0f;
            
            Detectable detect = col.GetComponent<Detectable>();
            if (!detect.immuneToEffects)
            {


                GameEntity ally = detect.componentManager.entity;
               
                if (ally != null)
                {
                    if(ally != componentManager.Value.entity)
                    {
                        if (isHealByPercentHp)
                        {
                            healAmount = healMultiple.Value * ally.health.MaxHP;
                        }
                        else
                        {
                            healAmount= healMultiple.Value* componentManager.Value.entity.attackPower.atk;
                        }
                        var tempEntity = CleanUpBufferManager.instance.CreateTempEntity();
                        CleanUpBufferManager.instance.AddReactiveComponent(() => { tempEntity.AddHeal(healAmount, ally); }, () => { tempEntity.Destroy(); });
                    }
                }
            }
        }

    }
}

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HpChange : Action
{
    public SharedComponentManager componentManager;
    public SharedFloat percentToRestore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override TaskStatus OnUpdate()
    {
        componentManager.Value.entity.health.HP.Value = (int)(componentManager.Value.entity.health.MaxHP * percentToRestore.Value);
        return TaskStatus.Success;
    }
}

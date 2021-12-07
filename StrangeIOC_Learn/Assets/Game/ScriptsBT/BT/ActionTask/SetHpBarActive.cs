using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHpBarActive : Action
{
    // Start is called before the first frame updatepublic LayerMask whatIsAllies;
    public SharedComponentManager componentManager;
    public SharedBool active;

    public override void OnStart()
    {
        componentManager.Value.entity.healthBar.isActive = active.Value;
        componentManager.Value.entity.healthBar.healthbar.transform.parent.gameObject.SetActive(active.Value);        
    }
}

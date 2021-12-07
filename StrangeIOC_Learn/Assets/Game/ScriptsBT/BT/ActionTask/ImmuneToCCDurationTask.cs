using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmuneToCCDurationTask : Action
{
    public SharedFloat duration;
    float currDuration;
    public bool persistance;
    Detectable body;
    public override void OnStart()
    {
        if (body == null)
        {
            body = gameObject.GetComponentInChildren<Detectable>();
        }
        //StopAllCoroutines();
        body.StartCoroutine(StartImmuneCC());
    }
    IEnumerator StartImmuneCC()
    {
        currDuration = duration.Value;
        
        Immune(true);
        while(currDuration>0f)
        {
            
            currDuration -= Time.deltaTime;
            yield return null;
            if(persistance)
            {
                Immune(true);
            }
        }
       
        Immune(false);
    }
    void Immune(bool isImmune)
    {
        if(body.componentManager.entity!=null&&body.componentManager.entity.hasStateMachineContainer)
        {
            body.immune = isImmune;
        }
      
    }
}

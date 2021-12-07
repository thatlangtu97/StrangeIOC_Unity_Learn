using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealStatus : DamageCollider
{
    public GameObject sourceDmg;
    
   
   
    public List<EffectInfo> effect;
    bool setup = false;

    private void Start()
    {
        StartCoroutine(DelaySetup());
    }



  

    IEnumerator DelaySetup()
    {
        yield return new WaitForSeconds(1f);
        allEffects.Add(effect);
        source = sourceDmg.GetComponent<ComponentManager>().entity;
        setup = true;
    }
}

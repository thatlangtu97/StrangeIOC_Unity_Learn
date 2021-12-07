using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDealDamage: MonoBehaviour
{
    public float frequency;
    float curTime;
    public DamageCollider dmgc;
    private void OnParticleCollision(GameObject other)
    {
        curTime -= Time.deltaTime;
        if (curTime <= 0)
        {
            curTime = frequency;
            DealDmgManager.DealDamage(other, dmgc.allEffects, dmgc.attackInfo, dmgc.source);               
        }
        
    }


}

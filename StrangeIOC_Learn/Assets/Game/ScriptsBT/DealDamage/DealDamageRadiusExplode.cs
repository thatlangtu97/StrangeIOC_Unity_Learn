//using Spine.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageRadiusExplode : DamageCollider
{
    public float waitForFx;
    public Transform dealDmgCastPoint;
    public float radius;
    public int numberOfTarget;
    public LayerMask whatIsEnemy;
    Collider2D[] result;
    int numberOfVictim;
    public bool notRecycle;
    //DamageCollider dealDamage;
    public List<EffectInfo> effect;

    //public bool castRemainFX;
    //public GameObject remainFX;
    //public int castChance;
    //int rand;
    private void Awake()
    {
        result = new Collider2D[numberOfTarget];     
        //dealDamage = GetComponent<DealDamageRadiusExplode>();
        //dealDamage.allEffects.Add(effect);
        allEffects.Add(effect);
        //dealDamage.source = Contexts.sharedInstance.game.playerFlagEntity.stateMachineContainer.stateMachine.componentManager.entity;          
        source = Contexts.sharedInstance.game.playerFlagEntity.stateMachineContainer.stateMachine.componentManager.entity;
    }

    private void OnEnable()
    {
        DealDamage();
    }

   

    private void DealDamage()
    {

        numberOfVictim = Physics2D.OverlapCircleNonAlloc(dealDmgCastPoint.position, radius, result, whatIsEnemy);
       
        
        if (numberOfVictim != 0)
        {
            foreach (Collider2D col in result)
            {
                if (col != null)
                {
                   
                    if (source != null)
                    {
                        DealDmgManager.DealDamage(col, allEffects, attackInfo, source);
                        //CastRemainFx(col);                    
                    }
                }
                else
                    break;
            }
        }
        if(!notRecycle)
        {
            ObjectPool.Recycle(gameObject, waitForFx);        
        }
      
        
    }

    //void CastRemainFx(Collider2D col)
    //{
    //    if (castRemainFX)
    //    {
    //        rand = Random.Range(0, 100);
    //        if(rand <= castChance)
    //        ObjectPool.Spawn(remainFX, col.transform.position);
    //    }
    //}

    public override void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : DamageCollider
{
    public float radius;
    public int numberOfTarget;
    public LayerMask whatIsEnemy;
    public float speed;
    public float stopTime;
    float curStopTime;
    public GameObject impactFX;
    bool active;
    bool isMoving;
    int targetIndex;
    Collider2D[] result;
    int numberOfVictim;
    public List<EffectInfo> effect;
    private void Awake()
    {
        result = new Collider2D[numberOfTarget];
        allEffects.Add(effect);    
        source = Contexts.sharedInstance.game.playerFlagEntity.stateMachineContainer.stateMachine.componentManager.entity;
    }

    private void OnEnable()
    {
        
        DealDamage();
       
    }



    private void DealDamage()
    {  
        numberOfVictim = Physics2D.OverlapCircleNonAlloc(transform.position, radius, result, whatIsEnemy);
        if (numberOfVictim == 0)
        {
            isMoving = false;
            active = false;
            ObjectPool.Recycle(gameObject);     
        }     
        else
        {
            active = true;
            isMoving = true;
        }
            
        //if (numberOfVictim != 0)
        //{
        //    foreach (var col in result)
        //    {
        //        if (col != null)
        //        {

        //            if (source != null)
        //            {
        //                DealDmgManager.DealDamage(col, allEffects, attackInfo, source);
        //            }
        //        }
        //        else
        //            break;
        //    }
        //}
        //if (!notRecycle)
        //{
        //    ObjectPool.Recycle(gameObject, waitForFx);
        //}

        
    }

    private void Update()
    {
        if(active)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        if(!isMoving)
        {
            curStopTime -= Time.deltaTime;
            if(curStopTime <=0)
            {
                targetIndex++;           
                if (targetIndex >= result.Length)
                {
                    ObjectPool.Recycle(gameObject);
                    active = false;                   
                    return;
                }

                if (result[targetIndex] == null)
                {
                    ObjectPool.Recycle(gameObject);
                    active = false;                  
                    return;
                }
               
                    
                isMoving = true;
            }
            return;
        }

        if (result[targetIndex] != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, result[targetIndex].transform.position, speed * Time.deltaTime);        
        }     
        else
        {          
            isMoving = false;
        }
            

        if (isMoving)
        {         
            if(transform.position == result[targetIndex].transform.position)
            {
                if (source != null)
                {                
                    DealDmgManager.DealDamage(result[targetIndex], allEffects, attackInfo, source);
                    EffectRequestManager.RequestEffect(impactFX, null, transform.position, 0.2f);
                }              
                curStopTime = stopTime;
                isMoving = false;
            }     
          
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void OnDisable()
    {
        targetIndex = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageAfterTime : ProjectileDamageCollider
{
    public bool noDamage;
    public float delayTime;
    public GameObject dealDmgFx;
    public GameObject remainFx;
    public float remainfxDuration;
    public Transform dealDmgCastPoint;
    public int dealDmgTimes=1;
    public float interval;
    public float delayDestroy = 1f;
    public bool notDestroyAfterTime;
    public bool notRecycleDealDmgFx;
    public bool shake;
    public bool spawnOnGround;
    public ShakeCameraCommand shakeCommand;
    private void OnEnable()
    {
        
        StopAllCoroutines();
        StartCoroutine(DealDamage());   
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public void disableCoroutines() {
        StopAllCoroutines();
    }
    public override void  OnTriggerEnter2D(Collider2D collision)
    {
        //do nothing
    }
    IEnumerator DealDamage()
    {

     
        yield return new WaitForSeconds(delayTime);



        for (int i=0;i<dealDmgTimes;i++)
        {
            if(!noDamage)
            {
                DealDmgAfterTime();
            }
            
            
            if (dealDmgFx != null)
            {
                if (notRecycleDealDmgFx)
                {
                    ObjectPool.Spawn(dealDmgFx, transform.position);
                }
                else
                {
                    EffectRequestManager.RequestEffect(dealDmgFx, null, transform.position, 2f);
                }
            }
               
            if(remainFx!=null)
            {
                if (notRecycleDealDmgFx)
                {
                    if(spawnOnGround)
                    ObjectPool.Spawn(remainFx, new Vector2(transform.position.x, -4.7f));
                    else
                    ObjectPool.Spawn(remainFx, transform.position);
                }
                else
                {
                    EffectRequestManager.RequestEffect(remainFx, null, new Vector2(transform.position.x, -4.7f), remainfxDuration);
                }
            }
            yield return new WaitForSeconds(interval);
        }
        if(!notDestroyAfterTime)
        ObjectPool.Recycle(gameObject, delayDestroy);
        
    }
    public Vector2 size;
    public LayerMask whatIsEnemy;

    void DealDmgAfterTime()
    {
        Collider2D[] cols;
        cols = Physics2D.OverlapBoxAll(dealDmgCastPoint.position, size, 0f, whatIsEnemy);
        if (cols != null)
        {

            foreach (var col in cols)
            {
                //Debug.Log("Deal dmg" + col.gameObject.name);
                if (source != null && col.gameObject.GetComponent<Detectable>())
                {
                    DealDmgManager.DealDamage(col, allEffects, attackInfo, source);
                    if (source.attackPower.identity == IdentityType.Player)
                    {
                        EffectSoundController.instance.PlaySoundHit();
                    }
                }

                // DealDmgManager.DealDamage(col, allEffect, attackInfo, component.Value.entity);

            }
        }
        if (shake)
            CameraFollow.instance.Shake(shakeCommand.level, shakeCommand.duration);
    }
    
}

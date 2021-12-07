using Com.LuisPedroFonseca.ProCamera2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventAnimationToStateMachine : MonoBehaviour
{
    public GameObject EarthStompFx;
    public GameObject groundCastPoint;
    public Vector3 offset;
    public StateMachineController stateMachine;
    public ItemToDrop[] itemDrops;
    [SerializeField]
    bool lockForce;
    public List<UnityEvent> listSpecialEvent = new List<UnityEvent>();
    Vector3 offsetConvert;
    //call thong qua animation eventinvoke
    public void CallSpecialEvent(int eventId)
    {
        listSpecialEvent[eventId].Invoke();
    }
    public void OnHit() // khi bat dau animation hit
    {
        stateMachine.isHit = true;
        
    }
    public void OnHitDone() // khi het animation hit
    {
        stateMachine.isHit = false;
    }
    public void GetUp() // khi het animation get up
    {
        stateMachine.GetUp();
    }
    /*
    public void OnLayOnGround() // khi het animation knockdown 1
    {
        //  stateMachine.componentManager.entity.rigidbodyContainer.rigidbody.velocity = Vector2.zero; //lock 
    }
    */
    public void Move(int movePower)
    {
        //Debug.Log("trigger" + movePower);
        //stateMachine.componentManager.entity.rigidbodyContainer.rigidbody.AddForce(stateMachine.transform.right * movePower * 20f);

        if (stateMachine.componentManager.entity.hasDash)
        {
            stateMachine.componentManager.entity.ReplaceDash(0.5f, 0.5f, movePower*0.1f, movePower*0.1f);
        }
        else
        {
            stateMachine.componentManager.entity.AddDash(0.5f, 0.5f, movePower*0.1f, movePower*0.1f);
        }
      //  stateMachine.componentManager.entity.dash.power = 1*movePower+0.1f;
       // stateMachine.componentManager.entity.dash.duration = (float) movePower * 0.01f;
        CleanUpBufferManager.instance.AddReactiveComponent(
            () =>
            {
               
              //  stateMachine.componentManager.entity.AddKnockBack(stateMachine.transform.right * movePower * 20);
            },
            () =>
            {
               // stateMachine.componentManager.entity.RemoveKnockBack();
            });
    }

    public void StopMove()
    {
        if (stateMachine.suspendAnimationEvent)
            return;
        if (stateMachine.componentManager.entity.hasDash)
        {
            stateMachine.componentManager.entity.RemoveDash();
        }
        if (stateMachine.componentManager.entity.hasRigidbodyContainer)
        stateMachine.componentManager.entity.rigidbodyContainer.rigidbody.velocity = Vector2.zero;
    }
    /*
    public void UnlockAttackAnim()
    {
        stateMachine.animator.SetBool(AnimationName.IS_ON_ATTACK, false);
    }

    public void ContinueMove()
    {
        stateMachine.componentManager.entity.isStopMove = false;
    }

    public void PauseMove()
    {
        stateMachine.componentManager.entity.isStopMove = true;
    }

    

    public void SetLockForce()
    {
        //Debug.Log("change force");
        if (lockForce)
        {
            //lockForce = false;
        }
        else
        {
            lockForce = true;
        }
    }

    public void ForceExitState()
    {
        //Debug.Log("anim exit");
        if (!lockForce)
        {
            stateMachine.OnForceExitState();
            //Debug.Log("force");
        }

    }

    public void RemoveLockForce()
    {
        lockForce = false;
    }

    public void ForceMove()
    {
        //Debug.Log(lockForce);
        if (!lockForce)
        {
            stateMachine.componentManager.entity.isStopMove = false;
        }
    }
    */
    public void DieAnimation()
    {

        GameEntity tempEntity = Contexts.sharedInstance.game.CreateEntity();
        CleanUpBufferManager.instance.AddReactiveComponent(
            () =>
            {
                tempEntity.AddCenterPoint(transform, transform);
                tempEntity.AddSpawnOnDeath(itemDrops);
            },
            () =>
            {
                tempEntity.Destroy();
            }
            );
        //Debug.Log("spawn effect");
    }
    IEnumerator DelaySpawnEfect(GameObject fxPrefab, float force = 0f) {
        yield return new WaitForSeconds(0f);
        GameObject fx = ObjectPool.Spawn(fxPrefab, stateMachine.componentManager.entity.centerPoint.centerPoint.position, fxPrefab.transform.rotation);
        if (force != 0)
            fx.transform.localScale = new Vector3(force, 1f, 1f);

        ObjectPool.Recycle(fx, 3);
    }

    public void SpawnFx(GameObject fxPrefab, Vector2 position, float force =0f)
    {
        //StartCoroutine(DelaySpawnEfect(fxPrefab, force));


        GameObject fx = ObjectPool.Spawn(fxPrefab, position, fxPrefab.transform.rotation);
        if (force != 0)
            fx.transform.localScale = new Vector3(force, 1f, 1f);
       
    }

    

    public void ShakeTime(float time)
    {
        Camera.main.GetComponent<ProCamera2DShake>().Shake(time, Vector2.one * 0.2f);
    }
    public void SpawnFxDie(float force=0f) 
    {     
        if (itemDrops.Length != 0)
        {
            for (int i = 0; i < itemDrops.Length; i++)
            {
                SpawnFx(itemDrops[i].dropObj, stateMachine.centerPoint.position, force);
            }
            
            //Debug.Log("spawn effect");
        }
        else
        {
            //Debug.Log("No Gameobject effect");
        }
    }

    public void SpawnDeathEffect()
    {
        if(GameSceneConfig.instance.enemyDeathEffect.Count!=0)
        for (int i = 0; i < GameSceneConfig.instance.enemyDeathEffect.Count; i++)
        {

                //SpawnFx(GameSceneConfig.instance.enemyDeathEffect[i], stateMachine.componentManager.entity.centerPoint.groundPoint.position);
                if (GameSceneConfig.instance.enemyDeathEffect[i] != null)
                    SpawnFx(GameSceneConfig.instance.enemyDeathEffect[i], stateMachine.centerPoint.position);
        }
        
    }

    public void SpawnEarthStomp()
    {
        offsetConvert = offset;
        if (!stateMachine.characterDirection.isFaceRight)
            offsetConvert.x *= -1; 
        
        ObjectPool.Spawn(EarthStompFx, groundCastPoint.transform.position + offsetConvert, EarthStompFx.transform.rotation);
    }

    public void LockDirection()
    {
        stateMachine.characterDirection.lockDirection = true;
        //Debug.Log("Locl turning");
    }
}

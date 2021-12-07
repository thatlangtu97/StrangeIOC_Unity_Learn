using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy Die State", menuName = "State/Enemy/Die")]
public class EnemyDieState : State
{
    //public float delayDisableTime = 2f;
    //[Header("custom Scale When Enemy Die")]
    //public bool UseScaleWhenDie;
    //public Vector2 ScaleValue;
    //public float timeScale;
    //public int indexChildVisual;

    bool end;
    //bool visualSet;
    //float enterTime;
    public override void EnterState()
    {
        base.EnterState();
        end = false;
        if (controller.attackSoundController != null)
            controller.attackSoundController.StopSound();

        controller.animator.SetBool(AnimationName.MOVE, false);

        if (controller.movementSoundController != null)
        controller.movementSoundController.PlaySound(AudioName.Die, false);

        controller.behaviorTree.DisableBehavior(true);

        // KHONG CHO DI CHUYEN NGANG KHI BI CHET
        controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity = new Vector2(0, controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y);
        
        CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = false; } });
        if (controller.componentManager.entity.isEnabled == true)
        {
            controller.componentManager.entity.isAttackDone = true;
            CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = false; controller.componentManager.entity.isSkillComplete = false; } });
        }


        if (!controller.noHitStop && !CameraFollow.instance.checkOffScreen(controller.transform.position))
        {
            float shakeDuration;
            if (controller.componentManager.entity.hasBossHpBar)
            {                
                shakeDuration = GameSceneConfig.instance.bossKillHitStopDuration;
            }
            else
            {
                shakeDuration = GameSceneConfig.instance.enemyHitStopDuration;
            }
            HitStopController.instance.HitStop(1, shakeDuration);
        }       

        //if (controller.componentManager.dependCount > 0)
        //{
        //    enterTime = Time.time;
        //    visualSet = false;
        //}
        //else
        //{
        //    visualSet = true;
        //}
        
        //DependPooledObject depends = controller.GetComponent<DependPooledObject>();
        //if(depends != null)
        //{
        //    foreach(GameObject obj in depends.dependList)
        //    {
        //        ObjectPool.Recycle(obj);
        //    }
        //}
        //Debug.Log("Die State");
        controller.animator.SetBool(AnimationName.LOCK_HIT_ANIM, true);
        controller.animator.SetTrigger(AnimationName.DIE);
        controller.animator.SetBool("DieBool",true);
        if (controller.effectSoundController != null)
            controller.effectSoundController.PlaySoundDie();
      
        if (GameSceneConfig.instance.deathStatus && controller.componentManager.entity.attackPower.identity != IdentityType.Boss)
        {
            
            controller.animator.gameObject.GetComponent<EventAnimationToStateMachine>().SpawnDeathEffect();
            controller.HideGameObject(controller.useHideGameObject);
            controller.useEffectDie = false;
        }

        if (controller.useEffectDie)
        {

            controller.animator.gameObject.GetComponent<EventAnimationToStateMachine>().SpawnFxDie(controller.forceDie);

            controller.HideGameObject(controller.useHideGameObject);
            //controller.animator.gameObject.SetActive(false);
        }

      
    }
    public override TaskStatus OnFrezee(float duration)
    {
        return TaskStatus.Failure;
    }
    public override TaskStatus OnKnockDown()
    {
        return TaskStatus.Failure;
    }
    public override void UpdateState(float deltaTime)
    {
        if (controller.componentManager.dependCount <= 0 && !end)
        {
            end = true;
            if (controller.componentManager.entity.checkEnemyInSigh.lockedTarget != null)
            {
                controller.componentManager.entity.checkEnemyInSigh.lockedTarget.postEvent(EventID.GAME_EVENT, new EventPassiveContainer(EventPassive.ON_KILL_ENEMY, controller.componentManager.entity));
            }
            if (controller.componentManager.entity.attackPower.identity != IdentityType.Boss)
                GameObject.Destroy(controller.gameObject, 2.5f);
            else
                GameObject.Destroy(controller.gameObject, 6f);

            CleanUpBufferManager.instance.AddReactiveComponent(() => { }, () => { if (controller.componentManager.entity != null) controller.componentManager.entity.Destroy(); });

        }

        /*
        if (timeScale > 0)
        {
            timeScale -= Time.deltaTime;

               
        }
        else
        {
            if (UseScaleWhenDie)
            {
                controller.transform.GetChild(indexChildVisual).localScale = Vector2.Lerp(controller.transform.GetChild(indexChildVisual).localScale, ScaleValue, 0.05f);
            }
        }
        */
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy Die Fall State", menuName = "State/Enemy/DieFall")]
public class EnemyDieFallState : State
{
    public float checkDistance;
    public float animationDieEndDuration;
    bool onAir;
    bool visualSet;
    float enterTime;
    public bool useChangeBoxValue;
    float gravityScale;
    bool end;
    public override void EnterState()
    {

        end = false;

        base.EnterState();
        controller.animator.SetLayerWeight(1, 0); // disable blending with fly minion when die animation start
        gravityScale = controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale;
        onAir = true;
        visualSet = false;
        //base.EnterState();

        if (controller.componentManager.entity.isEnabled == true)
        {
            controller.componentManager.entity.isAttackDone = true;
            CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = false; controller.componentManager.entity.isSkillComplete = false; } });
        }


     
        CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = false; } });

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
        DependPooledObject depends = controller.GetComponent<DependPooledObject>();
        if (controller.componentManager.dependCount > 0)
        {
            enterTime = Time.time;
            visualSet = false;
        }
        else
        {
            visualSet = true;
        }

        if (depends != null)
        {
            foreach (GameObject obj in depends.dependList)
            {
                ObjectPool.Recycle(obj);
            }
        }
        //controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity = new Vector2(0f, controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity.y);
        controller.componentManager.entity.rigidbodyContainer.rigidbody.bodyType = RigidbodyType2D.Dynamic;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale = 2;

        controller.animator.SetBool(AnimationName.LOCK_HIT_ANIM, true);
        controller.animator.SetTrigger(AnimationName.DIE);
        controller.animator.SetBool("DieBool", true);
        if (controller.effectSoundController != null)
            controller.effectSoundController.PlaySoundDie();

        if (useChangeBoxValue)
        {
            controller.softBody.GetComponent<Detectable>().KnockDown();
        }
        /*
        if (controller.AddforceUPDead)
        {
            controller.componentManager.entity.rigidbodyContainer.rigidbody.AddForce(new Vector2(0f, controller.valueForce));
        }*/
        controller.transform.rotation = Quaternion.LookRotation(controller.transform.forward);
    }
    public override void UpdateState(float deltaTime)
    {
        if (onAir)
        {
            RaycastHit2D hit = Physics2D.Raycast(controller.transform.position, Vector2.down, checkDistance, GameSceneConfig.instance.groundCastMask);
           
            if (hit.collider != null)
            {
                onAir = false;
       
                controller.animator.SetTrigger(AnimationName.DIE_END);

                if (GameSceneConfig.instance.deathStatus)
                {

                    controller.animator.gameObject.GetComponent<EventAnimationToStateMachine>().SpawnDeathEffect();
                    controller.HideGameObject(controller.useHideGameObject);
                    controller.useEffectDie = false;
                }

                if (controller.useEffectDie)
                {

                    controller.animator.gameObject.GetComponent<EventAnimationToStateMachine>().SpawnFxDie(controller.forceDie);
                    controller.HideGameObject(controller.useHideGameObject);
                }
            }
        }

        if (controller.componentManager.dependCount <= 0 && !onAir && !end)
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

        if (!visualSet)
        {
            if (Time.time - enterTime > 2f)
            {
                visualSet = true;
                controller.animator.gameObject.SetActive(false);
            }
        }

      
    }
    
}

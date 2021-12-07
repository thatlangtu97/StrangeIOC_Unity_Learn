using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeSkill : Action
{
    public SharedComponentManager componentManager;
    private float rangeToEnemy;
    public SharedFloat chargeSpeedMultiple;
    StateMachineController stateMachine;
    float speedScaleOriginal;
    public LayerMask stopMask;
    public LayerMask noneDetectableMask;
    public float rangeCheck = 1f;
    public bool slowDownBeforeStop;
    public bool stopWhenMissPlayer;

    public float waitUntilStop;

    float elapseTime;
    public float slowDownDuration;
    public List<AnimationChain> listAnimation;
    public SharedGameObject castPoint;
    bool isStop;
    bool isStartAnimation;
    float curSpeedScale;
    GameObject hitTarget;
    bool hitPLayer;
    public override void OnStart()
    {
        base.OnStart();

        if(slowDownBeforeStop)
        {
            isStop = false;
            isStartAnimation = false;
            elapseTime = slowDownDuration;
            curSpeedScale = chargeSpeedMultiple.Value;
            hitTarget = null;
            hitPLayer = false;
        }
       
        stateMachine = componentManager.Value.entity.stateMachineContainer.stateMachine;
        speedScaleOriginal = componentManager.Value.entity.moveByDirection.speedScale;
        componentManager.Value.entity.moveByDirection.speedScale = chargeSpeedMultiple.Value;
        
        componentManager.Value.entity.isStopMove = false;
        componentManager.Value.entity.moveByDirection.direction = transform.right;

    }
    public override TaskStatus OnUpdate()
    {
        componentManager.Value.entity.moveByDirection.speedScale = chargeSpeedMultiple.Value;
        if (stopWhenMissPlayer && !isStop)
        {
            CalculateRangeToEnemy();
            if (!CheckEnemyForward())
            {
                //CheckSlowDownWhenHit();
                isStop = true;
                StartCoroutine(CheckSlowDownWhenHit());
            }             
        }

        if(isStop && isStartAnimation)
        {
            return SLowDown();
        }

        //RaycastHit2D hit = Physics2D.Raycast(componentManager.Value.entity.centerPoint.centerPoint.position, transform.right, rangeCheck, stopMask); // when hit player
        RaycastHit2D hit;
        if(castPoint.Value !=null)
        {
            hit = Physics2D.BoxCast(castPoint.Value.transform.position,
            componentManager.Value.entity.stateMachineContainer.stateMachine.bodyCollider.size + new Vector2(rangeCheck, 0), 0, Vector2.right, 1, stopMask);
        }
        else
        {
            hit = Physics2D.BoxCast(componentManager.Value.entity.centerPoint.centerPoint.position,
            componentManager.Value.entity.stateMachineContainer.stateMachine.bodyCollider.size + new Vector2(rangeCheck, 0), 0, Vector2.right, 1, stopMask);
        }
        
        if (hit.collider != null) //if hit then slow down and stop
        {
            
            if (!slowDownBeforeStop)
            componentManager.Value.entity.moveByDirection.speedScale = speedScaleOriginal;

            if(stopWhenMissPlayer && !isStop)
            {
                isStop = true;
                hitTarget = hit.collider.gameObject;
                
                if(hitTarget.gameObject.layer != noneDetectableMask)
                {
                    if (!hitTarget.gameObject.GetComponent<Detectable>().immune)
                        hitPLayer = true;
                }
                StartCoroutine(CheckSlowDownWhenHit());    
            }

           
            //CheckSlowDownWhenHit();

            if(!slowDownBeforeStop)
            {
                return TaskStatus.Success;
            }
            else
            {        
                return TaskStatus.Running;
            }
        }

      
        if(stateMachine.currentState != stateMachine.skillState)
        {
            return TaskStatus.Success;
        }
            
        return TaskStatus.Running;
    }
    public override void OnEnd()
    {
        
        base.OnEnd();
        componentManager.Value.entity.moveByDirection.speedScale = speedScaleOriginal;
        componentManager.Value.entity.isStopMove = true;
    }
   
    void SetUpAnimation()
    {
        List<AnimationChain> listAnimationChainClone = new List<AnimationChain>();
        for (int i = 0; i < listAnimation.Count; i++)
        {
            AnimationChain chain = new AnimationChain();
            chain.animationName = listAnimation[i].animationName;
            chain.parameter = listAnimation[i].parameter;
            chain.delayTime = listAnimation[i].delayTime;
            chain.animationType = listAnimation[i].animationType;
            chain.isTriggered = false;
            listAnimationChainClone.Add(chain);
        }
        if (componentManager.Value.entity.hasAnimationChain)
        {

            componentManager.Value.entity.ReplaceAnimationChain(listAnimationChainClone, 0);
        }
        else
        {

            componentManager.Value.entity.AddAnimationChain(listAnimationChainClone, 0);
        }
        isStartAnimation = true;

    }
    
    bool CheckEnemyForward()
    {

        if (rangeToEnemy > 0.1f)
        {
            if (!stateMachine.characterDirection.isFaceRight) // neu k dang quay mat ben phai thi change direction
            {
                return false; // no enemy
            }
            else
            {

                return true;
            }
        }
        else if (rangeToEnemy < -0.1f)
        {
            if (stateMachine.characterDirection.isFaceRight)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    void CalculateRangeToEnemy()
    {
        if(componentManager.Value.entity.checkEnemyInSigh != null)
        rangeToEnemy = componentManager.Value.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint.position.x - componentManager.Value.entity.centerPoint.centerPoint.position.x;

        if (rangeToEnemy == 0)
            rangeToEnemy = 0.01f;
    }

    IEnumerator CheckSlowDownWhenHit()
    {
        yield return new WaitForSeconds(waitUntilStop);
        if (slowDownBeforeStop)
        {
            SetUpAnimation();
        }
    }
    TaskStatus SLowDown()
    {
        elapseTime -= Time.deltaTime;
        componentManager.Value.entity.moveByDirection.speedScale = curSpeedScale * ((elapseTime / slowDownDuration));
        /*
        if (elapseTime <= 0)
        {
            isStop = true;
            componentManager.Value.entity.moveByDirection.speedScale = speedScaleOriginal;
           
            return TaskStatus.Success;
        }
        else
        {
            componentManager.Value.entity.moveByDirection.speedScale = componentManager.Value.entity.moveByDirection.speedScale * ((elapseTime / slowDownDuration));
            return TaskStatus.Running;
        }
        */
        if (elapseTime <= 0)
        {
            isStop = true;
            componentManager.Value.entity.moveByDirection.speedScale = speedScaleOriginal;

            
            if (hitTarget)
            {            
                if(hitPLayer)
                {                 
                    return TaskStatus.Failure;
                }            
            }
            return TaskStatus.Success;
        }
        else
            return TaskStatus.Running;
    }


}

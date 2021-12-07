using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy KnockDown State", menuName = "State/Enemy/EnemyKnockDownState")]
public class EnemyKnockDownState : KnockDownState
{
    public bool fixedKnockDownTime;
    float knockDownTime;
    bool isGetingUp = false;
    bool isGrounded;
    public float delayTime = 0.2f;
    float animationSpeed;
    public override void EnterState()
    {
        animationSpeed = controller.animator.speed;
        base.EnterState();
        if (controller.componentManager.entity.isPlayerFlag)
            controller.componentManager.entity.postEvent(EventID.GAME_EVENT, new EventPassiveContainer(EventPassive.ON_GET_UP, null));
        if (controller.componentManager.entity.hasMoveByDirection)
            controller.componentManager.entity.moveByDirection.direction = Vector2.zero;
        controller.componentManager.entity.animatorContainer.animator.SetBool(AnimationName.MOVE, false);
        //controller.componentManager.entity.rigidbodyContainer.rigidbody.velocity = Vector2.zero;// cancel knock back
        if (!fixedKnockDownTime)
            knockDownTime = Random.Range(ConstantValueManager.instance.minKnockDownTime, ConstantValueManager.instance.maxKnockDownTime);
        else
            knockDownTime = delayTime;
        isGetingUp = false;
        isGrounded = false;
        
        if (controller.componentManager.entity.health.HP.Value <= 0)
        {
            //if (controller.behaviorTree != null)
            //    Destroy(controller.behaviorTree);
            if (controller.behaviorTree.enabled == true)
                controller.behaviorTree.enabled = false;
            CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = false; } });
            if (controller.componentManager.entity.isEnabled == true)
            {
                controller.componentManager.entity.isAttackDone = true;
                CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = false; controller.componentManager.entity.isSkillComplete = false; } });
            }
            controller.ChangeState(controller.dieState);
        }
        // controller.animator.SetLayerWeight((int)LayerWeightID.HIT, 0f);
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        if (controller.componentManager.entity.health.HP.Value <= 0)
        {
            //if (controller.behaviorTree != null)
            //    Destroy(controller.behaviorTree);
            if (controller.behaviorTree.enabled == true)
                controller.behaviorTree.enabled = false;
            CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = false; } });
            if (controller.componentManager.entity.isEnabled == true)
            {
                controller.componentManager.entity.isAttackDone = true;
                CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = false; controller.componentManager.entity.isSkillComplete = false; } });
            }
            controller.ChangeState(controller.dieState);
            // thêm return để không gọi nốt những lệnh cuối
            return;
        }

        if (delayTime>0f)
        {
            delayTime -= Time.deltaTime;
        }
        else
        {
            if (!isGrounded)
            {
                RaycastHit2D hit = Physics2D.Raycast(controller.transform.position, Vector2.down, 0.1f, GameSceneConfig.instance.groundCastMask);
                if (hit.collider != null)
                {
                    isGrounded = true;
                    controller.animator.SetTrigger(AnimationName.KNOCK_DOWN_2);
                    controller.bodyEffect.KnockDown();                       
                }
            }
            else
            {
                if (knockDownTime > 0f)
                {
                    knockDownTime -= Time.deltaTime;
                }
                else
                {
                    if (!isGetingUp)
                    {
                        isGetingUp = true;
                        controller.animator.SetTrigger(AnimationName.GET_UP);
                        controller.bodyEffect.ExitKnockDown();                      
                    }
                }
            }
        }
        

    }
    public override TaskStatus OnGetUp()
    {
        base.OnGetUp();
        if (controller.componentManager.entity.hasSlowMoveEffect)
            controller.animator.speed = animationSpeed;
        controller.bodyEffect.ExitKnockDown();
        
        controller.ChangeState(controller.idleState);
        
        return TaskStatus.Success;

    }
    public override TaskStatus OnFrezee(float duration)
    {
        return TaskStatus.Success;
    }

    public override TaskStatus OnInputDash()
    {
        return TaskStatus.Failure;
    }

}

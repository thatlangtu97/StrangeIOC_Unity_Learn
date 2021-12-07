using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Freeze State", menuName = "State/Enemy/Freeze")]
public class FreezeState : State
{
    public float freezeTime;
    GameObject freezeEffect;
    //Detectable hitBox;
    public override void EnterState()
    {
        controller.SpawnEffect(EffectType.Freeze);
        EffectSoundController.instance.PlayerSoundEffect();
        if (controller.componentManager.entity.hasBlockPhysicDamage)
        controller.componentManager.entity.blockPhysicDamage.blockChance = 0;

        //controller.StartCoroutine(TurnOffDamageBox());
        DisableDamageBox();

        if (controller.allFxOfSkillChanelling != null)
        {
            
            controller.allFxOfSkillChanelling.SetActive((false));
        }
        if (controller.isFreezeAndImmumeToForce)
        {
            controller.bodyEffect.immuneToForce = true;
        }
        controller.animator.enabled = false;
        controller.componentManager.entity.isStopMove = true;
        if (controller.componentManager.entity.hasMoveByDirection)
        {
            controller.componentManager.entity.moveByDirection.isEnable = false;
        }

        if (controller.componentManager.entity.hasMoveByDestination)
        {
            controller.componentManager.entity.moveByDestination.isEnable = false;
        }


        //controller.behaviorTree.PauseWhenDisabled = true;
        controller.behaviorTree.DisableBehavior(true);
        //controller.behaviorTree.enabled = false;
      

        if (controller.componentManager.entity.health.HP.Value <= 0)
        {
            //if (controller.behaviorTree != null)
            //    Destroy(controller.behaviorTree);
            
            CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = false; } });
            if (controller.componentManager.entity.isEnabled == true)
            {
                controller.componentManager.entity.isAttackDone = true;
                CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = false; controller.componentManager.entity.isSkillComplete = false; } });
            }
            controller.ChangeState(controller.dieState);
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        //controller.componentManager.entity.isStopMove = false;
        controller.animator.enabled = true;
        

        EnableDamageBox();
        DestroyFreezeEffect(controller.gameObject);

        controller.onRunOutOfHealthAction -= DestroyFreezeEffect;
        if (controller.isFreezeAndImmumeToForce)
        {
            controller.bodyEffect.immuneToForce = false;
        }
        Vector2 pos = controller.componentManager.entity.centerPoint.centerPoint.position;
        EffectRequestManager.RequestEffect(GameSceneConfig.instance.freezeBreakEffect,null  , pos,  2f);
     
        
        if (controller.componentManager.entity.hasMoveByDirection)
        {
            controller.componentManager.entity.moveByDirection.isEnable = true;
        }

        if (controller.componentManager.entity.hasMoveByDestination)
        {
            controller.componentManager.entity.moveByDestination.isEnable = true;
        }
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        freezeTime -= deltaTime;
        if (freezeTime <= 0f && controller.componentManager.entity.health.HP.Value > 0)
        {
            controller.DestroyEffect(EffectType.Freeze);
            if (controller.isFreezeAndImmumeToForce)
            {
                controller.bodyEffect.immuneToForce = false;
            }

            //if (controller.previousState != null)
            //    controller.ChangeState(controller.previousState);
            //else
            controller.behaviorTree.EnableBehavior();
            controller.ChangeState(controller.idleState);

            return;
        }
        if (controller.componentManager.entity.health.HP.Value <= 0)
        {
            controller.behaviorTree.DisableBehavior(true);
            if (controller.isFreezeAndImmumeToForce)
            {
                controller.bodyEffect.immuneToForce = true;
            }
            
            CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = false; } });
            if (controller.componentManager.entity.isEnabled == true)
            {
                controller.componentManager.entity.isAttackDone = true;
                CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = false; controller.componentManager.entity.isSkillComplete = false; } });
            }
            controller.ChangeState(controller.dieState);
        }
    }
    void DestroyFreezeEffect(GameObject characterObject)
    {
        controller.DestroyEffect(EffectType.Freeze);
        EffectSoundController.instance.PlayerSoundEffect();
    }
    public override TaskStatus OnFrezee(float duration)
    {       
        if (controller.isFreezeAndImmumeToForce)
        {
            controller.bodyEffect.immuneToForce = true;
        }
        controller.onRunOutOfHealthAction += DestroyFreezeEffect;

        freezeTime = duration;
        if (freezeTime <= 0)
            freezeTime = 2;
       
     
        if (controller.isFreezeAndImmumeToForce)
            controller.bodyEffect.immuneToForce = true;
        return TaskStatus.Success;
       
    }
    public override TaskStatus OnHit(bool isBlock)
    {
        return TaskStatus.Success;
        //do nothing
    }
    public override TaskStatus OnKnockDown()
    {
        return TaskStatus.Success;
        //ko the knock down
    }
    public override TaskStatus OnInputIdle()
    {
        return TaskStatus.Failure;
    }
    public override TaskStatus OnInputSkill(int skillId)
    {
        return TaskStatus.Failure;
    }
    public override TaskStatus OnInputMove()
    {
        return TaskStatus.Failure;
    }
    public override TaskStatus OnInputChangeFacing()
    {
        return TaskStatus.Failure;
    }

    IEnumerator TurnOffDamageBox()
    {
        yield return new WaitForEndOfFrame();
      
    }

    void DisableDamageBox()
    {
        if (controller.componentManager.entity.hasWeapon)// turn of damagebox
        {
            controller.componentManager.entity.weapon.weaponController.gameObject.SetActive(false);
        }
    }

    void EnableDamageBox()
    {
        if (controller.componentManager.entity.hasWeapon)// turn of damagebox
        {
            controller.componentManager.entity.weapon.weaponController.gameObject.SetActive(true);
        }
    }
}

using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[CreateAssetMenu(fileName = "Player Die State", menuName = "State/Player/Die")]
public class PlayerDieState : State
{
    //public int reviveCount = 100;
    bool visualSet;
    float enterTime;
    bool revive = false;
    public GameObject revivalShockWave;
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetBool("DieBool", true);
        controller.animator.SetBool(AnimationName.LOCK_HIT_ANIM, true);
        controller.animator.SetTrigger(AnimationName.DIE);
        if (controller.componentManager.entity.isImmortality)
        {
            controller.StartCoroutine(ReviveByPassive());
            return;
        }

        if (CaculateRegeToEnemy.instance != null)
        {
            if (CaculateRegeToEnemy.instance.listPet != null)
            {
                foreach(PetController pet in CaculateRegeToEnemy.instance.listPet)
                    pet.canFire = false;
            }
        }
        //Debug.Log("PlayerDie");
        CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = false; } });
        //if (controller.componentManager.entity.isEnabled == true)
        //{
            //controller.componentManager.entity.isAttackDone = true;
            //CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = false; controller.componentManager.entity.isSkillComplete = false; } });
        //}
        if (controller.allFxOfSkillChanelling != null)
        {
            controller.allFxOfSkillChanelling.SetActive((false));
        }
        if (controller.componentManager.dependCount > 0)
        {
            enterTime = Time.time;
            visualSet = false;
        }
        else
        {
            visualSet = true;
        }
        this.registerListener(EventID.PLAYER_REVIE, (sender, param)=>OnRevive());
        controller.componentManager.entity.isStopMove = true;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale = 3.5f;
        foreach (var bt in controller.componentManager.entity.skillContainer.BehaviorTrees)
        {
            bt.enabled = false;
        }

        //Debug.Log("TriggerDieAnimation");
        //controller.effectSoundController.PlaySoundDie();

        //controller.softBody.ChangeLayerMask(ConstantValueManager.instance.corpseLayer);
        controller.bodyEffect.immune = true;
        this.postEvent(EventID.ON_DIE);
        controller.StopAllCoroutines();
        controller.StartCoroutine(DisplayRevivePanel());
    }
    IEnumerator DisplayRevivePanel()
    {
        yield return new WaitForSecondsRealtime(1f);
        //Debug.Log("revive me");
        if(BrickController.instance==null)
            this.postEvent(EventID.REVIVE_POPUP);
        else
        {
            LevelCreatorModeLibrary.instance.ToGameOver();
        }
    }
    public override void UpdateState(float deltaTime)
    {
        //base.UpdateState(deltaTime);
    }
    
    public override void OnRevive()
    {
        if (!revive)
        {
            revive = true;
            base.OnRevive();
            controller.StartCoroutine(ReviveCoroutine());
        }
       
    }
    IEnumerator ReviveCoroutine()
    {
        controller.animator.SetBool("DieBool", false);
        controller.animator.SetBool(AnimationName.LOCK_HIT_ANIM, false);
        //EffectRequestManager.RequestEffect(GameSceneConfig.instance.reviveFx, controller.transform, Vector2.zero, 5f);
        ObjectPool.Spawn(GameSceneConfig.instance.reviveFx, controller.gameObject.transform);
        controller.animator.updateMode = AnimatorUpdateMode.UnscaledTime;

        controller.animator.SetTrigger(AnimationName.REVIVE);
        //Debug.Log("Revive");
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1.9f);
        this.postEvent(EventID.PLAYER_REVIE_DONE);
        yield return new WaitForSecondsRealtime(0.1f);
        controller.softBody.ChangeLayerMask(ConstantValueManager.instance.playerLayer);
        //ReviveView.instance.reviveCount--;
        Time.timeScale = 1f;
        controller.animator.updateMode = AnimatorUpdateMode.Normal;
        foreach (var bt in controller.componentManager.entity.skillContainer.BehaviorTrees)
        {
            
            bt.enabled = true;
        }
        var tempEntity = CleanUpBufferManager.instance.CreateTempEntity();
        //Debug.Log("revive");
        CleanUpBufferManager.instance.AddReactiveComponent(() => { tempEntity.AddHeal(controller.componentManager.entity.health.MaxHP, controller.componentManager.entity); }, () => { tempEntity.Destroy(); });
        controller.componentManager.entity.isAttackDone = false;
        controller.ChangeState(controller.idleState);
        InputManager.instance.uiGameplay.SetActive(true);
        if ( controller.componentManager. entity.hasImmune)
            controller.componentManager.entity.ReplaceImmune(Faction.PLAYER, 3f);
        else
            controller.componentManager.entity.AddImmune(Faction.PLAYER, 3f);
        controller.bodyEffect.immuneToForce = true;
        controller.immuneKnock = true;
        EffectRequestManager.RequestEffect(GameSceneConfig.instance.repelEffect, controller.componentManager.entity.centerPoint.centerPoint, Vector2.zero, 3f);
        // controller.componentManager.entity.AddImmune(Faction.PLAYER, 3f);
        revive = false;
        if (CaculateRegeToEnemy.instance != null)
        {
            if (CaculateRegeToEnemy.instance.listPet != null)
            {
                foreach (PetController pet in CaculateRegeToEnemy.instance.listPet)
                    pet.canFire = true;
            }
        }
    }

    IEnumerator ReviveByPassive()
    {
        yield return new WaitForSecondsRealtime(1f);
        controller.animator.SetBool("DieBool", false);
        controller.animator.SetBool(AnimationName.LOCK_HIT_ANIM, false);
       
        ObjectPool.Spawn(GameSceneConfig.instance.reviveFx, controller.gameObject.transform);
        controller.animator.updateMode = AnimatorUpdateMode.UnscaledTime;

        controller.animator.SetTrigger(AnimationName.REVIVE);
        //Debug.Log("Revive");
      
        yield return new WaitForSecondsRealtime(1.9f);
        this.postEvent(EventID.PLAYER_REVIE_DONE);

        //if(revivalShockWave != null)
        //ObjectPool.Spawn(revivalShockWave, controller.centerPoint.position);

        yield return new WaitForSecondsRealtime(0.1f);
       
        controller.softBody.ChangeLayerMask(ConstantValueManager.instance.playerLayer);
        
        controller.animator.updateMode = AnimatorUpdateMode.Normal;
        foreach (var bt in controller.componentManager.entity.skillContainer.BehaviorTrees)
        {

            bt.enabled = true;
        }
        var tempEntity = CleanUpBufferManager.instance.CreateTempEntity();
        
        CleanUpBufferManager.instance.AddReactiveComponent(() => { tempEntity.AddHeal(controller.componentManager.entity.health.MaxHP, controller.componentManager.entity); }, () => { tempEntity.Destroy(); });
        controller.componentManager.entity.isAttackDone = false;
        controller.ChangeState(controller.idleState);
        InputManager.instance.uiGameplay.SetActive(true);
        if (controller.componentManager.entity.hasImmune)
            controller.componentManager.entity.ReplaceImmune(Faction.PLAYER, 3f);
        else
            controller.componentManager.entity.AddImmune(Faction.PLAYER, 3f);
        controller.bodyEffect.immuneToForce = true;
        controller.immuneKnock = true;
        EffectRequestManager.RequestEffect(GameSceneConfig.instance.repelEffect, controller.componentManager.entity.centerPoint.centerPoint, Vector2.zero, 3f);
        
        revive = false;
        if (CaculateRegeToEnemy.instance != null)
        {
            if (CaculateRegeToEnemy.instance.listPet != null)
            {
                foreach (PetController pet in CaculateRegeToEnemy.instance.listPet)
                    pet.canFire = true;
            }
        }
    }
    
    public override TaskStatus OnInputSkill(int skillId)
    {
        
            return TaskStatus.Failure;
    }

    public override TaskStatus OnInputAttack()
    {
        return TaskStatus.Failure;
    }

    public override TaskStatus OnInputChangeFacing()
    {
        return TaskStatus.Failure;
    }

    public override TaskStatus OnKnockDown()
    {
        return TaskStatus.Failure;
    }

    public override TaskStatus OnInputIdle()
    {       
        return TaskStatus.Failure;
    }

    public override TaskStatus OnInputMove()
    {
       
        return TaskStatus.Failure;
    }
}
 
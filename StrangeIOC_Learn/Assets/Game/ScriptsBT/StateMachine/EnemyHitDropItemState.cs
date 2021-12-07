using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Hit Drop Item State", menuName = "State/Enemy/enemy hit drop")]
public class EnemyHitDropItemState : State
{
    float knockTime = 0.6f;
    GameObject coin;
    ParticleSystem ps;
    ParticleSystem.MainModule main;
    public bool moveWhileGetHit;
    public bool dropGem;
    public override void EnterState()
    {
        base.EnterState();
        knockTime = controller.beHitTime;

        if(!moveWhileGetHit)
        controller.animator.SetBool(AnimationName.MOVE, false); // if move anim is in play
        else
        {
            controller.componentManager.entity.isStopMove = false;
        }
        if (controller.componentManager.entity.expOnDeath.goldDropEachHit != 0)
            DropGold(controller.componentManager.entity.expOnDeath.goldDropEachHit);
        else if (dropGem)
            DropGem(1);
        int randomHitAnim = Random.Range(0, controller.CountAnimHit);
        if (controller.CountAnimHit == 2)
            if (randomHitAnim == 0)
                controller.animator.SetTrigger(AnimationName.HIT);
            else
                controller.animator.SetTrigger(AnimationName.HIT2);
        else
            controller.animator.SetTrigger(AnimationName.HIT);
        //Play sound Hit

        if (controller.componentManager.entity.health.HP.Value <= 0)
        {          
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
      

    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        knockTime -= Time.deltaTime;
        if (knockTime <= 0f && controller.componentManager.entity.health.HP.Value > 0)
        {
            controller.ChangeState(controller.idleState);
            return;
        }
        else if (controller.componentManager.entity.health.HP.Value <= 0)
        {
            if (controller.behaviorTree.enabled == true)
                controller.behaviorTree.enabled = false;
            CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isSkillComplete = false; } });
            if (controller.componentManager.entity.isEnabled == true)
            {
                controller.componentManager.entity.isAttackDone = true;
                CleanUpBufferManager.instance.AddReactiveComponent(() => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = true; } }, () => { if (controller.componentManager.entity.isEnabled) { controller.componentManager.entity.isAttackComplete = false; controller.componentManager.entity.isSkillComplete = false; } });
            }
            controller.ChangeState(controller.dieState);
            return;
        }
    }
    public override void ExitState()
    {
        base.ExitState();
       
    }
    public override TaskStatus OnHit(bool isBlock)
    {

        EnterState();
        return TaskStatus.Success;
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

    void DropGold(int amount)
    {
        ResourceInGameManager.instance.totalGoldCollect += amount;
        coin = ObjectPool.Spawn(GameSceneConfig.instance.coinPrefab, controller.componentManager.entity.centerPoint.centerPoint.position, GameSceneConfig.instance.coinPrefab.transform.rotation);
        ps = coin.GetComponent<ParticleSystem>();
        main = ps.main;
        ps.Clear();
        if (amount > 200)
            main.maxParticles = amount / 200;
        else
            main.maxParticles = 2;
        ps.Play();
    }

    void DropGem(int amount)
    {
        ResourceInGameManager.instance.totalGemCollect += amount;
        coin = ObjectPool.Spawn(GameSceneConfig.instance.gemPrefab, controller.componentManager.entity.centerPoint.centerPoint.position, GameSceneConfig.instance.coinPrefab.transform.rotation);
        ps = coin.GetComponent<ParticleSystem>();
        main = ps.main;
        ps.Clear();
        if (amount > 200)
            main.maxParticles = amount / 200;
        else
            main.maxParticles = 2;
        ps.Play();
    }
}

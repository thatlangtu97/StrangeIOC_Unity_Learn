using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;


public class TakeDamageSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    GameEntity entity;
    GameEntity entityTakeDamage;
    //int damageToTake;
    //int totalDamageTaken = 0;
    public TakeDamageSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.TakeDamage);
    }
    protected override bool Filter(GameEntity entity)
    {
        return entity.hastakeDamageComponent;
    }
    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {

            entity = e.takeDamageComponent.entity;
            entityTakeDamage = e.takeDamageComponent.entityTakeDamage;

            entity.stateMachineContainer.stateMachine.componentManager.properties.Heal -= e.takeDamageComponent.damage;
            if (entity.stateMachineContainer.stateMachine.componentManager.properties.Heal <= 0)
            {
                entity.stateMachineContainer.stateMachine.ChangeState(entity.stateMachineContainer.stateMachine.dieState);
            }

        }
        /*   
        foreach (GameEntity e in entities)
        {
            victim = e.takeDamage.victim;
            damageToTake = e.takeDamage.damage;
            totalDamageTaken = 0;

            if(victim.health.HP.Value > 0 && !isShield)
            {
                if(!victim.hasImmune)
                {
                    //SpawnDamageText(e, ref typeText);  
                    CalculateDamageBytype(e);
                    if (victim.hasBossHpBar)
                    {
                        InputManager.instance.bossHpBar.ChangeHp(totalDamageTaken, false);
                    }
                    damageToTake = Mathf.CeilToInt(totalDamageTaken);
                    
                    if (victim.isPlayerFlag)
                    {
                        if (e.takeDamage.source.hasStateMachineContainer)
                        {
                            int index = e.takeDamage.source.stateMachineContainer.stateMachine.index;

                            if (FireBaseProgressData.Instance != null)
                            {
                                if (FireBaseProgressData.Instance.arrindexDamageEnemy.ContainsKey(index))
                                    FireBaseProgressData.Instance.arrindexDamageEnemy[index] += totalDamageTaken;
                                else
                                {
                                    FireBaseProgressData.Instance.arrindexDamageEnemy.Add(index, totalDamageTaken);
                                }
                            }
                        }


                        if (victim.health.HP.Value <= 0)
                        {
                            CleanUpBufferManager.instance.AddReactiveComponent(() => { if (victim.isEnabled) { victim.isSkillComplete = true; } }, () => { if (victim.isEnabled) { victim.isSkillComplete = false; } });
                            if (victim.isEnabled == true)
                            {
                                victim.isAttackDone = true;
                                CleanUpBufferManager.instance.AddReactiveComponent(() => { if (victim.isEnabled) { victim.isAttackComplete = true; } }, () => { if (victim.isEnabled) { victim.isAttackComplete = false; victim.isSkillComplete = false; } });
                            }
                            victim.stateMachineContainer.stateMachine.ChangeState(victim.stateMachineContainer.stateMachine.dieState);
                        }
                    }
                    victim.health.HP.Value = Mathf.Clamp(victim.health.HP.Value - totalDamageTaken, 0, victim.health.MaxHP);
                    if (e.takeDamage.attacktype != AttackType.Subsequent && e.takeDamage.source.isPlayerFlag)
                    {
                        onAttackHitEvent.entity = victim;
                        e.takeDamage.source.postEvent(EventID.GAME_EVENT, onAttackHitEvent);
                        e.takeDamage.source.skillUIButton.comboUI.ResetElapseTime(); 
                        e.takeDamage.source.skillUIButton.comboUI.curCombo++;
                        e.takeDamage.source.skillUIButton.comboUI.countAllCombo++;
                    }
                    
                   
                    
                    //else
                    //{
                    //    e.takeDamage.source.postEvent(EventID.GAME_EVENT, new EventPassiveContainer(EventPassive.ON_ATTACK_HIT, null));
                    //}
                    //loop                  
                  
                    if (e.takeDamage.source.attackPower.identity == IdentityType.SpiritProjectile)// if is spirit projectile then not knocking
                    {
                        onAttackHitEvent.entity = victim;
                        e.takeDamage.source.postEvent(EventID.GAME_EVENT, onAttackHitEvent);
                        victim.stateMachineContainer.stateMachine.OnHit(false, true);
                    }
                    else if (victim != null && victim.hasStateMachineContainer)
                    {

                        victim.stateMachineContainer.stateMachine.OnHit(false, false);
                    }
                }
                else
                {
                    GameEntity newE = Contexts.sharedInstance.game.CreateEntity();
                    CleanUpBufferManager.instance.AddReactiveComponent(() => { newE.AddFlyingText(TextType.Block, "Block", victim.anchorHolder.topAnchor.position); }, () => { newE.Destroy(); });
                }
            }         
        }
        */
    }
    /*




    int Crittical(int damage, float CRTChance, float CRTMulti)
    {      
        if (CRTChance == 0)
        return damage;
        int critchance = Random.Range(0, 100);
            
        if (critchance < CRTChance)
        {
            damage = (int)(damage * CRTMulti );
            isCrit = true;                
            return damage;
        }
        isCrit = false;
        
        return damage;
    }
    bool IsBlock(GameEntity e)
    {
        if (e.hasBlockPhysicDamage)
        {
            int rd = Random.Range(0, 100);
            if (rd < e.blockPhysicDamage.blockChance)
            {
                GameEntity txe = Contexts.sharedInstance.game.CreateEntity();
                CleanUpBufferManager.instance.AddReactiveComponent(() => { txe.AddFlyingText(TextType.Block, "Block", e.anchorHolder.topAnchor.position); }, () => { txe.Destroy(); });
                return true;
            }
            else
                return false;
        }else if (e.stateMachineContainer.stateMachine.ShieldHP > 0)
        {
            GameEntity txe = Contexts.sharedInstance.game.CreateEntity();
            CleanUpBufferManager.instance.AddReactiveComponent(() => { txe.AddFlyingText(TextType.Block, "Block", e.anchorHolder.topAnchor.position); }, () => { txe.Destroy(); });
            return true;
        }
        else
            return false;
    }

    //if crit chance of a combo is lagrer base crit chance then take that combo crit chance;
    int CalculateCritDamage(GameEntity takeDamage, int damageToTake)
    {
        if (takeDamage.takeDamage.type.Count <=0)
            return damageToTake;
        for (int i = 0; i < takeDamage.takeDamage.type.Count; i++)
        {
            if (takeDamage.takeDamage.type[i] == DamageType.Physic)
            {
                if (!takeDamage.takeDamage.source.hasCritEffect)
                    return damageToTake;

                if(takeDamage.takeDamage.damage != 0) // damage take from skill
                {
                    damageToTake = (Crittical(damageToTake, takeDamage.takeDamage.source.critEffect.skillChance, takeDamage.takeDamage.source.critEffect.skillMultiplier));
                    
                }
                else
                {
                    
                    if (takeDamage.takeDamage.critChance > takeDamage.takeDamage.source.critEffect.chance)
                        damageToTake = (Crittical(damageToTake, takeDamage.takeDamage.critChance, takeDamage.takeDamage.source.critEffect.multiplier));
                    else
                        damageToTake = (Crittical(damageToTake, takeDamage.takeDamage.source.critEffect.chance, takeDamage.takeDamage.source.critEffect.multiplier));
                }          
                return damageToTake;           
            }
        }
        return damageToTake;
    }

    void CalculateDamageBytype(GameEntity takeDamage)
    {
       
        for (int i = 0; i < takeDamage.takeDamage.type.Count; i++)
        {
            switch (takeDamage.takeDamage.type[i])
            {
                case DamageType.Physic:
                    typeText = TextType.PhysicDamage;
                    if (IsBlock(takeDamage.takeDamage.victim))
                    {
                        // neu co anim block thi kich hoat
                        if (takeDamage != null && takeDamage.hasStateMachineContainer)
                        {

                            takeDamage.stateMachineContainer.stateMachine.OnHit(true, false);
                        }
                        continue; 
                    }             
                    break;
                case DamageType.Fire:
                    typeText = TextType.BurnDamage;
                    break;
                case DamageType.Ice:
                    typeText = TextType.IceDamage;
                    break;
                case DamageType.Lighting:
                    typeText = TextType.LigtingDamage;
                    break;
            }

            damageToTake = (int)takeDamage.takeDamage.damageByType[i];

            int test = damageToTake;
            totalDamageTaken += damageToTake;
            TextType newTypeText = typeText;
            if (test == 0)
                return;

            if (takeDamage.takeDamage.type[i] == DamageType.Physic)
            {
                damageToTake = CalculateCritDamage(takeDamage, damageToTake);
                test = damageToTake;
                if (isCrit)
                {
                    GameEntity txe = Contexts.sharedInstance.game.CreateEntity();
                    CleanUpBufferManager.instance.AddReactiveComponent(() => { txe.AddFlyingText(TextType.Crittical, test.ToString(), victim.anchorHolder.topAnchor.position); }, () => { txe.Destroy(); });
                }
                else
                {
                    GameEntity txe = Contexts.sharedInstance.game.CreateEntity();
                    CleanUpBufferManager.instance.AddReactiveComponent(() => { txe.AddFlyingText(newTypeText, test.ToString(), victim.anchorHolder.topAnchor.position); }, () => { txe.Destroy(); });
                }
            }
            else
            {
                GameEntity txe = Contexts.sharedInstance.game.CreateEntity();
                CleanUpBufferManager.instance.AddReactiveComponent(() => { txe.AddFlyingText(newTypeText, test.ToString(), victim.anchorHolder.topAnchor.position); }, () => { txe.Destroy(); });
            }
          

            //if (isCrit)
            //{
            //    GameEntity txe = Contexts.sharedInstance.game.CreateEntity();
            //    CleanUpBufferManager.instance.AddReactiveComponent(() => { txe.AddFlyingText(TextType.Crittical, test.ToString(), victim.anchorHolder.topAnchor.position); }, () => { txe.Destroy(); });              
            //}
            //else
            //{
            //    GameEntity txe = Contexts.sharedInstance.game.CreateEntity();
            //    CleanUpBufferManager.instance.AddReactiveComponent(() => { txe.AddFlyingText(newTypeText, test.ToString(), victim.anchorHolder.topAnchor.position); }, () => { txe.Destroy(); });
            //    //Debug.Log("Entity: " + txe.creationIndex);
            //    //Debug.Log("Text: " + takeDamage.takeDamage.damageByType[i]);
            //}
           
        }

       
    }

    bool ShieldTakeDamage(GameEntity takeDamge)
    {
        if (takeDamge.takeDamage.victim.stateMachineContainer.stateMachine.ShieldHP > 0)
        {        
            for (int i = 0; i < takeDamge.takeDamage.type.Count; i++)
            {
                if (takeDamge.takeDamage.type[i] == DamageType.Physic)
                {
                    GameEntity newE = Contexts.sharedInstance.game.CreateEntity();
                    CleanUpBufferManager.instance.AddReactiveComponent(() => { newE.AddFlyingText(TextType.Block, "Block", takeDamge.takeDamage.victim.anchorHolder.topAnchor.position); }, () => { newE.Destroy(); });
                    takeDamge.takeDamage.victim.stateMachineContainer.stateMachine.ShieldHP--;
                    takeDamge.takeDamage.victim.stateMachineContainer.stateMachine.OnHit(false, false);
                    return true;
                }  
            }
        }
        return false;
    }
    */
}

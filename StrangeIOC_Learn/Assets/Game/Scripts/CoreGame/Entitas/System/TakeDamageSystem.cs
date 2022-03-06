using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;


public class TakeDamageSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    GameEntity targetEnemy;
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
        return entity.hasTakeDamage;
    }
    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity myEntity in entities)
        {
            targetEnemy = myEntity.takeDamage.targetEnemy;
            if (targetEnemy == null)
            {
                //targetEnemy.Destroy();
                return;
            }
            StateMachineController stateMachine = targetEnemy.stateMachineContainer.stateMachine;
            if (!stateMachine)
            {
                myEntity.Destroy(); 
                return;
            }
            if(!stateMachine.componentManager.HasImmune(Immune.BLOCK))
                stateMachine.componentManager.heal -= (int)(myEntity.takeDamage.damageInfoSend.damageProperties.baseDamage * myEntity.takeDamage.damageInfoSend.damageInfoEvent.damageScale);

            if (stateMachine.componentManager.heal <= 0)
            {
                stateMachine.ChangeState(NameState.DieState);
            }
            else
            {
                switch (myEntity.takeDamage.damageInfoSend.damageInfoEvent.powerCollider) {
                    //case PowerCollider.Node:
                    //    entityEnemy.stateMachineContainer.stateMachine.InvokeAction(e.takeDamage.action);
                    //    break;
                    case PowerCollider.Small:
                    case PowerCollider.Medium:
                    case PowerCollider.Heavy:
                        stateMachine.OnHit(myEntity.takeDamage.damageInfoSend.action);
                        break;
                    case PowerCollider.KnockDown:
                        stateMachine.OnKnockDown(myEntity.takeDamage.damageInfoSend.action); 
                        break;
                }
                
            }
            myEntity.Destroy();
        }
    }
}

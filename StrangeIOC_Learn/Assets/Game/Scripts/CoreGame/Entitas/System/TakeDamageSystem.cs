using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;


public class TakeDamageSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    GameEntity entityEnemy;
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
        foreach (GameEntity e in entities)
        {
            entityEnemy = e.takeDamage.entityEnemy;
            
            StateMachineController stateMachine = entityEnemy.stateMachineContainer.stateMachine;
            if (!stateMachine)
            {
                e.Destroy(); 
                return;
            }
            if(!stateMachine.componentManager.HasImmune(Immune.BLOCK))
                stateMachine.componentManager.heal -= e.takeDamage.damage;

            if (stateMachine.componentManager.heal <= 0)
            {
                stateMachine.ChangeState(NameState.DieState);
            }
            else
            {
                switch (e.takeDamage.powerCollider) {
                    //case PowerCollider.Node:
                    //    entityEnemy.stateMachineContainer.stateMachine.InvokeAction(e.takeDamage.action);
                    //    break;
                    case PowerCollider.Small:
                    case PowerCollider.Medium:
                    case PowerCollider.Heavy:
                        stateMachine.OnHit(e.takeDamage.action);
                        break;
                    case PowerCollider.KnockDown:
                        stateMachine.OnKnockDown(e.takeDamage.action);
                        break;
                }
                
            }
            e.Destroy();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;


public class TakeDamageSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    //GameEntity entity;
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
            //entity = e.takeDamageComponent.entity;
            entityEnemy = e.takeDamage.entityEnemy;
            entityEnemy.stateMachineContainer.stateMachine.componentManager.properties.Heal -= e.takeDamage.damage;

            if (entityEnemy.stateMachineContainer.stateMachine.componentManager.properties.Heal <= 0)
            {
                entityEnemy.stateMachineContainer.stateMachine.ChangeState(entityEnemy.stateMachineContainer.stateMachine.dieState);
            }
            else
            {
                entityEnemy.stateMachineContainer.stateMachine.currentState.OnHit();
            }
            e.Destroy();
        }
    }
}

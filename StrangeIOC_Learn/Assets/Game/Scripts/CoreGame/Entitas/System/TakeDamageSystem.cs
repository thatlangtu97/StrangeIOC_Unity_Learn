﻿using System.Collections;
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
        return entity.hastakeDamageComponent;
    }
    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            //entity = e.takeDamageComponent.entity;
            entityEnemy = e.takeDamageComponent.entityEnemy;
            entityEnemy.stateMachineContainer.stateMachine.componentManager.properties.Heal -= e.takeDamageComponent.damage;

            if (entityEnemy.stateMachineContainer.stateMachine.componentManager.properties.Heal <= 0)
            {
                entityEnemy.stateMachineContainer.stateMachine.ChangeState(entityEnemy.stateMachineContainer.stateMachine.dieState);
            }
            e.Destroy();
        }
    }
}

using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineUpdateSystem : IExecuteSystem
{
    public readonly Contexts context;
    readonly IGroup<GameEntity> entities;
    //SimpleMoveJobSystem simpleMoveJobSystem;
    public StateMachineUpdateSystem(Contexts _contexts)
    {
        context = _contexts;
        entities = context.game.GetGroup(GameMatcher.AllOf(GameMatcher.StateMachineContainer));
        //simpleMoveJobSystem = new SimpleMoveJobSystem(context.game, 4);
    }
    public void Execute()
    {
        foreach (var e in entities.GetEntities())
        {
            e.stateMachineContainer.stateMachine.UpdateState();
        }
        //simpleMoveJobSystem.Execute();
    }
    //public class SimpleMoveJobSystem : JobSystem<GameEntity>
    //{
    //    public SimpleMoveJobSystem(GameContext context, int threads) :
    //    base(context.GetGroup(GameMatcher.AllOf(GameMatcher.StateMachineContainer)), threads)
    //    {
    //    }

    //    protected override void Execute(GameEntity entity)
    //    {
    //        entity.stateMachineContainer.stateMachine.UpdateState();
    //    }
    //}
}

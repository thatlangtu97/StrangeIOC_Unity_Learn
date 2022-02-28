using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineUpdateSystem : IExecuteSystem
{
    public readonly Contexts context;
    readonly IGroup<GameEntity> entities;
    //UpdateMecanimJobSystem updateMecanimJobSystem;
    public StateMachineUpdateSystem(Contexts _contexts)
    {
        context = _contexts;
        entities = context.game.GetGroup(GameMatcher.AllOf(GameMatcher.StateMachineContainer));
        //updateMecanimJobSystem = new UpdateMecanimJobSystem(context.game, 1);
    }
    public void Execute()
    {
        foreach (var e in entities.GetEntities())
        {
            if (!e.stateMachineContainer.stateMachine)
            {
                e.Destroy();
                continue;
            }

            e.stateMachineContainer.stateMachine.UpdateState();
            //e.stateMachineContainer.stateMachine.componentManager.UpdateMecanim();
        }
        //updateMecanimJobSystem.Execute();
    }
}

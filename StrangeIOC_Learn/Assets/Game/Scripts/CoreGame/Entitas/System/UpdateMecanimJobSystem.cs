using Entitas;
public class UpdateMecanimJobSystem : JobSystem<GameEntity>
{
    public UpdateMecanimJobSystem(GameContext context, int threads) :
        base(context.GetGroup(GameMatcher.AllOf(GameMatcher.StateMachineContainer)), threads)
    {
    }

    protected override void Execute(GameEntity entity)
    {
        //entity.stateMachineContainer.stateMachine.componentManager.UpdateMecanim();
    }

}

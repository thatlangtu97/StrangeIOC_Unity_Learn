using Entitas;
[Game]
public class PlayerFlagComponent : IComponent
{
}
public partial class GameContext
{
    public GameEntity playerFlagEntity { get { return GetGroup(GameMatcher.PlayerFlag).GetSingleEntity(); } }
}
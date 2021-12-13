using Entitas;
[Game]
public class TakeDamageComponent : IComponent
{
    public GameEntity entity;
    public GameEntity entityEnemy;
    public int damage;
    public TakeDamageComponent()
    {
    }
    public TakeDamageComponent(GameEntity e)
    {
        entity = e;
    }
}

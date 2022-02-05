using Entitas;
using System;

[Game]
public class TakeDamageComponent : IComponent
{
    public GameEntity entity;
    public GameEntity entityEnemy;
    public int damage;
    public PowerCollider powerCollider;
    public Action action;
    public TakeDamageComponent()
    {
    }
    public TakeDamageComponent(GameEntity entity, GameEntity entityEnemy, int damage , PowerCollider powerCollider, Action action=null)
    {
        this.entity = entity;
        this.entityEnemy = entityEnemy;
        this.damage = damage;
        this.action = action;
        this.powerCollider = powerCollider;
    }
    public TakeDamageComponent(GameEntity e)
    {
        entity = e;
    }
}

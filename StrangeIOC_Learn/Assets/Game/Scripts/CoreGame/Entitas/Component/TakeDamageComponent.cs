using Entitas;
using System;

[Game]
public class TakeDamageComponent : IComponent
{
    public GameEntity myEntity;
    public GameEntity targetEnemy;
    public DamageInfoSend damageInfoSend;
    public TakeDamageComponent(){}
    public TakeDamageComponent(GameEntity myEntity, GameEntity targetEnemy,DamageInfoSend damageInfoSend)
    {
        this.myEntity = myEntity;
        this.targetEnemy = targetEnemy;
        this.damageInfoSend = damageInfoSend;
    }
}

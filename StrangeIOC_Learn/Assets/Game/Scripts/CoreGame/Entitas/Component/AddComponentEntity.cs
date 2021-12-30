using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameEntity
{
    /*
    public void AddTakeDamageComponent(int damage, GameEntity entity, GameEntity entityEnemy)
    {
        var index = GameComponentsLookup.TakeDamage;
        var componentPool = GetComponentPool(index);
        var component = (componentPool.Count > 0
                            ? componentPool.Pop()
                            : CreateComponent(index, typeof(TakeDamageComponent))) as TakeDamageComponent;

        //var component = (TakeDamageComponent)CreateComponent(index, typeof(TakeDamageComponent));
        component.damage = damage;
        component.entity = entity;
        component.entityEnemy = entityEnemy;
        AddComponent(index, component);
    }
    */
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDmgManager
{
    public static void DealDamage(Collider2D target, GameEntity myEntity, DamageInfoSend damageInfoSend)
    {
        ComponentManager enemyComponent = target.GetComponent<ComponentManager>();      
        GameEntity targetEntity = enemyComponent.entity;
        AddReactiveComponent(myEntity, targetEntity, damageInfoSend);
    }
    static void AddReactiveComponent( GameEntity myEntity, GameEntity targetEntity, DamageInfoSend damageInfoSend)
    {
        GameEntity takeDamageComponent = Contexts.sharedInstance.game.CreateEntity();
        takeDamageComponent.AddTakeDamage(myEntity, targetEntity, damageInfoSend);
    }
}
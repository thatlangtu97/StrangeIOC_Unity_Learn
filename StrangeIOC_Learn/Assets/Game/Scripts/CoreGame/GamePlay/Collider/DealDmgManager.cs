using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDmgManager
{
    public static void DealDamage(Collider2D other, GameEntity entity)
    {
        ComponentManager enemyComponent = other.GetComponent<ComponentManager>();      
        GameEntity entityEnemy = enemyComponent.entity;
        int damage=10;
        AddReactiveComponent(damage, entity, enemyComponent.entity);
    }
    public static void DealDamageProjectile(Collider2D other, GameEntity entity)
    {
        ProjectileComponent enemyComponent = other.GetComponent<ProjectileComponent>();
        GameEntity entityEnemy = enemyComponent.entity;
        int damage = 5;
        AddReactiveComponent(damage, entity, enemyComponent.entity);
    }
    static void AddReactiveComponent(int damage, GameEntity entity, GameEntity entityEnemy)
    {
        GameEntity takeDamageComponent;
        takeDamageComponent = Contexts.sharedInstance.game.CreateEntity();
        takeDamageComponent.AddTakeDamage(entity, entityEnemy, damage);
    }
}
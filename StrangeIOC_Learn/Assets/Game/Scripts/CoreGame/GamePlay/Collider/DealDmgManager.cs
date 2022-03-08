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

public class DamageTextManager
{
    public static string hexColorNormal="#9c9c9c9c";
    public static void AddReactiveComponent( DamageTextType newDamageTextType,string newValue, Vector3 newPosition)
    {
        GameEntity damageText = Contexts.sharedInstance.game.CreateEntity();
        damageText.AddDamageText(newDamageTextType, newValue, newPosition);
    }

    public static Color GetColor(DamageTextType newDamageTextType)
    {
        switch (newDamageTextType)
        {
            case DamageTextType.Normal:
//                ColorUtility.TryParseHtmlString(hexColorNormal, out Color colorNormal);
//                return colorNormal;
                return Color.white;
            default: 
                return Color.black;
        }

        

    }
}
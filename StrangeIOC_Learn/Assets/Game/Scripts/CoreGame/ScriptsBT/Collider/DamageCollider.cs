using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class DamageCollider : MonoBehaviour
{
    //public GameEntity entity;
    public ComponentManager component;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Take Damage");
        DealDmgManager.DealDamage(other, component.entity);

    }
}

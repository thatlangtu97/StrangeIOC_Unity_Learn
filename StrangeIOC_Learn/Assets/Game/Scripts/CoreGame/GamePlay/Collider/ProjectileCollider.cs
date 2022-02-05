using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class ProjectileCollider : MonoBehaviour
{
    public ProjectileComponent component;
    [FoldoutGroup("CAST BOX COLLIDER")]
    //[BoxGroup("Cast Collider")]
    public PowerCollider powerCollider;

    [FoldoutGroup("CAST BOX COLLIDER")]
    //[BoxGroup("Cast Collider")]
    public Vector2 force;
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //DealDmgManager.DealDamage(other, component.entity, PowerCollider.Node);
        //component.colliderProjectile.enabled = false;
        
        Action action = delegate
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, other.GetComponent<Rigidbody2D>().velocity.y);
            other.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(force.x * transform.localScale.x, force.y), other.transform.position);
        };
        DealDmgManager.DealDamage(other, component.entity, powerCollider, action);
    }
}

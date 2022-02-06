using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class ProjectileCollider : MonoBehaviour
{
    public ProjectileComponent component;
    [FoldoutGroup("EVENT COLLIDER")]
    public PowerCollider powerCollider;

    [FoldoutGroup("EVENT COLLIDER")]
    public Vector2 force;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        Action action = delegate
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, other.GetComponent<Rigidbody2D>().velocity.y);
            other.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(force.x * transform.localScale.x, force.y), other.transform.position);
        };
        DealDmgManager.DealDamage(other, component.entity, powerCollider, action);
    }
}

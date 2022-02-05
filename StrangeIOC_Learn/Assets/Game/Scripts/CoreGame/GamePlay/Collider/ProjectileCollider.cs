using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    public ProjectileComponent component;
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        DealDmgManager.DealDamage(other, component.entity, PowerCollider.Node);
        //component.colliderProjectile.enabled = false;
    }
}

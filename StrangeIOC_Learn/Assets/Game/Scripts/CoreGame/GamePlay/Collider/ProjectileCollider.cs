using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEngine.Serialization;

public class ProjectileCollider : MonoBehaviour
{
    public DamageInfoEvent damageInfoEvent;
    public DamageProperties damageProperties;
    public ProjectileComponent component;
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        void Action()
        {
            other.GetComponent<Rigidbody2D>().AddForceAtPosition(damageInfoEvent.forcePower * transform.localScale.x, other.transform.position);
        }

        Vector2 direction = (other.transform.position - transform.position).normalized;
        
        DamageInfoSend damageInfoSend = new DamageInfoSend(damageInfoEvent, damageProperties, Action);
        DealDmgManager.DealDamage(other, component.entity, damageInfoSend);
        ObjectPool.Recycle(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DamageInfoEvent
{
    public float damageScale;
    public PowerCollider powerCollider;
    public Vector2 forcePower;

    public DamageInfoEvent(DamageInfoEvent clone)
    {
        this.damageScale = clone.damageScale;
        this.powerCollider = clone.powerCollider;
        this.forcePower = clone.forcePower;
    }
}

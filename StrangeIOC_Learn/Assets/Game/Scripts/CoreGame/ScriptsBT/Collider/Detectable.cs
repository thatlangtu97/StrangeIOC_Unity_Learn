using System;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Detectable : MonoBehaviour
{
    public ComponentManager componentManager;
    public bool immuneToEffects;
    public bool immuneToFreeze;
    public bool immuneToInstantKill;
    public bool immuneToForce;
    public bool immune;
    public bool isCancleSkill;
    [Header("Properties Collider")]
    public bool IsTrigger;
    public Vector2 OffsetKnockDown;
    public Vector2 SizeKnockDown;
    Vector2 OffsetStand,SizeStand;
    public CapsuleCollider2D StandCollider;
    public BoxCollider2D StandColliderBox;
    private void Awake()
    {
        if (StandCollider != null)
        {
            OffsetStand = StandCollider.offset;
            SizeStand = StandCollider.size;

        }
        if (StandColliderBox != null)
        {
            OffsetStand = StandColliderBox.offset;
            SizeStand = StandColliderBox.size;
        }

    }
    public void DisableAllImmune()
    {
        immuneToEffects = false;
        immuneToFreeze = false;
        immuneToForce = false;
        immune = false;
        immuneToInstantKill = false;
    }
}

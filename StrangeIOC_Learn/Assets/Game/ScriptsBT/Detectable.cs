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
    //private void Start()
    //{
    //    if (StandCollider != null )
    //    {
    //        StandCollider.enabled = true;


    //    }
    //}
    public void DisableAllImmune()
    {
        immuneToEffects = false;
        immuneToFreeze = false;
        immuneToForce = false;
        immune = false;
        immuneToInstantKill = false;
    }
    public void KnockDown(){
        if (StandCollider != null )
        {
           
            StandCollider.direction = CapsuleDirection2D.Horizontal;
            StandCollider.offset = OffsetKnockDown;
            StandCollider.size = SizeKnockDown;
        }
        if (StandColliderBox != null)
        {
            StandColliderBox.offset = OffsetKnockDown;
            StandColliderBox.size = SizeKnockDown;
        }
    }
    public void ExitKnockDown() {
        if (StandCollider != null )
        {
            StandCollider.direction = CapsuleDirection2D.Vertical;
            StandCollider.offset = OffsetStand;
            StandCollider.size = SizeStand;

        }
        if (StandColliderBox != null)
        {
            StandColliderBox.offset = OffsetStand;
            StandColliderBox.size = SizeStand;
        }
    }
}

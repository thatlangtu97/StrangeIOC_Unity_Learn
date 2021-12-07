using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastMasterThrowAxeTask : Action
{
    public SharedComponentManager component;
    public GameObject projectilePrefab;
    public LayerMask wallMask;
    public float throwDistance;
    public float flySpeed;
    public SharedTransform lauchPoint;
    bool isFinish;
    Vector2 hitresult;
    Vector2 result;
    
    public SharedSkillCheck sharedSkill;
    public AttackInfo info;
    public override void OnStart()
    {
        isFinish = false;
        GameObject bullet = ObjectPool.Spawn(projectilePrefab, lauchPoint.Value.position, Quaternion.identity);
        if (sharedSkill != null)
        {
            if (sharedSkill.Value != null)
            {
                if (sharedSkill.Value.dmgMultiple > 0f)
                {
                    info.damagescale = sharedSkill.Value.dmgMultiple;
                }

                if (sharedSkill.Value.knockBackForce > 0f)
                {
                    info.knockBackForce = sharedSkill.Value.knockBackForce;
                }

                if (sharedSkill.Value.critChance > 0)
                {
                    info.critChance = sharedSkill.Value.critChance;
                    info.critMultiplier = sharedSkill.Value.critMultiply;
                }
            }
        }
        var dmgCollider = bullet.GetComponent<DamageCollider>();
        dmgCollider.attackInfo = info;
        dmgCollider.source = component.Value.entity;
        //transform.right = dir;

        RaycastHit2D hit = Physics2D.Raycast((Vector2)lauchPoint.Value.position, transform.right, throwDistance, wallMask);
       
        hitresult = hit.point;

        if (hit.collider != null)
        {
            result = hit.point;
        }
        else
        {
            Ray2D r = new Ray2D();
            r.origin = (Vector2)lauchPoint.Value.position;
            r.direction = transform.right;
            result = r.GetPoint(throwDistance);
        }

        bullet.transform.DOMove(result, flySpeed).SetSpeedBased(true).OnComplete(() =>
        {
            bullet.transform.DOMove(lauchPoint.Value.position, flySpeed).SetSpeedBased().OnComplete(() =>
            {
                isFinish = true;
               ObjectPool.Recycle(bullet);
            });
        });
    }
    public override TaskStatus OnUpdate()
    {
        if(isFinish)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
    }
}

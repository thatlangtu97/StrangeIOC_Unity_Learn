using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Entitas.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
public class DealDamageInRadius : Action
{
    public SharedFloat radius;
    public AttackInfo attackInfo; // dung de modify hieu ung co san bang file config
    public SharedSkillCheck skillInfo;// dung de modify power scale bang file config
    public SharedComponentManager component;
    public List<EffectInfo> listEffect;
    public LayerMask whatIsEnemy;
    public SharedVector2 castPoint;
    public bool useCastPoint, useCastPointTransform;
    public SharedTransform castPointTf;
    public override void OnStart()
    {
        List<List<EffectInfo>> allEffect = new List<List<EffectInfo>>();
        allEffect.Add(listEffect);
        if(skillInfo.Value!=null)
        {
            if(skillInfo.Value.dmgMultiple>0f)
            {
                attackInfo.damagescale = skillInfo.Value.dmgMultiple;
            }
            if (skillInfo.Value.knockBackForce > 0f)
            {
                attackInfo.knockBackForce = skillInfo.Value.knockBackForce;
            }
            if (skillInfo.Value.critChance > 0)
            {
                attackInfo.critChance = skillInfo.Value.critChance;
                attackInfo.critMultiplier = skillInfo.Value.critMultiply;
                
            }
        }
        Collider2D[] cols;
        if (!useCastPoint)
        {
            cols = Physics2D.OverlapCircleAll(component.Value.entity.centerPoint.centerPoint.position, radius.Value, whatIsEnemy);
        }
        else
        {
            if(useCastPointTransform)
            {
                cols = Physics2D.OverlapCircleAll(castPointTf.Value.transform.position, radius.Value, whatIsEnemy);
            }
            else
            {
                cols = Physics2D.OverlapCircleAll(castPoint.Value, radius.Value, whatIsEnemy);
            }
           
        }

        if (cols != null)
        {
            foreach (var col in cols)
            {
                //Debug.Log("Deal dmg" + col.gameObject.name);
                //Debug.Log(col.gameObject.name);
                DealDmgManager.DealDamage(col, allEffect, attackInfo, component.Value.entity);

            }
        }
    }
}

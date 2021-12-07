using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
public class SpawnSpikeTask : Action
{
    public LayerMask whatIsEnemy;
    public LayerMask whatIsGround;
    public float radius;
    public SharedComponentManager componentManager;
    public GameObject spawnObjectPrefab;
    public float spawnOjectDuration;
    public AttackInfo atkInfo;
    public SharedSkillCheck sharedSkill;

    public override void OnStart()
    {

        GameObject spawnGameObject = ObjectPool.Spawn(spawnObjectPrefab, componentManager.Value.entity.centerPoint.centerPoint.position);
        componentManager.Value.gameObject.GetComponent<DependPooledObject>().dependList.Add(spawnGameObject);
        spawnGameObject.transform.SetParent(transform);
        DamageCollider dmgCol = spawnGameObject.GetComponentInChildren<DamageCollider>();
        dmgCol.allEffects.Add(componentManager.Value.entity.effectWeapon.permanentEffectList);
        dmgCol.allEffects.Add(componentManager.Value.entity.effectWeapon.temporaryEffectList);
        dmgCol.attackInfo = atkInfo;
        if (sharedSkill != null)
        {
            if (sharedSkill.Value != null)
            {
                if (sharedSkill.Value.dmgMultiple > 0f)
                {
                    atkInfo.damagescale = sharedSkill.Value.dmgMultiple;
                }
                if (sharedSkill.Value.knockBackForce > 0f)
                {
                    atkInfo.knockBackForce = sharedSkill.Value.knockBackForce;
                }
            }
        }
        dmgCol.source = componentManager.Value.entity;
        if (spawnOjectDuration > 0f)
        {
            ObjectPool.Recycle(spawnGameObject, spawnOjectDuration);
        }
        //Vector2 castPoint = componentManager.Value.entity.centerPoint.centerPoint.position;
        //var cols = Physics2D.OverlapCircleAll(castPoint, radius, whatIsEnemy);
        //foreach (var col in cols)
        //{
        //    Detectable detect = col.GetComponent<Detectable>();
        //    GameEntity enemy = detect.componentManager.entity;
        //    if (enemy != null)
        //    {
        //        Debug.Log("atk enemy");
        //        RaycastHit2D hit = Physics2D.Raycast(enemy.centerPoint.centerPoint.position, Vector2.down, 100f, whatIsGround);
        //        if(hit.collider!=null)
        //        {
        //            GameObject spawnGameObject = ObjectPool.Spawn(spawnObjectPrefab, hit.point);
        //            DamageCollider dmgCol = spawnGameObject.GetComponentInChildren<DamageCollider>();
        //            dmgCol.allEffects.Add(componentManager.Value.entity.effectWeapon.permanentEffectList);
        //            dmgCol.allEffects.Add(componentManager.Value.entity.effectWeapon.temporaryEffectList);
        //            dmgCol.attackInfo = atkInfo;
        //            dmgCol.source = componentManager.Value.entity;
        //            if (spawnOjectDuration > 0f)
        //            {
        //                ObjectPool.Recycle(spawnGameObject, spawnOjectDuration);
        //            }
        //            Debug.Log("hit");
        //        }
        //        else
        //        {
        //            Debug.Log("not hit");
        //        }
                

        //    }

        //}

    }

}


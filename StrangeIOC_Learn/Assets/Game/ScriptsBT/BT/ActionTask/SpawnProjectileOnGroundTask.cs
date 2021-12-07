using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas.Unity;

public class SpawnProjectileOnGroundTask : Action
{
    public LayerMask whatIsEnemy;
    public LayerMask whatIsGround;
    public float radius;
    public SharedComponentManager componentManager;
    public GameObject spawnObjectPrefab;
    public float spawnOjectDuration;
    public AttackInfo atkInfo;
    public SharedSkillCheck sharedSkill;
    public bool isNotUseGround;
    public Vector2 offtset;
    public bool addTarget;
    public override void OnStart()
    {
        Vector2 castPoint = componentManager.Value.entity.centerPoint.centerPoint.position;
        var cols = Physics2D.OverlapCircleAll(castPoint, radius, whatIsEnemy);
        Vector2 spawnPoint=Vector2.zero;
        if (cols.Length == 0)
        {
            
            //Debug.Log("khong raycas dc Player");
            spawnPoint = componentManager.Value.gameObject.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(spawnPoint, Vector2.down, 10f, whatIsGround);
            if (hit.collider != null)
            {
                spawnPoint = hit.point;
                //Debug.Log(hit.collider.gameObject.name);
            }
            spawnPoint += offtset;
            GameObject spawnGameObject = ObjectPool.Spawn(spawnObjectPrefab, Contexts.sharedInstance.game.playerFlagEntity.stateMachineContainer.stateMachine.gameObject.transform.position);
            foreach (DamageCollider dmgCol in spawnGameObject.GetComponentsInChildren<DamageCollider>(true))
                {
                    dmgCol.source = componentManager.Value.entity;
                    dmgCol.attackInfo = atkInfo;
                   
                }
                if (spawnOjectDuration > 0f)
                {
                    ObjectPool.Recycle(spawnGameObject, spawnOjectDuration);
                }

        }
        else
        //var plcols = Physics2D.OverlapCircleAll(castPoint, radius, whatIsEnemy);
        foreach (var col in cols)
        {
            
            Detectable detect = col.GetComponent<Detectable>();
            GameEntity enemy = detect.componentManager.entity;
            if (/*enemy != null*/true)
            {
                spawnPoint = Vector2.zero;
                if (isNotUseGround)
                {
                    spawnPoint = enemy.centerPoint.centerPoint.position;
                }
                else
                {
                    RaycastHit2D hit = Physics2D.Raycast(enemy.centerPoint.centerPoint.position, Vector2.down, 10f, whatIsGround);
                    if (hit.collider != null)
                    {
                        spawnPoint = hit.point;
                        //Debug.Log(hit.collider.gameObject.name);
                    }
                }

                spawnPoint += offtset;
                GameObject spawnGameObject = ObjectPool.Spawn(spawnObjectPrefab, spawnPoint);
               // GameEntity e = (GameEntity)spawnGameObject.GetEntityLink().entity;
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
                foreach (DamageCollider dmgCol in spawnGameObject.GetComponentsInChildren<DamageCollider>(true))
                {
                    dmgCol.source = componentManager.Value.entity;
                    dmgCol.attackInfo = atkInfo;
                   
                }
                if (spawnOjectDuration > 0f)
                {
                    ObjectPool.Recycle(spawnGameObject, spawnOjectDuration);
                }
                //Debug.Log("hit");
            }
            else
            {
                
            }
            

        }


    }

}


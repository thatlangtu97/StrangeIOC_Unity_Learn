using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SpawnProjectileAtPositionTask : Action
{

    public SharedComponentManager componentManager;
    public SharedGameObject spawnObjectPrefab;
    public float spawnOjectDuration;
    public SharedVector2 position;
    public Vector3 offset;
    public SharedSkillCheck sharedSkill;
    //public SharedBool isFacingByCharacter;
    public SharedBool setParent;
    //public SharedGameObject projectile;
    public bool addEnemyCount;
    public bool spawnAtTransform;
    public bool useCharacterDirection;
    Vector2 offSetConvert;
    public override void OnStart()
    {

        offSetConvert = offset;
        if (spawnAtTransform)
            position.Value = componentManager.Value.entity.centerPoint.centerPoint.position + offset;

        
        GameObject spawnGameObject/* = ObjectPool.Spawn(spawnObjectPrefab.Value, position.Value + (Vector2)offset)*/;
        
        if(useCharacterDirection)
        {
            if(componentManager.Value.entity.stateMachineContainer.stateMachine.characterDirection.isFaceRight)
               spawnGameObject = ObjectPool.Spawn(spawnObjectPrefab.Value, position.Value + offSetConvert, Quaternion.identity);
            else
            {
                offSetConvert.x *= -1;
                spawnGameObject = ObjectPool.Spawn(spawnObjectPrefab.Value, position.Value + offSetConvert, Quaternion.identity);
            }
               
        }
        else
            spawnGameObject = ObjectPool.Spawn(spawnObjectPrefab.Value, position.Value);

        if (setParent.Value)
        {
            spawnGameObject.transform.parent = componentManager.Value.transform;
        }

        foreach(DamageCollider dmgCol in spawnGameObject.GetComponentsInChildren<DamageCollider>())
        {
            
            if (sharedSkill.Value.dmgMultiple > 0f)
            {
                dmgCol.attackInfo.damagescale = sharedSkill.Value.dmgMultiple;
            }
            if (sharedSkill.Value.knockBackForce > 0f)
            {
                dmgCol.attackInfo.knockBackForce = sharedSkill.Value.knockBackForce;
            }
            componentManager.Value.entity.projectTileLauncher.attackInfo = componentManager.Value.entity.weapon.weaponController.attackInfo;
            dmgCol.attackInfo = componentManager.Value.entity.weapon.weaponController.attackInfo;
            dmgCol.source = componentManager.Value.entity;
        }
       
        if (spawnOjectDuration > 0f)
        {
            ObjectPool.Recycle(spawnGameObject, spawnOjectDuration);
        }

        if (addEnemyCount)
        {
            LevelCreator.instance.currEnemyCount++;
        }

        //projectile.Value = spawnGameObject;
    }
}
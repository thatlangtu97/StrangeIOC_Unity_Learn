using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileTriggerTask : Action
{
    public SharedComponentManager component;
    public bool aimByTarget;
    public bool isHorizontalProjectile;
    public SharedGameObject prefab;
    public override void OnStart()
    {
        if (component.Value.entity != null)
        {

            Transform point = component.Value.entity.projectTileLauncher.launchPoint;
            //Debug.Log("fire");
            Vector3 dir = new Vector3();
            if (!aimByTarget)
            {
                dir = component.Value.entity.projectTileLauncher.launchPoint.transform.right;
            }
            else
            {
                dir = component.Value.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint.position - point.position;
            }
            CleanUpBufferManager.instance.AddReactiveComponent(() => { component.Value.entity.AddSpawnProjectileCommand(component.Value.entity.projectTileLauncher.target, point.position, 1, isHorizontalProjectile, dir, true,prefab.Value); }, () => { component.Value.entity.RemoveSpawnProjectileCommand(); });
           
        }
    }
}

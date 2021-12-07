using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas.Unity;

public class PlayerShootsProjectileOnEvent : MonoBehaviour
{
    public int airAngle, groundAngle;
    public bool isHorizontalProjectile;
    public Transform bombDropPosition;
    public ComponentManager component;
    public GameObject dashAttackFx, airStompFx;
    public CharacterDirection direction;
    public int numberOfShot;
    public GameObject projectile;
    public Transform originalShootPoint;
    public Transform shootPoint;
    public Transform airShootPoint;
    [Tooltip("Aim with fixed target with Shoot function")]
    public int max_X_Range;
    public int min_X_Range;
    public List<int> numberOfProjectileCombo;
    public List<int> numberOfProjectileComboOnAir;
    public List<GameObject> projectileOnCombo;
    //Vector3 fixedTarget;


    public void CreateProjectile(GameEntity player)
    {
        if (projectileOnCombo.Count == 0)
            return;
        ObjectPool.CreatePool(projectileOnCombo[0], 10);
        List<GameObject> pro = new List<GameObject>();
        ObjectPool.GetPooled(projectileOnCombo[0], pro, false);
        foreach (GameObject o in pro)
        {

            o.GetComponent<ProjectileDamageCollider>().source = player;
        }

    }

    public void Shoot()
    {
        component.entity.projectTileLauncher.type = SpawnType.SpreadFromOnePoint;
        component.entity.projectTileLauncher.launchPoint.transform.position = shootPoint.position;
        component.entity.projectTileLauncher.launchPoint.eulerAngles = shootPoint.eulerAngles;

        /*
        fixedTarget.x = Random.Range(min_X_Range, max_X_Range);
      
        if (direction.isFaceRight)
        {
            component.entity.projectTileLauncher.target = shootPoint.position + fixedTarget;
            
        }
        else
        {
            component.entity.projectTileLauncher.target = shootPoint.position - fixedTarget;
        
        }
        */
        HitStopController.instance.HitStop(1, 0.1f);

        if (component.entity != null)
        {
                //Transform point = component.entity.projectTileLauncher.launchPoint;
                //Debug.Log("fire");
                Vector3 dir = new Vector3();
                dir = component.entity.projectTileLauncher.launchPoint.transform.right;
                CleanUpBufferManager.instance.AddReactiveComponent(() => { component.entity.AddSpawnProjectileCommand(component.entity.projectTileLauncher.target, shootPoint.position, numberOfShot, isHorizontalProjectile, dir, true, projectile); }, () => { component.entity.RemoveSpawnProjectileCommand(); });
        }
    }

    public void CastProjectile(GameObject projectileToCast, Vector2 castPosition, SpawnType type)
    {
        component.entity.projectTileLauncher.type = type;
        CleanUpBufferManager.instance.AddReactiveComponent(() => { component.entity.AddSpawnProjectileCommand(component.entity.projectTileLauncher.target, castPosition, 1, false,
            component.entity.projectTileLauncher.launchPoint.right, true, projectileToCast); }, () => { component.entity.RemoveSpawnProjectileCommand(); });
    }

    public void SetNumberOfProjectile(int combo, bool onAir)
    {
        if (!onAir)
        {
            numberOfShot = numberOfProjectileCombo[combo];
            projectile = projectileOnCombo[combo];
        }
            
        else
        {
            numberOfShot = numberOfProjectileComboOnAir[combo];   
            projectile = projectileOnCombo[combo];
        }
            
    }

    public void SetAirAngle()
    {
        shootPoint.eulerAngles = airShootPoint.eulerAngles;
    }

    public void SetGroundAngle()
    {
        shootPoint.eulerAngles = originalShootPoint.eulerAngles;
    }

    public void DropBomb(GameObject bomb)
    {
        component.entity.projectTileLauncher.type = SpawnType.Single;
        CleanUpBufferManager.instance.AddReactiveComponent(() => { component.entity.AddSpawnProjectileCommand(component.entity.projectTileLauncher.target, bombDropPosition.position, numberOfShot, isHorizontalProjectile, Vector2.zero, true, bomb); }, () => { component.entity.RemoveSpawnProjectileCommand(); });
    }

    public void LoadProjectileSkin(GameObject obj)
    {
        for (int i = 0; i < projectileOnCombo.Count-1; i++)
            projectileOnCombo[i] = obj;
    }
}

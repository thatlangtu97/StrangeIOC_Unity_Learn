using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Entitas.Unity;
using UnityEngine;

public class SwordRainSkill : Action
{
    public LayerMask whatIsGround;
    public SharedComponentManager componentManager;
    public GameObject spawnObjectPrefab;
    public float spawnOjectDuration;
    public AttackInfo atkInfo;
    public SharedSkillCheck sharedSkill;
    public float offtset;
    public int count;
    public float delayBetweenTwoCast;
    private bool isComplete;

    public override void OnStart()
    {
        isComplete = false;
        StartCoroutine((CastSwordRain()));
    }

    public override TaskStatus OnUpdate()
    {
        if (isComplete)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
    }

    IEnumerator CastSwordRain()
    {
        float direction = 1;
        var middleMapPos = LevelCreator.instance.map.leftAnchor.position.x +
                           (0.5f * (LevelCreator.instance.map.rightAnchor.position.x -
                                    LevelCreator.instance.map.leftAnchor.position.x));

        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                if (i == 0)
                {
                    j++; // lan xuat hien kiem dau tien k bi lap 2 lan
                }
                direction *= -1f;
                Vector2 castPoint = new Vector2(middleMapPos + ((offtset * (float) i) * direction), 2f);
                var spawnPoint = castPoint;
                RaycastHit2D hit = Physics2D.Raycast(castPoint, Vector2.down, 10f, whatIsGround);
                if (hit.collider != null)
                {
                    spawnPoint = hit.point;
                }

                GameObject spawnGameObject = ObjectPool.Spawn(spawnObjectPrefab, spawnPoint);
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
            }

            yield return new WaitForSeconds(delayBetweenTwoCast);

            //Debug.Log("hit");
        }

        isComplete = true;
    }
}
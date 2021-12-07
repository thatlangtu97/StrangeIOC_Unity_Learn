using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSkillTask : Action
{
    public SharedComponentManager componentManager;
    public SharedGameObject enemyToSummonPrefab;
    public SharedGameObjectList minions;
    public int summonCount;
    int maxCount;
    public float spawnRadius;
    public LayerMask cantSpawnLayer;
    public Vector2 spawnPrefabSize;
    public Vector2 offset;
    bool isRegisterEvent;
    public float coolDownBetweenSummon=0.05f;
    bool endSummon;
    public override void OnStart()
    {
        if (!isRegisterEvent)
        {
            maxCount = summonCount;
           // this.registerListener(EventID.ON_ENEMY_DIE, (sender, param) => { });
            isRegisterEvent = true;
        }
        StartCoroutine(Spawn());
        
    }
    IEnumerator Spawn()
    {
        endSummon = false;
        int count = summonCount;
        Detectable detecEnemy;
        for (int i = 0; i < count;)
        {
            Vector2 spawnPostion = componentManager.Value.entity.centerPoint.centerPoint.position;
            spawnPostion.x += offset.x + Random.Range(-spawnRadius, spawnRadius);
            spawnPostion.y += offset.y;
            if (spawnPostion.x < LevelCreator.instance.map.leftAnchor.position.x || spawnPostion.x > LevelCreator.instance.map.rightAnchor.position.x)
            {
                continue;
            }
            RaycastHit2D hit = Physics2D.BoxCast(spawnPostion, spawnPrefabSize, 0f, Vector2.down, 5f, cantSpawnLayer);
            if (!hit)
            {
                GameObject summonObj = GameObject.Instantiate(enemyToSummonPrefab.Value, spawnPostion, Quaternion.identity);
                if (GameModeManager.instance.mode != GameMode.BossRaid)
                    LevelCreator.instance.currEnemyCount++;
                else
                    BossRaidLevelCreator.instance.enemyCount++;
              


                i++;
                summonCount--;
                summonObj.GetComponent<StateMachineController>().onRunOutOfHealthAction += OnPetDie;
                detecEnemy = null;
                detecEnemy = summonObj.GetComponent<StateMachineController>().softBody.GetComponent<Detectable>();
                StartCoroutine(DelayAddDetectableToJob(detecEnemy));
                /*
                if (minions.Value == null)
                {
                    minions.Value = new List<GameObject>();
                }
                minions.Value.Add(summonObj);*/
                //Debug.Log(summonObj.GetComponent<StateMachineController>().componentManager.entity);
                
                yield return new WaitForSeconds(coolDownBetweenSummon);
                
            }

        }
        endSummon = true;
    }
    IEnumerator DelayAddDetectableToJob(Detectable detecEnemy) {
        yield return new WaitForSeconds(3f);
        if (CaculateRegeToEnemy.instance != null)
        {
            if (detecEnemy != null)
                CaculateRegeToEnemy.instance.SpawnEnemy(detecEnemy);
        }
    }
    public override TaskStatus OnUpdate()
    {
        if(!endSummon)
        {
            return TaskStatus.Running;
        }
        else
        {
            return TaskStatus.Success;
        }
    }
    void OnPetDie(GameObject minion)
    {
        if (summonCount < maxCount)
        {
            //summonCount++;
        }
        //minions.Value.Remove(minion);
    }

    public override void OnEnd()
    {
        base.OnEnd();
        summonCount = maxCount;
    }

}

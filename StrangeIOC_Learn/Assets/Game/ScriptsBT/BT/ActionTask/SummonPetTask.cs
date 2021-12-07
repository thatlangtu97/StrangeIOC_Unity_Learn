using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPetTask : Action
{
    public SharedComponentManager componentManager;
    public List<GameObject> summonPrefab;
    public SharedTransform summonPoint;
    public int maxNumberSummon;
    public int minNumberSummon;
    public float timeBetweenSummon;
    int random;
    int numberOfSummon;
    public override void OnStart()
    {
        numberOfSummon = Random.Range(minNumberSummon, maxNumberSummon);
        StartCoroutine(Summon());
       
    }

    IEnumerator Summon ()
    {
        for(int i =0; i< numberOfSummon; i++)
        {
            random = Random.Range(0, summonPrefab.Count);
            GameObject summonObj = GameObject.Instantiate(summonPrefab[random], summonPoint.Value.position, Quaternion.identity);
            if (GameModeManager.instance.mode != GameMode.BossRaid)
                LevelCreator.instance.currEnemyCount++;
            else
                BossRaidLevelCreator.instance.enemyCount++;

     
            Detectable detecEnemy;
            detecEnemy = null;
            detecEnemy = summonObj.GetComponent<StateMachineController>().softBody.GetComponent<Detectable>();
            StartCoroutine(DelayAddDetectableToJob(detecEnemy));
            yield return new WaitForSeconds(timeBetweenSummon);
        }     
    }
    IEnumerator DelayAddDetectableToJob(Detectable detecEnemy)
    {
        yield return new WaitForSeconds(3f);
        if (CaculateRegeToEnemy.instance != null)
        {
            if (detecEnemy != null)
                CaculateRegeToEnemy.instance.SpawnEnemy(detecEnemy);
        }
    }
}

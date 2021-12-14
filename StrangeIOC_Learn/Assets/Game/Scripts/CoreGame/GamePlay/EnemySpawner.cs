using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	string PATH = "Enemy/";
	public List<string> listPathPrefabs = new List<string>();
	Dictionary<int, List<GameObject>>dicEnemyOnWave = new Dictionary<int, List<GameObject>>();
    Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
	GameObject LoadPrefab(string path)
	{
		if (!prefabs.ContainsKey(path) || prefabs[path] == null)
		{
			GameObject prefab = Resources.Load<GameObject>(path);
			if (prefab == null)
			{
				Debug.LogError("Not found prefab: " + path);
			}
			if (!prefabs.ContainsKey(path))
			{
				prefabs.Add(path, prefab);
			}
			else
			{
				prefabs[path] = prefab;
			}
		}
		return prefabs[path];
	}
    public void Awake()
    {
		Setup();

	}
    private void Start()
    {
		InitListEnemySpawn(0);
	}
    void Setup()
    {
		foreach(string temp in listPathPrefabs)
        {
			GameObject prefab = LoadPrefab(PATH+temp);
		}
    }
	void InitEnemy()
    {

    }
	void InitListEnemySpawn(int waveId)
	{
		List<GameObject> listEnemy = new List<GameObject>();
		foreach (GameObject temp in prefabs.Values)
        {
			GameObject E = Instantiate(temp);
			E.SetActive(false);
			listEnemy.Add(E);
		}
		if (dicEnemyOnWave.ContainsKey(waveId))
		{
			foreach (GameObject temp in listEnemy)
				dicEnemyOnWave[waveId].Add(temp);
		}
		else
		{
			dicEnemyOnWave.Add(waveId, listEnemy);
		}
	}
}

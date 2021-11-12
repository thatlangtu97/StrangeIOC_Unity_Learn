using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabUtils 
{
    static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
	public static GameObject LoadPrefab(string path)
	{
		if (!prefabs.ContainsKey(path) || prefabs[path] == null)
		{
			GameObject prefab = Resources.Load<GameObject>(path);
			if (prefab == null)
			{
				Debug.LogError("Not found prefab: " + path);
			}
			if (!prefabs.ContainsKey(path)){
				prefabs.Add(path, prefab);
			}
            else
            {
				prefabs[path] = prefab;
			}
		}
		return prefabs[path];
	}
}

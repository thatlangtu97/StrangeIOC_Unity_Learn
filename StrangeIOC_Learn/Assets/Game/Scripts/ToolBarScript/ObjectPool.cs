using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public sealed class ObjectPool : MonoBehaviour
{
    public enum StartupPoolMode
    {
        Awake,
        Start,
        CallManually
    };

    [System.Serializable]
    public class StartupPool
    {
        public int size;
        public GameObject prefab;
    }

    static ObjectPool _instance;
    static List<GameObject> tempList = new List<GameObject>();

    Dictionary<GameObject, List<GameObject>> pooledObjects = new Dictionary<GameObject, List<GameObject>>();
    Dictionary<GameObject, GameObject> spawnedObjects = new Dictionary<GameObject, GameObject>();
    static Dictionary<string, GameObject> pathToGameObjectDict = new Dictionary<string, GameObject>();

    public StartupPoolMode startupPoolMode;
    public StartupPool[] startupPools;
    bool startupPoolsCreated;

    public static T Spawn<T>(T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : Component
    {
        return Spawn(prefab.gameObject, parent, position, rotation).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
    {
        return Spawn(prefab.gameObject, null, position, rotation).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Transform parent, Vector3 position) where T : Component
    {
        return Spawn(prefab.gameObject, parent, position, Quaternion.identity).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Vector3 position) where T : Component
    {
        return Spawn(prefab.gameObject, null, position, Quaternion.identity).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Transform parent) where T : Component
    {
        return Spawn(prefab.gameObject, parent, Vector3.zero, Quaternion.identity).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab) where T : Component
    {
        return Spawn(prefab.gameObject, null, Vector3.zero, Quaternion.identity).GetComponent<T>();
    }
    
    public static void Recycle<T>(T obj) where T : Component
    {
        Recycle(obj.gameObject);
    }
    
    public static void RecycleAll<T>(T prefab) where T : Component
    {
        RecycleAll(prefab.gameObject);
    }
    
    public static int CountPooled<T>(T prefab) where T : Component
    {
        return CountPooled(prefab.gameObject);
    }
    
    public static int CountSpawned<T>(T prefab) where T : Component
    {
        return CountSpawned(prefab.gameObject);
    }
    
    public static List<T> GetPooled<T>(T prefab, List<T> list, bool appendList) where T : Component
    {
        if (list == null)
            list = new List<T>();
        if (!appendList)
            list.Clear();
        if (instance.pooledObjects.TryGetValue(prefab.gameObject, out List<GameObject> pooled))
            for (int i = 0; i < pooled.Count; ++i)
                list.Add(pooled[i].GetComponent<T>());
        return list;
    }
    
    public static List<T> GetSpawned<T>(T prefab, List<T> list, bool appendList) where T : Component
    {
        if (list == null)
            list = new List<T>();
        if (!appendList)
            list.Clear();
        var prefabObj = prefab.gameObject;
        foreach (var item in instance.spawnedObjects)
            if (item.Value == prefabObj)
                list.Add(item.Key.GetComponent<T>());
        return list;
    }
    
    public static void CreatePool<T>(T prefab, int initialPoolSize) where T : Component
    {
        CreatePool(prefab.gameObject, initialPoolSize);
    }
    
    public static void DestroyPooled<T>(T prefab) where T : Component
    {
        DestroyPooled(prefab.gameObject);
    }
    void Awake()
    {
        _instance = this;
        if (startupPoolMode == StartupPoolMode.Awake)
            CreateStartupPools();
    }

    void Start()
    {
        if (startupPoolMode == StartupPoolMode.Start)
            CreateStartupPools();
    }
    
    IEnumerator delay( Action action, float time)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }
    
    public static void CreateStartupPools()
    {
        if (!instance.startupPoolsCreated)
        {
            instance.startupPoolsCreated = true;
            var pools = instance.startupPools;
            //Debug.Log(pools.Length);
            if (pools != null && pools.Length > 0)
                for (int i = 0; i < pools.Length; ++i)
                    CreatePool(pools[i].prefab, pools[i].size);
        }
    }
    
    public static void CreatePool(GameObject prefab, int initialPoolSize)
    {
        if (prefab != null && !instance.pooledObjects.ContainsKey(prefab))
        {
            var list = new List<GameObject>();
            instance.pooledObjects.Add(prefab, list);
            if (initialPoolSize > 0)
            {
                bool active = prefab.activeSelf;
                prefab.SetActive(false);
                Transform parent = instance.transform;
                while (list.Count < initialPoolSize)
                {
                    var obj = (GameObject)Object.Instantiate(prefab, parent, true);
                    obj.transform.SetParent(parent);
                    list.Add(obj);
                }
                prefab.SetActive(active);
            }
        }
    }
    
//    public static GameObject CreateBeforePool(GameObject prefab, int initialPoolSize)
//    {
//        GameObject objTemp = null;
//        if (prefab != null && !instance.pooledObjects.ContainsKey(prefab))
//        {
//            var list = new List<GameObject>();
//            instance.pooledObjects.Add(prefab, list);
//
//            if (initialPoolSize > 0)
//            {
//                bool active = prefab.activeSelf;
//                prefab.SetActive(false);
//                Transform parent = instance.transform;
//                
//                while (list.Count < initialPoolSize)
//                {
//                    var obj = (GameObject)Object.Instantiate(prefab);
//                    objTemp = obj;
//                    list.Add(obj);
//                }
//
//                prefab.SetActive(active);
//                
//            }
//        }
//        return objTemp;
//    }

    public static void Recycle(GameObject gameObject)
    {   
        if (instance.spawnedObjects.TryGetValue(gameObject, out GameObject prefab))
        {
            gameObject.SetActive(false);
            gameObject.transform.parent = null;
            instance.pooledObjects[prefab].Add(gameObject);
            instance.spawnedObjects.Remove(gameObject);
        }
    }
    public void Recycle(GameObject gameObject,float Time)
    {
       StartCoroutine( delay(() => { Recycle(gameObject); }, Time));
    }
    
    public static GameObject Spawn(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
    {
        if (instance.pooledObjects.TryGetValue(prefab, out List<GameObject> list))
        {
            GameObject obj = null;
            Transform transform;
            if (list.Count > 0)
            {
                while (obj == null && list.Count > 0)
                {
                    obj = list[0];
                    list.RemoveAt(0);
                    break;
                }
                if (obj != null)
                {
                    transform = obj.transform;
                    transform.parent = parent;
                    transform.localPosition = position;
                    transform.localRotation = rotation;
                    transform.localScale = prefab.transform.localScale;
                    obj.SetActive(true);
                    instance.spawnedObjects.Add(obj, prefab);
                    return obj;
                }
            }
            obj = (GameObject)Object.Instantiate(prefab);
            transform = obj.transform;
            transform.parent = parent;
            transform.localPosition = position;
            transform.localRotation = rotation;
            transform.localScale = prefab.transform.localScale;
            instance.spawnedObjects.Add(obj, prefab);
            return obj;
        }
        else
        {
            CreatePool(prefab, 1);
            return Spawn(prefab, parent, position, rotation);
        }
    }
    
    
    
    public static GameObject Spawn(GameObject prefab, Transform parent, Vector3 localPosition, Quaternion localRotation , Vector3 localScale)
    {
        if (instance.pooledObjects.TryGetValue(prefab, out List<GameObject> list))
        {
            GameObject obj = null;
            Transform transform;
            if (list.Count > 0)
            {
                while (obj == null && list.Count > 0)
                {
                    obj = list[0];
                    list.RemoveAt(0);
                    break;
                }
                if (obj != null)
                {
                    transform = obj.transform;
                    transform.parent = parent;
                    transform.localPosition = localPosition;
                    transform.localRotation = localRotation;
                    transform.localScale = localScale;
                    obj.SetActive(true);
                    instance.spawnedObjects.Add(obj, prefab);
                    return obj;
                }
            }
            obj = (GameObject)Object.Instantiate(prefab);
            transform = obj.transform;
            transform.parent = parent;
            transform.localPosition = localPosition;
            transform.localRotation = localRotation;
            transform.localScale = localScale;
            obj.SetActive(true);
            instance.spawnedObjects.Add(obj, prefab);
            return obj;
        }
        else
        {
            CreatePool(prefab, 1);
            return Spawn(prefab, parent, localPosition, localRotation, localScale);
        }
    }
    
    public static GameObject Spawn(GameObject prefab, Transform parent, Vector3 localPosition, Vector3 rightTransform , Vector3 localScale)
    {
        if (instance.pooledObjects.TryGetValue(prefab, out List<GameObject> list))
        {
            GameObject obj = null;
            Transform transform;
            if (list.Count > 0)
            {
                while (obj == null && list.Count > 0)
                {
                    obj = list[0];
                    list.RemoveAt(0);
                    break;
                }
                if (obj != null)
                {
                    transform = obj.transform;
                    transform.parent = parent;
                    transform.localPosition = localPosition;
                    transform.right = rightTransform;
                    transform.localScale = localScale;
                    obj.SetActive(true);
                    instance.spawnedObjects.Add(obj, prefab);
                    return obj;
                }
            }
            obj = (GameObject)Object.Instantiate(prefab);
            transform = obj.transform;
            transform.parent = parent;
            transform.localPosition = localPosition;
            transform.right = rightTransform;
            transform.localScale = localScale;
            obj.SetActive(true);
            instance.spawnedObjects.Add(obj, prefab);
            return obj;
        }
        else
        {
            CreatePool(prefab, 1);
            return Spawn(prefab, parent, localPosition, rightTransform, localScale);
        }
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
//    public static GameObject GetObjectBeforeSpawn(GameObject prefab)
//    {
//        List<GameObject> list;
//        Transform trans;
//        GameObject obj;
//        if (instance.pooledObjects.TryGetValue(prefab, out list))
//        {
//            obj = null;
//            if (list.Count > 0)
//            {
//                while (obj == null && list.Count > 0)
//                {
//                    obj = list[0];
//                    list.RemoveAt(0);
//                    break;
//                }
//
//                if (obj != null)
//                {
////                    trans = obj.transform;
////                    //trans.parent = parent;
////                    trans.SetParent(parent);
////                    trans.localPosition = position;
////                    trans.localRotation = rotation;
////                    trans.localScale = prefab.transform.localScale;
////                    obj.SetActive(true);
//                    instance.spawnedObjects.Add(obj, prefab);
//                    
//                    return obj;
//                }
//            }
//            prefab.SetActive(false);
//            obj = (GameObject)Object.Instantiate(prefab);
////            trans = obj.transform;
////            //trans.parent = parent;
////            trans.SetParent(parent);
////            trans.localPosition = position;
////            trans.localRotation = rotation;
////            trans.localScale = prefab.transform.localScale;
//            
//            obj.SetActive(false);
//            instance.spawnedObjects.Add(obj, prefab);
//            prefab.SetActive(true);
//            return obj;
//        }
//        else
//        {
//            //CreatePool(prefab, 1);
//
//            return CreateBeforePool(prefab, 1);
//
//
//        }
//        
//    }

    public static GameObject Spawn(GameObject prefab, Transform parent, Vector3 position)
    {
        return Spawn(prefab, parent, position, Quaternion.identity);
    }

    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return Spawn(prefab, null, position, rotation);
    }

    public static GameObject Spawn(GameObject prefab, Transform parent)
    {
        return Spawn(prefab, parent, Vector3.zero, Quaternion.identity);
    }

    public static GameObject Spawn(GameObject prefab, Vector3 position)
    {
        return Spawn(prefab, null, position, Quaternion.identity);
    }

    public static GameObject Spawn(GameObject prefab)
    {
        return Spawn(prefab, null, Vector3.zero, Quaternion.identity);
    }
    
    public static GameObject Spawn(string path, Transform parent, Vector3 position, Quaternion rotation)
    {
        return Spawn(GetGameObjectFromPath(path), parent, position, rotation);
    }

    public static GameObject Spawn(string path, Transform parent, Vector3 position)
    {
        return Spawn(GetGameObjectFromPath(path), parent, position, Quaternion.identity);
    }
    
    public static GameObject Spawn(string path, Vector3 position, Quaternion rotation)
    {
        return Spawn(GetGameObjectFromPath(path), null, position, rotation);
    }
    
    public static GameObject Spawn(string path, Transform parent)
    {
        return Spawn(GetGameObjectFromPath(path), parent, Vector3.zero, Quaternion.identity);
    }
    
    public static GameObject Spawn(string path, Vector3 position)
    {
        return Spawn(GetGameObjectFromPath(path), null, position, Quaternion.identity);
    }

    public static GameObject Spawn(string path)
    {
        return Spawn(GetGameObjectFromPath(path), null, Vector3.zero, Quaternion.identity);
    }

    static GameObject GetGameObjectFromPath(string path)
    {
        if (!pathToGameObjectDict.ContainsKey(path))
        {
            var obj = Resources.Load(path) as GameObject;
#if UNITY_EDITOR
            if (obj == null) Debug.LogErrorFormat("ERROR: Can't load object at {0}", path);
#endif
            pathToGameObjectDict.Add(path, obj);
        }
        return pathToGameObjectDict[path];
    }

    //public static void Recycle(GameObject obj)
    //{
    //    GameObject prefab;
    //    if (instance.spawnedObjects.TryGetValue(obj, out prefab))
    //        Recycle(obj, prefab);
    //    else
    //        Object.Destroy(obj);
    //}

    static void Recycle(GameObject obj, GameObject prefab)
    {
        if (obj)
        {
            instance.pooledObjects[prefab].Add(obj);
            instance.spawnedObjects.Remove(obj);
            obj.transform.SetParent(instance.transform);
            obj.SetActive(false);
        }
    }
    public static void RecycleAll(GameObject prefab)
    {
        foreach (var item in instance.spawnedObjects)
            if (item.Value == prefab)
                tempList.Add(item.Key);
        for (int i = 0; i < tempList.Count; ++i)
            Recycle(tempList[i]);
        tempList.Clear();
    }

    public static void RecycleAll()
    {
        tempList.AddRange(instance.spawnedObjects.Keys);
        for (int i = 0; i < tempList.Count; ++i)
            Recycle(tempList[i]);
        tempList.Clear();
    }

    public static bool IsSpawned(GameObject obj)
    {
        return instance.spawnedObjects.ContainsKey(obj);
    }
    
    public static int CountPooled(GameObject prefab)
    {
        List<GameObject> list;
        if (instance.pooledObjects.TryGetValue(prefab, out list))
            return list.Count;
        return 0;
    }

    public static int CountSpawned(GameObject prefab)
    {
        int count = 0;
        foreach (var instancePrefab in instance.spawnedObjects.Values)
            if (prefab == instancePrefab)
                ++count;
        return count;
    }

    public static int CountAllPooled()
    {
        int count = 0;
        foreach (var list in instance.pooledObjects.Values)
            count += list.Count;
        return count;
    }

    public static List<GameObject> GetPooled(GameObject prefab, List<GameObject> list, bool appendList)
    {
        if (list == null)
            list = new List<GameObject>();
        if (!appendList)
            list.Clear();
        List<GameObject> pooled;
        if (instance.pooledObjects.TryGetValue(prefab, out pooled))
            list.AddRange(pooled);
        return list;
    }
    
    public static List<GameObject> GetSpawned(GameObject prefab, List<GameObject> list, bool appendList)
    {
        if (list == null)
            list = new List<GameObject>();
        if (!appendList)
            list.Clear();
        foreach (var item in instance.spawnedObjects)
            if (item.Value == prefab)
                list.Add(item.Key);
        return list;
    }

    public static void DestroyPooled(GameObject prefab)
    {
        List<GameObject> pooled;
        if (instance.pooledObjects.TryGetValue(prefab, out pooled))
        {
            for (int i = 0; i < pooled.Count; ++i)
                GameObject.Destroy(pooled[i]);
            pooled.Clear();
        }
    }
    
    public static void DestroyAll(GameObject prefab)
    {
        RecycleAll(prefab);
        DestroyPooled(prefab);
    }

    public static void DestroyAll<T>(T prefab) where T : Component
    {
        DestroyAll(prefab.gameObject);
    }

    public static ObjectPool instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            _instance = Object.FindObjectOfType<ObjectPool>();
            if (_instance != null)
                return _instance;

            var obj = new GameObject("ObjectPool");
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            _instance = obj.AddComponent<ObjectPool>();
            return _instance;
        }
    }
}
//
//public static class ObjectPoolExtensions
//{
//    public static void CreatePool<T>(this T prefab) where T : Component
//    {
//        ObjectPool.CreatePool(prefab, 0);
//    }
//
//    public static void CreatePool<T>(this T prefab, int initialPoolSize) where T : Component
//    {
//        ObjectPool.CreatePool(prefab, initialPoolSize);
//    }
//
//    public static void CreatePool(this GameObject prefab)
//    {
//        ObjectPool.CreatePool(prefab, 0);
//    }
//
//    public static void CreatePool(this GameObject prefab, int initialPoolSize)
//    {
//        ObjectPool.CreatePool(prefab, initialPoolSize);
//    }
//
//    public static T Spawn<T>(this T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : Component
//    {
//        return ObjectPool.Spawn(prefab, parent, position, rotation);
//    }
//
//    public static T Spawn<T>(this T prefab, Vector3 position, Quaternion rotation) where T : Component
//    {
//        return ObjectPool.Spawn(prefab, null, position, rotation);
//    }
//
//    public static T Spawn<T>(this T prefab, Transform parent, Vector3 position) where T : Component
//    {
//        return ObjectPool.Spawn(prefab, parent, position, Quaternion.identity);
//    }
//
//    public static T Spawn<T>(this T prefab, Vector3 position) where T : Component
//    {
//        return ObjectPool.Spawn(prefab, null, position, Quaternion.identity);
//    }
//
//    public static T Spawn<T>(this T prefab, Transform parent) where T : Component
//    {
//        return ObjectPool.Spawn(prefab, parent, Vector3.zero, Quaternion.identity);
//    }
//
//    public static T Spawn<T>(this T prefab) where T : Component
//    {
//        return ObjectPool.Spawn(prefab, null, Vector3.zero, Quaternion.identity);
//    }
//
//    public static GameObject Spawn(this GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
//    {
//        return ObjectPool.Spawn(prefab, parent, position, rotation);
//    }
//
//    public static GameObject Spawn(this GameObject prefab, Vector3 position, Quaternion rotation)
//    {
//        return ObjectPool.Spawn(prefab, null, position, rotation);
//    }
//
//    public static GameObject Spawn(this GameObject prefab, Transform parent, Vector3 position)
//    {
//        return ObjectPool.Spawn(prefab, parent, position, Quaternion.identity);
//    }
//
//    public static GameObject Spawn(this GameObject prefab, Vector3 position)
//    {
//        return ObjectPool.Spawn(prefab, null, position, Quaternion.identity);
//    }
//
//    public static GameObject Spawn(this GameObject prefab, Transform parent)
//    {
//        return ObjectPool.Spawn(prefab, parent, Vector3.zero, Quaternion.identity);
//    }
//
//    public static GameObject Spawn(this GameObject prefab)
//    {
//        return ObjectPool.Spawn(prefab, null, Vector3.zero, Quaternion.identity);
//    }
//
//    public static void Recycle<T>(this T obj) where T : Component
//    {
//        ObjectPool.Recycle(obj);
//    }
//
//    public static void Recycle(this GameObject obj)
//    {
//        ObjectPool.Recycle(obj);
//    }
//
//    //public static void Recycle(this GameObject gameObject, float delay)
//    //{
//    //    ObjectPool.Recycle(gameObject, delay);
//    //}
//
//    public static void RecycleAll<T>(this T prefab) where T : Component
//    {
//        ObjectPool.RecycleAll(prefab);
//    }
//
//    public static void RecycleAll(this GameObject prefab)
//    {
//        ObjectPool.RecycleAll(prefab);
//    }
//
//    public static int CountPooled<T>(this T prefab) where T : Component
//    {
//        return ObjectPool.CountPooled(prefab);
//    }
//
//    public static int CountPooled(this GameObject prefab)
//    {
//        return ObjectPool.CountPooled(prefab);
//    }
//
//    public static int CountSpawned<T>(this T prefab) where T : Component
//    {
//        return ObjectPool.CountSpawned(prefab);
//    }
//
//    public static int CountSpawned(this GameObject prefab)
//    {
//        return ObjectPool.CountSpawned(prefab);
//    }
//
//    public static List<GameObject> GetSpawned(this GameObject prefab, List<GameObject> list, bool appendList)
//    {
//        return ObjectPool.GetSpawned(prefab, list, appendList);
//    }
//
//    public static List<GameObject> GetSpawned(this GameObject prefab, List<GameObject> list)
//    {
//        return ObjectPool.GetSpawned(prefab, list, false);
//    }
//
//    public static List<GameObject> GetSpawned(this GameObject prefab)
//    {
//        return ObjectPool.GetSpawned(prefab, null, false);
//    }
//
//    public static List<T> GetSpawned<T>(this T prefab, List<T> list, bool appendList) where T : Component
//    {
//        return ObjectPool.GetSpawned(prefab, list, appendList);
//    }
//
//    public static List<T> GetSpawned<T>(this T prefab, List<T> list) where T : Component
//    {
//        return ObjectPool.GetSpawned(prefab, list, false);
//    }
//
//    public static List<T> GetSpawned<T>(this T prefab) where T : Component
//    {
//        return ObjectPool.GetSpawned(prefab, null, false);
//    }
//
//    public static List<GameObject> GetPooled(this GameObject prefab, List<GameObject> list, bool appendList)
//    {
//        return ObjectPool.GetPooled(prefab, list, appendList);
//    }
//
//    public static List<GameObject> GetPooled(this GameObject prefab, List<GameObject> list)
//    {
//        return ObjectPool.GetPooled(prefab, list, false);
//    }
//
//    public static List<GameObject> GetPooled(this GameObject prefab)
//    {
//        return ObjectPool.GetPooled(prefab, null, false);
//    }
//
//    public static List<T> GetPooled<T>(this T prefab, List<T> list, bool appendList) where T : Component
//    {
//        return ObjectPool.GetPooled(prefab, list, appendList);
//    }
//
//    public static List<T> GetPooled<T>(this T prefab, List<T> list) where T : Component
//    {
//        return ObjectPool.GetPooled(prefab, list, false);
//    }
//
//    public static List<T> GetPooled<T>(this T prefab) where T : Component
//    {
//        return ObjectPool.GetPooled(prefab, null, false);
//    }
//
//    public static void DestroyPooled(this GameObject prefab)
//    {
//        ObjectPool.DestroyPooled(prefab);
//    }
//
//    public static void DestroyPooled<T>(this T prefab) where T : Component
//    {
//        ObjectPool.DestroyPooled(prefab.gameObject);
//    }
//
//    public static void DestroyAll(this GameObject prefab)
//    {
//        ObjectPool.DestroyAll(prefab);
//    }
//
//    public static void DestroyAll<T>(this T prefab) where T : Component
//    {
//        ObjectPool.DestroyAll(prefab.gameObject);
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroLogic 
{
    static Dictionary<int, HeroConfig> cacheConfig = new Dictionary<int, HeroConfig>();
    public static void Cache()
    {
        cacheConfig = new Dictionary<int, HeroConfig>();
        foreach (HeroConfig itemConfig in ScriptableObjectData.HeroConfigCollection.heroes)
        {
            cacheConfig.Add(itemConfig.id, itemConfig);
        }
    }

    public static HeroConfig GetHeroConfigById(int id)
    {
        return cacheConfig[id];
    }
}

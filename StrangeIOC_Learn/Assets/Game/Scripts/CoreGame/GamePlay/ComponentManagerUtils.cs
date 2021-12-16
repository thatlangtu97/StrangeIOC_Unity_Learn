using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentManagerUtils
{
    static List<ComponentManager> componentManagers= new List<ComponentManager>();
    static List<ProjectileComponent > projectileComponents = new List<ProjectileComponent>();
    public static void AddComponent(ComponentManager component)
    {
        if (!componentManagers.Contains(component))
        {
            componentManagers.Add(component);
        }
    }
    public static void AddComponent(ProjectileComponent component)
    {
        if (!projectileComponents.Contains(component))
        {
            projectileComponents.Add(component);
        }
    }
    public static void ResetAll()
    {
        foreach(ComponentManager temp in componentManagers)
        {
            if (temp != null)
            {
                temp.DestroyEntity();
            }
        }
        componentManagers.Clear();
        projectileComponents.Clear();
    }
}

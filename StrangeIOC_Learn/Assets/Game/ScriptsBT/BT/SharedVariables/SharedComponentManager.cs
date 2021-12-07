using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedComponentManager : SharedVariable<ComponentManager>
{
    public static implicit operator SharedComponentManager(ComponentManager value) { return new SharedComponentManager { Value = value }; }
}
public class SharedSkillCheck : SharedVariable<SharedSkillCheckInfo>
{
    public static implicit operator SharedSkillCheck(SharedSkillCheckInfo value)
    {
        return new SharedSkillCheck { Value = value };
    }
}
public class SharedListInt : SharedVariable<ListInt>
{
    public static implicit operator SharedListInt(ListInt value)
    {
        return new SharedListInt { Value = value };
    }
}

public class SharedGameEntityList : SharedVariable<List<GameEntity>>
{
    public static implicit operator SharedGameEntityList(List<GameEntity> value) { return new SharedGameEntityList { Value = value }; }
}

public class SharedSkillPlayerConfig : SharedVariable<SkillPlayerConfig>
{
    public static implicit operator SharedSkillPlayerConfig(SkillPlayerConfig value)
    {
        return new SharedSkillPlayerConfig { Value = value };
    }
}

public class SharedProjectileGroup : SharedVariable<ProjectileGroup>
{
    public static implicit operator SharedProjectileGroup(ProjectileGroup value)
    {
        return new SharedProjectileGroup { Value = value };
    }
}

public class SharedGameEntity : SharedVariable<GameEntity>
{
    public static implicit operator SharedGameEntity(GameEntity value)
    {
        return new SharedGameEntity { Value = value };
    }
}
public class SharedAnimator : SharedVariable<Animator>
{
    public static implicit operator SharedAnimator(Animator value)
    {
        return new SharedAnimator { Value = value };
    }
}

[System.Serializable]
public class SharedSkillCheckInfo
{
    public float cooldown;
    public float lastTimeUsed;
    public float range;
    public float dmgMultiple;
    public float knockBackForce;
    public float knockUpForce;
    public bool canNotReduce;
    public float critChance;
    public float critMultiply;
    public float minRange;
}
[System.Serializable]
public class ListInt
{
    public List<int> listValue = new List<int>();
}

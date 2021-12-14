using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SharedComponentManager : SharedVariable<ComponentManager>
{
    public static implicit operator SharedComponentManager(ComponentManager value) { return new SharedComponentManager { Value = value }; }
}
/*
public class SharedSkillConfig : SharedVariable<SkillConfigBehaviourTree>
{
    public static implicit operator SharedSkillConfig(SkillConfigBehaviourTree value)
    {
        return new SharedSkillConfig { Value = value };
    }
}
*/
//public class SharedSkillCheck : SharedVariable<SharedSkillCheckInfo>
//{
//    public static implicit operator SharedSkillCheck(SharedSkillCheckInfo value)
//    {
//        return new SharedSkillCheck { Value = value };
//    }
//}
//public class SharedListInt : SharedVariable<ListInt>
//{
//    public static implicit operator SharedListInt(ListInt value)
//    {
//        return new SharedListInt { Value = value };
//    }
//}

//public class SharedGameEntityList : SharedVariable<List<GameEntity>>
//{
//    public static implicit operator SharedGameEntityList(List<GameEntity> value) { return new SharedGameEntityList { Value = value }; }
//}

//public class SharedSkillPlayerConfig : SharedVariable<SkillPlayerConfig>
//{
//    public static implicit operator SharedSkillPlayerConfig(SkillPlayerConfig value)
//    {
//        return new SharedSkillPlayerConfig { Value = value };
//    }
//}

//public class SharedProjectileGroup : SharedVariable<ProjectileGroup>
//{
//    public static implicit operator SharedProjectileGroup(ProjectileGroup value)
//    {
//        return new SharedProjectileGroup { Value = value };
//    }
//}

//public class SharedGameEntity : SharedVariable<GameEntity>
//{
//    public static implicit operator SharedGameEntity(GameEntity value)
//    {
//        return new SharedGameEntity { Value = value };
//    }
//}
//public class SharedAnimator : SharedVariable<Animator>
//{
//    public static implicit operator SharedAnimator(Animator value)
//    {
//        return new SharedAnimator { Value = value };
//    }
//}



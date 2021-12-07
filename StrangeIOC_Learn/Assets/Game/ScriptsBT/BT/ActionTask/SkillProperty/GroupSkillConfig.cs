using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skill ", menuName = "Tools/Group Of Skill")]
public class GroupSkillConfig : BaseSkillConfig
{
    [Expandable]
    [OdinSerialize]
    public List<BaseSkill> subSkillGroup = new List<BaseSkill>();
    public override void CastSkill(GameEntity entity)
    {
        base.CastSkill(entity);
        foreach(var skill in subSkillGroup)
        {
            skill.CastSkill(entity, null);
        }
        //var newEntity = Contexts.sharedInstance.game.CreateEntity();
        //newEntity.AddGroupSkill(Instantiate(this), subSkillGroup.Count, entity);
        
    }
   

}

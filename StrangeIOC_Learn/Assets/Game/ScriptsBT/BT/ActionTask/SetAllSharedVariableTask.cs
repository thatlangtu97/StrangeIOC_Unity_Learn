using System;
using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

public class SetAllSharedVariableTask : Action
{
    // Start is called before the first frame update
    BehaviorTree bt;
    public SharedComponentManager component;
    bool isSet;
    public override void OnStart()
    {
        //if (!isSet)
        //{
        //    isSet = true;
           
        //    BehaviorTree bt = component.Value.GetComponentInChildren<BehaviorTree>();
        //    var allVariable = bt.GetAllVariables();
        //    if(component.Value.entity.hasSkillContainer)
        //    {
        //        foreach (var skillId in component.Value.entity.skillContainer.skillId)
        //        {
        //            //Debug.Log(skillId);
        //            SharedSkillCheckInfo skillCheck = new SharedSkillCheckInfo();
        //            skillCheck.cooldown = float.Parse(SkillConfigManager.Instance.GetConfigBySkillType(skillId, SkillValueType.Cooldown), System.Globalization.CultureInfo.InvariantCulture);

        //            skillCheck.range = float.Parse(SkillConfigManager.Instance.GetConfigBySkillType(skillId, SkillValueType.Range), System.Globalization.CultureInfo.InvariantCulture);
        //            float.TryParse(SkillConfigManager.Instance.GetConfigBySkillType(skillId, SkillValueType.DamageMultiplier), out skillCheck.dmgMultiple);
        //            bool.TryParse(SkillConfigManager.Instance.GetConfigBySkillType(skillId, SkillValueType.cooldownImmune), out skillCheck.canNotReduce);
        //            float.TryParse(SkillConfigManager.Instance.GetConfigBySkillType(skillId, SkillValueType.KnockBackForce), out skillCheck.knockBackForce);
        //            string orderSkillName = SkillConfigManager.Instance.GetConfigBySkillType(skillId, SkillValueType.Order);

        //            //Debug.Log(orderSkillName);
        //            bt.SetVariableValue(orderSkillName, skillCheck);
        //            foreach (SkillConfigData skillValue in SkillConfigManager.Instance.GetAllConfigbyId(skillId).Values)
        //            {

                        
        //                switch (skillValue.VALUETYPE)
        //                {
        //                    case ValueType.BOOLEAN:
        //                        {
        //                            if (bt.GetVariable(skillValue.SKILLVALUETYPE.ToString()) != null)
        //                                bt.SetVariableValue(skillValue.SKILLVALUETYPE.ToString(), bool.Parse(skillValue.Skillvalue));
        //                            break;
        //                        }
        //                    case ValueType.FLOAT:
        //                        {
        //                            if (bt.GetVariable(skillValue.SKILLVALUETYPE.ToString()) != null)
        //                                bt.SetVariableValue(skillValue.SKILLVALUETYPE.ToString(), float.Parse(skillValue.Skillvalue, System.Globalization.CultureInfo.InvariantCulture));
        //                            break;
        //                        }
        //                    case ValueType.INTEGER:
        //                        {
        //                            if (bt.GetVariable(skillValue.SKILLVALUETYPE.ToString()) != null)
        //                                bt.SetVariableValue(skillValue.SKILLVALUETYPE.ToString(), int.Parse(skillValue.Skillvalue));
        //                            break;
        //                        }
        //                    case ValueType.STRING:
        //                        {
        //                            if(bt.GetVariable(skillValue.SKILLVALUETYPE.ToString())!=null)
        //                                bt.SetVariableValue(skillValue.SKILLVALUETYPE.ToString(), skillValue.Skillvalue);
        //                            break;
        //                        }
        //                }
        //            }
        //        }
        //    }
            
        //}
       

    }
}

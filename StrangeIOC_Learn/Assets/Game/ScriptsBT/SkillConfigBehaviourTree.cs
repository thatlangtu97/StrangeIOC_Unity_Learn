using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkillConfig", menuName = "SkillConfigBT/SkillConfig")]
[System.Serializable]
public class SkillConfigBehaviourTree : ScriptableObject
{
    public List<StepSkill> listStepSkill;
}
[System.Serializable]
public struct StepSkill
{
    public int idStep;
    public string animationName;
    public float duration;
}

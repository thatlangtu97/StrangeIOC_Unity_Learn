using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkillComboConfig", menuName = "CoreGame/SkillComboConfig")]
public class SkillComboConfig : ScriptableObject
{
    public List<SkillConfig> skillDatas;
}
[System.Serializable]
public class SkillConfig
{
    public int id;
    public AnimationCurve curveX,curveY;
    public string nameTrigger;
    public float durationAnimation;
    public string nameEvent;
    public bool lockVelocity;
    //public Vector2 velocity;
    //public float durationVelocity;
}

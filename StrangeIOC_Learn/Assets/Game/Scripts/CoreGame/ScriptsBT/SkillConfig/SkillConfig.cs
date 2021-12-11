using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkillConfig", menuName = "CoreGame/SkillConfig")]
public class ComboSkillConfig : ScriptableObject
{
    public List<SkillData> skillDatas;
}
[System.Serializable]
public class SkillData
{
    public int id;
    public string NameTrigger;
    public float durationAnimation;
    public string nameEvent;
    public Vector2 velocity;
    public float durationVelocity;

}



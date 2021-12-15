using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackComboConfig", menuName = "CoreGame/AttackComboConfig")]
public class AttackComboConfig : ScriptableObject
{
    public List<AttackConfig> skillDatas;
}
[System.Serializable]
public class AttackConfig
{
    public int id;
    public string NameTrigger;
    public float durationAnimation;
    public string nameEvent;
    public Vector2 velocity;
    public float durationVelocity;

}

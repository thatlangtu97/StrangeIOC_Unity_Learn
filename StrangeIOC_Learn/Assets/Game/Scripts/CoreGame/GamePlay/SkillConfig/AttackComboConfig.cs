using BehaviorDesigner.Runtime.Tasks;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "AttackComboConfig", menuName = "CoreGame/AttackComboConfig")]
//public class AttackComboConfig : SerializedScriptableObject
//{
//    public List<IComboOnEvent> EventCombo;
//    public EventConfig e;
//}
[System.Serializable]
public class AttackConfig 
{
    public string NameTrigger;
    public float durationAnimation;
    public Vector2 velocity;
    public float durationVelocity;
    public AnimationCurve curveX, curveY;
    public EventConfig eventConfig;
}

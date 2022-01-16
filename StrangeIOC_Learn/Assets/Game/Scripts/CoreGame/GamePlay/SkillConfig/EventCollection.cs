using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EventState", menuName = "CoreGame/EventState")]
public class EventCollection : SerializedScriptableObject
{
    public string NameTrigger;
    public float durationAnimation;
    public AnimationCurve curveX, curveY;
    public List<IComboEvent> EventCombo;
}

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
    [HideReferenceObjectPicker]
    [LabelText("EVENT")]
    public List<IComboEvent> EventCombo = new List<IComboEvent>();
    protected override void OnAfterDeserialize()
    {
        //base.OnAfterDeserialize();
        Modify();
    }
    protected override void OnBeforeSerialize()
    {
        //base.OnBeforeSerialize();
        Modify();
    }
    public void Modify()
    {
        if (EventCombo != null)
        {
            for (int i = 0; i < EventCombo.Count; i++)
            {
                EventCombo[i].id = i;
            }
        }
        else
        {
            EventCombo = new List<IComboEvent>();
        }
    }
}

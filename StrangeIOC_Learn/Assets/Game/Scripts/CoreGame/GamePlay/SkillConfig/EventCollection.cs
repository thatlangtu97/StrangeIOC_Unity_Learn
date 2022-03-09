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
    public AnimationCurve curveSpeedAnimation= new AnimationCurve(new Keyframe(0,1f));
    [HideReferenceObjectPicker]
    [LabelText("EVENT")]
    public List<IComboEvent> EventCombo = new List<IComboEvent>();

    //protected override void OnAfterDeserialize()
    //{
    //    //base.OnAfterDeserialize();
    //    //Modify();
    //}
    protected override void OnBeforeSerialize()
    {
        //base.OnBeforeSerialize();
        //Modify();
    }
    [Button("ACCEPT MODIFY",ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]

    public void Modify()
    {
        if (EventCombo != null)
        {
            for (int i = 0; i < EventCombo.Count; i++)
            {
                
                EventCombo[i].id = i*100;
            }
        }
        else
        {
            EventCombo = new List<IComboEvent>();
        }
    }
}

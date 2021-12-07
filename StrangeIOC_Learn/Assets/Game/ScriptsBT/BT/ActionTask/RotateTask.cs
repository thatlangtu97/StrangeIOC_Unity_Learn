using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTask :Action
{
    public SharedTransform visual;
    public SharedVector3 rotateValue;
    public override void OnStart()
    {
        var newRot = visual.Value.rotation.eulerAngles;

        visual.Value.DORotate(new Vector3(newRot.x, newRot.y, rotateValue.Value.z), 0.05f).OnComplete(()=> {  });
        
       // Quaternion newRotation = transform.rotation;
        //newRotation.z = rotateValue.Value.z;
        
        //transform.rotation = newRotation;
    }
}


using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateAngleTwoPoint : Action
{
    public float offset;    
    public SharedVector2  targetPos;
    private Vector3 thisPos;
    public SharedVector3 storedQuaternion;
    public float angle;
    public override void OnStart()
    {
        thisPos = transform.position;
        Vector2 dirFace;
        if(targetPos.Value.x > thisPos.x)
        {
            dirFace = Vector2.right;
        }
        else
        {
            dirFace = Vector2.left;
        }
        //var newPos = new Vector2(targetPos.Value.x - thisPos.x,targetPos.Value.y - thisPos.y);
        //angle = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        //if(targetPos.Value.x < thisPos.x)
        //{
        //    angle *= -1;
        //}
        angle = Vector2.Angle(targetPos.Value - (Vector2)thisPos, dirFace);
        if(targetPos.Value.y > thisPos.y)
        {
            angle *= -1;
        }
        storedQuaternion.Value = new Vector3(0, 0, angle + offset);
    }
}

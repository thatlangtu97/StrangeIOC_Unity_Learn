using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToTargetTask : Action
{
    public SharedComponentManager component;
    bool isFaceRight;
    bool isNotSet=true;
    public override TaskStatus OnUpdate()
    {
        var target = component.Value.entity.checkEnemyInSigh.enemy;
        if(target!=null)
        {
            var targetPos = target.centerPoint.centerPoint.position;
            if(component.Value.entity.centerPoint.centerPoint.position.x-targetPos.x<0f)
            {
                if(!isFaceRight||isNotSet)
                {
                    component.Value.transform.rotation = Quaternion.Euler(0, 0, component.Value.transform.rotation.z);
                    isFaceRight = true;
                    isNotSet = false;
                }
            }
            else
            {
                if(isFaceRight||isNotSet)
                {
                    component.Value.transform.rotation = Quaternion.Euler(0, 180, component.Value.transform.rotation.z);
                    isFaceRight = false;
                    isNotSet = false;
                }
            }
        }
        return TaskStatus.Running;
    }
}

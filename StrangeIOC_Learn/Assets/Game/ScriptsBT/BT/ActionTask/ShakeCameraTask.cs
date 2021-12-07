using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class ShakeCameraTask : Action
{
    public SharedComponentManager component;
    public float duration;
    public int shakeLevel;
    public bool notShakeByEvent;
    public override void OnStart()
    {
        base.OnStart();
        if(notShakeByEvent)
            HitStopController.instance.HitStop(shakeLevel, duration);
        else
        {
            component.Value.entity.stateMachineContainer.stateMachine.cameraShake.duration = duration;
            component.Value.entity.stateMachineContainer.stateMachine.cameraShake.shakeLevel = shakeLevel;
        }
       
        //HitStopController.instance.HitStop(shakeLevel, duration);
    }
}

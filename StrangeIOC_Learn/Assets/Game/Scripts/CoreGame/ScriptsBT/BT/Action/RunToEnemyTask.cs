using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class RunToEnemyTask : Action
{
    public SharedComponentManager componentManager;
    public SharedFloat rangeToEnemy;
    public string nameTrigger;

    public override void OnStart()
    {
        base.OnStart();
        
    }
    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
       float dir = componentManager.Value.enemy.position.x - componentManager.Value.transform.position.x;
        if (dir == 0) dir = 0.05f;
            rangeToEnemy.Value = dir;
        if (Mathf.Abs(rangeToEnemy.Value) > componentManager.Value.distanceChecEnemy*2f)
        {
            componentManager.Value.stateMachine.currentState = null;
            componentManager.Value.stateMachine.animator.SetTrigger(nameTrigger);
            componentManager.Value.rgbody2D.velocity = new Vector2(componentManager.Value.speedMove, 0f);
            return TaskStatus.Running;
        }

        return TaskStatus.Success;

    }
}

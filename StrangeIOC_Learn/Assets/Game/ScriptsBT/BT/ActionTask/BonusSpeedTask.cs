using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;
[TaskCategory("MyGame")]

public class BonusSpeedTask : Action
{
    public SharedComponentManager componentManager;
    public SharedString skillId;
    public SharedFloat scale;
    public SharedFloat duration;
    // Start is called before the first frame update
    public override TaskStatus OnUpdate()
    {
        if (!componentManager.Value.entity.hasMoveBonus)
        {
            Debug.Log("o");
            componentManager.Value.entity.AddMoveBonus(new List<MoveBonus>());
        }
        foreach(MoveBonus bonus in componentManager.Value.entity.moveBonus.moveBonuses)
        {
            if(bonus.ID == skillId.Value)
            {
                bonus.timer = bonus.duration;
                return TaskStatus.Success;
            }
        }
        MoveBonus skillSpeed = new MoveBonus();
        skillSpeed.duration = duration.Value;
        skillSpeed.timer = duration.Value;
        skillSpeed.ID = skillId.Value;
        skillSpeed.scale = scale.Value;
        componentManager.Value.entity.moveBonus.moveBonuses.Add(skillSpeed);
        return TaskStatus.Success;
    }
}

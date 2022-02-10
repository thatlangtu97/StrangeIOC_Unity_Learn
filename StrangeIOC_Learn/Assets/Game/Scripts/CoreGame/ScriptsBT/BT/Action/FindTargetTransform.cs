using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class FindTargetTransform : Action
    {
        public SharedComponentManager componentManager;
        public int frameUpdate = 5;
        public int frameCount;
        public override void OnStart()
        {
            base.OnStart();
        }
        public override TaskStatus OnUpdate()
        {
            frameCount += 1;
            if (componentManager.Value.enemy != null)
            {
                if (frameCount % frameUpdate != 0)
                {
                    if (Contexts.sharedInstance.game.playerFlagEntity == null)
                    {
                        componentManager.Value.enemy = null;
                        componentManager.Value.stateMachine.ChangeState(NameState.IdleState, 0, true);
                        componentManager.Value.speedMove = 0;
                        componentManager.Value.vectorSpeed=Vector2.zero;
                        return TaskStatus.Failure;
                    }
                }
                return TaskStatus.Success;
            }
            else
            {
                if (frameCount % frameUpdate != 0)
                {
                    return TaskStatus.Failure;
                }
                if (Contexts.sharedInstance.game.playerFlagEntity != null)
                {
                    componentManager.Value.enemy = Contexts.sharedInstance.game.playerFlagEntity.stateMachineContainer.stateMachine.transform;
                    return TaskStatus.Success;
                }
                else
                {
                    return TaskStatus.Failure;
                }
            }
        }
    }
}

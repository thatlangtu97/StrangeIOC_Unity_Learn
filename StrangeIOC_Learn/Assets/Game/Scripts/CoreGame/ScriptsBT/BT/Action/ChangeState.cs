using BehaviorDesigner.Runtime.Tasks;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class ChangeState : Action
    {
        public SharedComponentManager componentManager;
        public NameState nameState;
        public override void OnStart()
        {
            base.OnStart();
            componentManager.Value.stateMachine.ChangeState(nameState);

        }
    }
}
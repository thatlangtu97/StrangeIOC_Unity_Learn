namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("Tác vụ biến tần sẽ đảo ngược giá trị trả về của tác vụ con sau khi nó thực hiện xong." +
                      "Nếu đứa trẻ trả về thành công, tác vụ biến tần sẽ trả về lỗi. Nếu đứa trẻ trả về lỗi, tác vụ biến tần sẽ trả về thành công.")]
    [TaskIcon("{SkinColor}InverterIcon.png")]
    public class Inverter : Decorator
    {
        // The status of the child after it has finished running.
        private TaskStatus executionStatus = TaskStatus.Inactive;

        public override bool CanExecute()
        {
            // Continue executing until the child task returns success or failure.
            return executionStatus == TaskStatus.Inactive || executionStatus == TaskStatus.Running;
        }

        public override void OnChildExecuted(TaskStatus childStatus)
        {
            // Update the execution status after a child has finished running.
            executionStatus = childStatus;
        }

        public override TaskStatus Decorate(TaskStatus status)
        {
            // Invert the task status.
            if (status == TaskStatus.Success) {
                return TaskStatus.Failure;
            } else if (status == TaskStatus.Failure) {
                return TaskStatus.Success;
            }
            return status;
        }

        public override void OnEnd()
        {
            // Reset the execution status back to its starting values.
            executionStatus = TaskStatus.Inactive;
        }
    }
}
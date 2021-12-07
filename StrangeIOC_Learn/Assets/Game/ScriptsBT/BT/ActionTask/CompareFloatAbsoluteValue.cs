using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class CompareFloatAbsoluteValue : Conditional
{
    public enum Operation
    {
        LessThan,
        LessThanOrEqualTo,
        EqualTo,
        NotEqualTo,
        GreaterThanOrEqualTo,
        GreaterThan
    }

    public Operation operation;
    public SharedFloat float1;
    public SharedFloat float2;

    public override TaskStatus OnUpdate()
    {
        float value;
        value = Mathf.Abs(float1.Value);
        switch (operation)
        {
            case Operation.LessThan:
                return value < float2.Value ? TaskStatus.Success : TaskStatus.Failure;
            case Operation.LessThanOrEqualTo:
                return value <= float2.Value ? TaskStatus.Success : TaskStatus.Failure;
            case Operation.EqualTo:
                return value == float2.Value ? TaskStatus.Success : TaskStatus.Failure;
            case Operation.NotEqualTo:
                return value != float2.Value ? TaskStatus.Success : TaskStatus.Failure;
            case Operation.GreaterThanOrEqualTo:
                return value >= float2.Value ? TaskStatus.Success : TaskStatus.Failure;
            case Operation.GreaterThan:
                return value > float2.Value ? TaskStatus.Success : TaskStatus.Failure;
        }
        return TaskStatus.Failure;
    }

    public override void OnReset()
    {
        operation = Operation.LessThan;
        float1.Value = 0;
        float2.Value = 0;
    }
}
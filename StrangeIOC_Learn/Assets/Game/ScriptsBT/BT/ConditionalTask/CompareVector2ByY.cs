using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareVector2ByY : Conditional
{
    // Start is called before the first frame update
    public SharedVector2 valueToCompare;
    public SharedVector2 threshHoldForCompare;
    public CompareEvaluate compareEvaluate;
    public override TaskStatus OnUpdate()
    {
        switch(compareEvaluate)
        {
            case CompareEvaluate.GREATER:
                if(valueToCompare.Value.y>threshHoldForCompare.Value.y)
                {
                    return TaskStatus.Success;
                }

                break;
            case CompareEvaluate.EQUAL:
                if(valueToCompare.Value.y == threshHoldForCompare.Value.y)
                {
                    return TaskStatus.Success;
                }
                break;
            case CompareEvaluate.LESS:
                if (valueToCompare.Value.y < threshHoldForCompare.Value.y)
                {
                    return TaskStatus.Success;
                }

                break;

        }
        return TaskStatus.Failure;
    }

}
public enum CompareEvaluate
{
    GREATER,
    EQUAL,
    LESS
}


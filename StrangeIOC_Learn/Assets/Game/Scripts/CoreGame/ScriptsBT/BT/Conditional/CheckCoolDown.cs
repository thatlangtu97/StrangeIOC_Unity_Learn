using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class CheckCoolDown : Conditional
    {
        public int baseFrame;
        public int countFrame;
        public override TaskStatus OnUpdate()
        {
            if (countFrame < 0)
            {
                countFrame = baseFrame;
                return TaskStatus.Success;
            }
            else
            {
                countFrame -= 1;
                return TaskStatus.Failure;
            }
        }
    }
}

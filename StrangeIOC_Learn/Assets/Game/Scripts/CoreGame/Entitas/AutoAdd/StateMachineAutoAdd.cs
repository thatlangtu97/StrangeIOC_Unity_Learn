using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineAutoAdd : MonoBehaviour, IAutoAdd<GameEntity>
{
    public StateMachineController stateMachine;
    public void AddComponent(ref GameEntity e)
    {
        e.AddStateMachineContainer(stateMachine);
        stateMachine.InitStateMachine();
        //Destroy(this);
    }

    
}

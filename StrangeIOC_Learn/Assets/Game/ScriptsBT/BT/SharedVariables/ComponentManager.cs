using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ComponentManager : MonoBehaviour
{
    public bool isPlayer;
    public bool hasCheckEnemyInSigh;
    public Transform enemy;
    public bool isFaceRight = false;

    public StateMachineController stateMachine;

    private void Awake()
    {
        
    }
    private void Start()
    {
        
    }
    private void OnDestroy()
    {
    }
    public void OnInputChangeFacing()
    {
        if (isFaceRight == true)
        {
            isFaceRight = false;
        }
        else
        {
            isFaceRight = true;
        }
        if (isFaceRight)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

    }
}

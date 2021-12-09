using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ComponentManager : MonoBehaviour
{
    public Transform enemy;
    public bool hasCheckEnemyInSigh;
    public bool isFaceRight = false;
    public bool isAttack = false;
    public float durationAttack = 0;
    public StateMachineController stateMachine;
    public SkillConfigBehaviourTree skillConfig;
    public StepSkill CurrentStep;
    public BehaviorTree BehaviorTree;

    public int idCurrentSkill, nextIdSkill;
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
    public void CurrentSkill(int idSkill)
    {
        foreach (StepSkill temp in skillConfig.listStepSkill)
        {
            if (temp.idStep == idSkill)
            {
                CurrentStep = temp;
                idCurrentSkill = idSkill;
            }
        }
    }
    public void NextSkill(int idSkill)
    {
        
        foreach (StepSkill temp in skillConfig.listStepSkill)
        {
            if(temp.idStep == idSkill)
            {
                nextIdSkill = idSkill;
                CurrentStep = temp;
                
            }
        }
    }
}

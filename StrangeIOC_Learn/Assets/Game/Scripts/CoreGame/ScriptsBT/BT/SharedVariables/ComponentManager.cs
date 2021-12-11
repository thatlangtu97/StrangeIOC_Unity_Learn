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
    //public SkillConfigBehaviourTree skillConfig;
    //public StepSkill CurrentStep;
    public BehaviorTree BehaviorTree;
    public float timeScale=1f;
    public float speedMove = 1f;
    public float maxSpeedMove = 2f;
    public int idCurrentSkill, nextIdSkill;

    public int comboCount=0;
    public int nextCombo = 0;



    public Rigidbody2D rgbody2D;
    public bool isBufferAttack;
    [Range(0f,2f)]
    public float distanceCheckGround=0.1f;
    public bool isOnGround;
    public LayerMask layerMask;
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
            speedMove = Mathf.Abs(speedMove);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            speedMove = -Mathf.Abs(speedMove);
        }
    }
    //public void CurrentSkill(int idSkill)
    //{
    //    foreach (StepSkill temp in skillConfig.listStepSkill)
    //    {
    //        if (temp.idStep == idSkill)
    //        {
    //            CurrentStep = temp;
    //            idCurrentSkill = idSkill;
    //        }
    //    }
    //}
    //public void NextSkill(int idSkill)
    //{
        
    //    foreach (StepSkill temp in skillConfig.listStepSkill)
    //    {
    //        if(temp.idStep == idSkill)
    //        {
    //            nextIdSkill = idSkill;
    //            CurrentStep = temp;
                
    //        }
    //    }
    //}
    /// <summary>
    /// /////////////
    /// </summary>
    /// <returns></returns>
    public bool checkGround()
    {
        
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distanceCheckGround, layerMask);
            if (hit.collider != null)
            {
                isOnGround = true;
                return true;
            }
            else
            {
                isOnGround = false;
                return false;

            }

    }
    public void Rotate()
    {
        if (speedMove == maxSpeedMove)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (speedMove == -maxSpeedMove)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}

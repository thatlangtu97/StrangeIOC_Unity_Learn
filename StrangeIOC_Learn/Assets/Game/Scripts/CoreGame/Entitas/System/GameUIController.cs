using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public Joystick Joystick;
    public CameraFollow cameraFollow;

    public StateMachineController stateMachine;

    public LayerMask maskToolTest;

    private bool useRayCastTest;
    //public Button btnJump, btnDash, btnAttack, btnSkill1, btnSkill2;
    //public EventTrigger EJump, EDash, EAttack, ESkill1, ESkill2;
    [Button("MODIFY", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    void MODIFY()
    {
        
        Joystick.componentManager = stateMachine.componentManager;
        if (stateMachine.componentManager.BehaviorTree)
        {
            stateMachine.componentManager.BehaviorTree.enabled = false;
        }
        if(cameraFollow)
            cameraFollow.player = stateMachine.gameObject;
        //btnJump.onClick.RemoveAllListeners();
        //btnDash.onClick.RemoveAllListeners();
        //btnAttack.onClick.RemoveAllListeners();
        //btnSkill1.onClick.RemoveAllListeners();
        //btnSkill2.onClick.RemoveAllListeners();
        //btnJump.onClick.AddListener(Jump);
        //btnDash.onClick.AddListener(Dash);
        //btnAttack.onClick.AddListener(Attack);
        //btnSkill1.onClick.AddListener(Skill1);
        //btnSkill2.onClick.AddListener(Skill2);

        //EJump.OnPointerDown()
    }
    [Button("RAYCAST TEST", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    void RAYCASTTEST()
    {
        useRayCastTest = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Dash();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.End))
        {
            stateMachine.ChangeState(NameState.ReviveState, true);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Skill1();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Skill2();
        }
        
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    OnInputSkill(2);
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    OnInputSkill(3);
        //}
        
        if(Input.GetMouseButtonDown(0) && useRayCastTest )
            RayCastChangeObject();
    }
    private void Start()
    {
        MODIFY();
    }
    public void Jump()
    {
        stateMachine.OnInputJump();
    }
    public void Dash()
    {
        stateMachine.OnInputDash();
    }
    public void Attack()
    {
        stateMachine.OnInputAttack();
    }
    public void Skill1()
    {
        stateMachine.OnInputSkill(0);
    }
    public void Skill2()
    {
        stateMachine.OnInputSkill(1);
    }
    public void Skill(int idSkill)
    {
        stateMachine.OnInputSkill(idSkill);
    }

    public void RayCastChangeObject()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position ,(Camera.main.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition) ).normalized , 100f,maskToolTest);
        if(hit.collider != null)
        {
            Debug.Log ("Target Position: " + hit.collider.gameObject);
            if (hit.collider.gameObject.GetComponent<StateMachineController>() != null)
            {
                stateMachine = hit.collider.gameObject.GetComponent<StateMachineController>();
                MODIFY();
            }
        }
        Debug.DrawRay(Camera.main.transform.position , (Camera.main.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition) ).normalized  *100f,Color.blue);
    }
}

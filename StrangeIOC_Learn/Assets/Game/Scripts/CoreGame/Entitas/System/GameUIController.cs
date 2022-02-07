﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public Joystick Joystick;
    public StateMachineController stateMachine;

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
}

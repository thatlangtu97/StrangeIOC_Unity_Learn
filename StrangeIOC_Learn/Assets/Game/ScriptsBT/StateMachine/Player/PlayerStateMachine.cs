using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : StateMachineController
{
    public Transform petPosition1;
    public Transform petPosition2;
    public Sprite avatarImg;
    private void Awake()
    {

    }
    //IEnumerator Start()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    InitStates();
    //    SetUpInput();
    //    currentState = idleState;
    //    //doubleJumpCharge = HeroCreator.instance.jumpCharge;


    //}

    public override void InitStateMachine()
    {
        InitStates();
        SetUpInput();
        currentState = idleState;
        if (DataManager.Instance.CurrencyDataManager.saveStepTutorial == 0 && DataManager.Instance.CurrencyDataManager.isTutorial)
        {
            componentManager.entity.isImmortality = true;
        }
        //doubleJumpCharge = HeroCreator.instance.jumpCharge;
    }

    public override void UpdateState()
    {
        frameInput++;
        if (frameInput >= maxFrameInterval)
            frameInput = maxFrameInput;
        base.UpdateState();
    }

    protected override void InitStates()
    {
        //base.InitStates();
        CreateStateFactory(ref idleState);
        CreateStateFactory(ref moveState);
        CreateStateFactory(ref dashState);
        CreateStateFactory(ref dashAttack);
        //CreateStateFactory(ref dashFallState);
        CreateStateFactory(ref jumpFallState);
        CreateStateFactory(ref jumpState);
        CreateStateFactory(ref doubleJumpState);
        //CreateStateFactory(ref doubleJumpFallState);
        CreateStateFactory(ref dieState);
        CreateStateFactory(ref attackState);
        CreateStateFactory(ref skillState);
        CreateStateFactory(ref airAttackState);
        CreateStateFactory(ref knockDownState);

    }
    public override void SetUpInput()
    {
        base.SetUpInput();
        InputManager.instance.inputLeftDelegate += OnInputMoveLeft;
        InputManager.instance.inpputRightDelegate += OnInputMoveRight;
        InputManager.instance.inputStopDelegate += OnInputStopMove;
        InputManager.instance.inputJumpDelegate += OnInputJump;
        InputManager.instance.inputDashDelegate += OnInputDash;
        InputManager.instance.inputAttackDelegate += OnInputAttack;
        InputManager.instance.inputSkillDelegate += OnInputSkill;
        //InputManager.instance.inputDropDownDelegate += OnInputDropDown;
    }
    protected override void OnInputAttack()
    {
        base.OnInputAttack();
        /*Input by frame*/
        if (frameInput <= maxFrameInput)
            return;
        frameInput = 0;

        if (currentState != null)
            currentState.OnInputAttack();
    }
    protected override void OnInputDash()
    {
        base.OnInputDash();
        /*Input by frame*/
        if (frameInput <= maxFrameInput)
            return;
        frameInput = 0;

        if (currentState != null)
            currentState.OnInputDash();
    }
    protected override void OnInputMoveLeft()
    {
        base.OnInputMoveLeft();
        if (currentState != null)
            currentState.OnInputMoveLeft();
    }
    protected override void OnInputJump()
    {
        base.OnInputJump();
        /*Input by frame*/
        if (frameInput <= maxFrameInput)
            return;
        frameInput = 0;

        if (currentState != null)
            currentState.OnInputJump();

    }
    protected override void OnInputStopMove()
    {
        base.OnInputStopMove();
        if (currentState != null)
            currentState.OnInputStopMove();
    }
    protected override void OnInputMoveRight()
    {
        base.OnInputMoveRight();
        if (currentState != null)
            currentState.OnInputMoveRight();
    }
    protected override void OnInputSkill(int skillId)
    {
        base.OnInputSkill(skillId);
        currentState.OnInputSkill(skillId);
    }
    public override void KnockDown()
    {

        base.KnockDown();
    }
    public override void OnRunOutOfHealth(GameObject playerObject)
    {
        base.OnRunOutOfHealth(playerObject);
    }
    public override void OnHit(bool isBlock, bool isNotKnock)
    {
        base.OnHit(isBlock, isNotKnock);
       // animator.SetTrigger(AnimationName.HIT);

    }
    public override void ChangeState(State newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void OnInputDropDown()
    {
        if (currentState != null)
            currentState.OnDropDown();
    }
}

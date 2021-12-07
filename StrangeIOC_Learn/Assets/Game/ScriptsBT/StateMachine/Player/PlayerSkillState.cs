using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Skill State", menuName = "State/Player/Skill")]
public class PlayerSkillState : State
{
    public int curSkillID;
    public bool noInterupt;
    [SerializeField]
    bool bufferredSkill;
    float startTime;
    // Start is called before the first frame update

    public override void EnterState()
    {
        base.EnterState();
       
        startTime = Time.time;
        controller.componentManager.entity.moveByDirection.direction = Vector2.zero;
        controller.componentManager.entity.rigidbodyContainer.rigidbody.gravityScale = 3.5f;
        controller.componentManager.entity.isSkillComplete = false;
        //controller.componentManager.entity.groundCombo.attackBuffer = false;
        controller.componentManager.entity.isStopMove = true; 
        
        //Debug.Log("Enter skill state: " + controller.componentManager.entity.isStopMove);
        //controller.animator.SetBool(AnimationName.IS_ON_ATTACK, true);

        controller.animator.SetBool(AnimationName.MOVE, false);
        //controller.componentManager.entity.rigidbodyContainer.rigidbody.mass = 9999;
        // controller.animator.SetTrigger(AnimationName.IDLE);
        //controller.animator.SetLayerWeight((int) LayerWeightID.SKILL, 1f);

    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        //Debug.Log("Is in skill state");
        //Debug.Log("SkillComplete: " + controller.componentManager.entity.isSkillComplete);
        if (controller.componentManager.entity.isSkillComplete)
        {
            if (bufferredSkill)
            {
                OnPlayerSkill(curSkillID);
            }
            else
            {
                if (controller.componentManager.entity.health.HP.Value > 0)
                {
                    if (controller.componentManager.entity.checkGround.isOnGround)
                    {
                        controller.ChangeState(controller.idleState);
                    }
                    else
                    {
                        controller.ChangeState(controller.jumpFallState);
                    }
                }
                else
                {
                    controller.ChangeState(controller.dieState);
                }
            }
            // controller.animator.SetLayerWeight((int)LayerWeightID.SKILL, 0f);
            
            controller.componentManager.entity.isSkillComplete = false;


            return;
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        bufferredSkill = false;
       // controller.componentManager.entity.rigidbodyContainer.rigidbody.mass = 1;
        controller.componentManager.entity.isCanMoveOnSkillState = false;
        controller.componentManager.entity.isCanInputJumpOnSkillState = false;
        //controller.animator.SetBool(AnimationName.IS_ON_ATTACK, false);
    }
    /*
    public override TaskStatus OnInputAttack()
    {
        Debug.Log("AttackOnSkill");
        base.OnInputAttack();
        //bufferAttack = true;
        ExitState();
        controller.componentManager.entity.skillContainer.BehaviorTrees[curSkillID].DisableBehavior();
        controller.ChangeState(controller.attackState);
        if (controller.componentManager.entity.health.HP.Value > 0)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }*/
    /*
    public override TaskStatus OnInputMoveLeft()
    {
        base.OnInputMoveLeft();
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            if (controller.componentManager.entity.isCanMoveOnSkillState)
            {
                if (controller.characterDirection.isFaceRight)
                {
                    controller.characterDirection.ChangeDirection();
                }
                //controller.componentManager.entity.isStopMove = false;

                //controller.componentManager.entity.moveByDirection.direction = Vector2.left;
            }
        }
        return TaskStatus.Success;
    }
    public override TaskStatus OnInputMoveRight()
    {
        base.OnInputMoveRight();
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            if (controller.componentManager.entity.isCanMoveOnSkillState)
            {
                if (!controller.characterDirection.isFaceRight)
                {
                    controller.characterDirection.ChangeDirection();
                }
                //controller.componentManager.entity.isStopMove = false;
                //controller.componentManager.entity.moveByDirection.direction = Vector2.right;
            }
        }
        return TaskStatus.Success;
    }*/
    public override TaskStatus OnInputStopMove()
    {
        base.OnInputStopMove();
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            if (controller.componentManager.entity.isCanMoveOnSkillState)
            {
                controller.componentManager.entity.isStopMove = true;
            }
        }
        return TaskStatus.Success;
    }
    public override TaskStatus OnInputJump()
    {
        /*
        base.OnInputJump();
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
          
            if (controller.componentManager.entity.checkGround.isOnGround)
            {
                ExitState();
                controller.componentManager.entity.skillContainer.BehaviorTrees[curSkillID].DisableBehavior();
                //controller.componentManager.entity.jump.isEnable = true;
                controller.ChangeState(controller.jumpState);
            }

            if (!controller.componentManager.entity.checkGround.isOnGround && controller.doubleJumpCharge > 0)
            {
                ExitState();
                controller.componentManager.entity.skillContainer.BehaviorTrees[curSkillID].DisableBehavior();
                //controller.componentManager.entity.jump.isEnable = true;
                controller.ChangeState(controller.doubleJumpState);
            }
        }*/
        return TaskStatus.Success;

    }
    public override TaskStatus OnInputSkill(int skillId)
    {
        if(skillId == curSkillID)
        {
            if (controller.componentManager.entity.health.HP.Value > 0)
            {
                bufferredSkill = true;
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
           
        }
        else
        {
            return TaskStatus.Failure;
        }    
    }
    public override TaskStatus OnKnockDown()
    {
        return TaskStatus.Failure;
    }

    public override void OnForceExitState()// fix tam ket skill state
    {
        if(Time.time - startTime > 0.1f)
        {
            if (controller.componentManager.entity.health.HP.Value > 0)
            {
                if (controller.componentManager.entity.checkGround.isOnGround)
                {
                    controller.ChangeState(controller.idleState);
                }
                else
                {
                    controller.ChangeState(controller.jumpFallState);
                }
            }
            else
            {
                controller.ChangeState(controller.dieState);
            }
            controller.componentManager.entity.isSkillComplete = false;
            //Debug.Log("force");
            controller.componentManager.entity.skillContainer.BehaviorTrees[curSkillID].DisableBehavior();
        }      
    }

    public override TaskStatus OnInputDash()
    {
        /*
        ExitState();
        Debug.Log("cancelSkill");
        controller.componentManager.entity.skillContainer.BehaviorTrees[curSkillID].DisableBehavior();
        if (!controller.componentManager.entity.checkGround.isOnGround)
            controller.rollOnSkillAir = true;
        base.OnInputDash();*/
        return TaskStatus.Success;
    }

}


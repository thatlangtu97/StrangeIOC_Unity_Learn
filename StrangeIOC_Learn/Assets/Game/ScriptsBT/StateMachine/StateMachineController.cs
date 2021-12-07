using BehaviorDesigner.Runtime;
using Entitas.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class StateMachineController : MonoBehaviour
{

    public State idleState;
    public State moveState;
    public State jumpState;
    public State attackState;
    public State skillState;
    public State jumpFallState;
    public State doubleJumpState;
    public State knockDownState;
    public State airAttackState;
    public State dashState;
    public State dieState;
    public State dashAttack;
    //public State doubleJumpFallState;
    //public State dashFallState;
    //public State chaseState;
    public State beHitState;   
    public State getUpState; 
    //public State forceJumpState;
    //public State revivalState;
    public State freezeState;
    public State stuntState;
    public ComponentManager componentManager;
    public Animator animator;
    public SkillContainer skillContainer;
    public Transform centerPoint;
    public CharacterDirection characterDirection;
    
    public AttackSoundController attackSoundController;
    public MovementSoundController movementSoundController;
    public EffectSoundController effectSoundController;
    //  protected IComponentManager componentManager;
    [SerializeField]
    public State currentState;
    public State previousState;
    
    public BehaviorTree behaviorTree;
    public Action<GameObject> onRunOutOfHealthAction;
    public UnityEvent onHitAction;
    public UnityEvent onDieAction;

    [Header("ComboDataConfig")]
    public ComboDataConfig groundComboData;
    public ComboDataConfig airComboData;
    public ComboDataConfig dashComboData;

    [Header("CharacterMovement Config")]
    public float dashDuration;
    public float dashPower;
    public float dashGravity;
    public AddForceOnMoveSetConfig forceOnEvent;
    public PlayerShootsProjectileOnEvent shootEvent;
    public EventAnimationToStateMachine eventOnAnimation;
    public PlayerAddOn addOn;
    public BufferInput buffer;
    public int doubleJumpCharge = 1;
    public int jumpAttackCharge = 1;
    public int maxJumpAttackCharge;
    public int rollCharge = 1;

    [Header("EnemyMovement Config")]
    public FireProjectileCommandEmit shootCommand;
    public EnemyJumpConfig enemyJumpConfig;

    [Header("Character Physics Body")]
    public Detectable bodyEffect;
    public GameObject softBody;
    public BoxCollider2D bodyCollider;
    [HideInInspector]
    public RaycastHit2D contactedPlatform;
    public LayerMask platFormCastMask;
    
  
    public int curCombo;
    public int numbertOfPiercingTarget;

    [Header("Character body status")]
    public bool isHit;
    public bool immuneHit;
    public bool immuneKnock;
    public bool isCanRotate; 
    public bool isImmuneStun;
    public bool isFreezeAndImmumeToForce;

    [Header("Character body effect")]
    public MeshRenderer meshRenderer;
    public bool noHitStop;

    [HideInInspector]
    public GameObject allFxOfSkillChanelling;
    public float beHitTime;
    //public bool rollOnSkillAir;
    public bool useEffectDie = true;
    public bool useHideGameObject = true;
    public int forceDie;
    public int CountAnimHit;

    protected int frameInput;
    public int maxFrameInterval;
    public int maxFrameInput;
   

    public int index;
    public string namePrefab;

    [Header("Visual Effect Config")]
    public ShakeCameraOnEvent cameraShake;
    public bool suspendAnimationEvent;
    public GameObject freezeEffect;
    public GameObject burnEffect; // burn dang particle system
    public GameObject frostEffect;
    public GameObject shockEffect;
    [Header("Shield")] public int ShieldHP;
    
    public void SpawnEffect(EffectType type)
    {
        switch (type) {
            case EffectType.Burn:
                if (burnEffect == null)
                {
                    burnEffect = ObjectPool.Spawn(GameSceneConfig.instance.burnEffect, componentManager.entity.centerPoint.centerPoint , Vector2.zero, GameSceneConfig.instance.burnEffect.transform.rotation);
                }              
                break;
            case EffectType.Freeze:
                if (currentState == freezeState)
                {
                    if (meshRenderer != null)
                        meshRenderer.enabled = false;
                    if (freezeEffect != null)
                    {
                        freezeEffect.gameObject.SetActive(true);
                        Vector3 scale = freezeEffect.gameObject.transform.localScale;
                        if (characterDirection.isFaceRight)
                        {
                            scale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
                        }
                        else
                        {
                            scale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
                            //Debug.Log("left");
                        }

                        freezeEffect.transform.localScale = scale;
                    }
                }
                break;
            case EffectType.Frost:
                if (frostEffect == null)
                {
                    frostEffect = ObjectPool.Spawn(GameSceneConfig.instance.frostEffect, componentManager.entity.centerPoint.centerPoint, Vector2.zero, GameSceneConfig.instance.frostEffect.transform.rotation);
                }            
                break;
            case EffectType.Shock:
                if (shockEffect == null)
                {
                    shockEffect = ObjectPool.Spawn(GameSceneConfig.instance.shockEffect, componentManager.entity.centerPoint.centerPoint, Vector2.zero, GameSceneConfig.instance.burnEffect.transform.rotation);
                }
                break;
        }
    }
    public void DestroyEffect(EffectType type) {
        switch (type)
        {
            case EffectType.Burn:
                if (burnEffect != null)
                {
                    ObjectPool.Recycle(burnEffect);
                    burnEffect = null;
                }
                break;
            case EffectType.Freeze:
                if (meshRenderer != null)
                    meshRenderer.enabled = true;
                if (freezeEffect != null)
                    freezeEffect.gameObject.SetActive(false);
                break;
            case EffectType.Shock:
                if (shockEffect != null)
                {
                    ObjectPool.Recycle(shockEffect);
                    shockEffect = null;
                }
                break;
            case EffectType.Frost:
                if (frostEffect != null)
                {
                    ObjectPool.Recycle(frostEffect);
                    frostEffect = null;
                }
                break;
        }
    }
   
    public void SetFxOFSkillChanelling(GameObject go)
    {
        allFxOfSkillChanelling = go;
    }
    public virtual void SetUpInput()
    {
        //if (inputManager != null)
        //{
        //    inputManager.AddCmdMoveRightListener(OnInputMoveRight);
        //    inputManager.AddCmdMoveLeftListener(OnInputMoveLeft);
        //    inputManager.AddCmdStopMoveListener(OnInputStopMove);
        //    inputManager.AddCmdJumpListener(OnInputJump);
        //    inputManager.AddCmdAttackListener(OnInputAttack);
        //    inputManager.AddCmdSkillListener(OnInputSkill);
        //    inputManager.AddCmdToolListener(OnInputTool);
        //    inputManager.AddCmdDartsListener(OnInputDarts);
        //    inputManager.AddCmdDashListener(OnInputDash);
        //    inputManager.AddCmdAiModeListener(componentManager.SwitchAiMode);
        //}
    }
    //void Awake()
    //{
    //    SetUpInput();
    //    InitStates();
    //    AnimatorControllerParameter[] parammeter = animator.parameters;
    //    if (parammeter.Length != 0)
    //    {
    //        foreach (AnimatorControllerParameter temp in parammeter)
    //        {
    //            if(temp.name == "Hit2")
    //            {
    //                CountAnimHit = 2;
    //                break;
    //            }
    //        }
    //    }
    //}
    //void Start()
    //{

    //    //yield return new WaitForSeconds(0.2f);
    //    currentState = idleState;
    //    allFxOfSkillChanelling = null;
    //}

    public virtual void InitStateMachine()
    {
        SetUpInput();
        InitStates();
        AnimatorControllerParameter[] parammeter = animator.parameters;
        if (parammeter.Length != 0)
        {
            foreach (AnimatorControllerParameter temp in parammeter)
            {
                if (temp.name == "Hit2")
                {
                    CountAnimHit = 2;
                    break;
                }
            }
        }
        currentState = idleState;
        allFxOfSkillChanelling = null;
    }

    public virtual void UpdateState()
    {
        if (currentState != null)
        {
            currentState.UpdateState(Time.deltaTime);
            
        }

    }
    public virtual void OnSpawn()
    {
        // throw new NotImplementedException();
    }

    public virtual void OnRevival()
    {
        //  throw new NotImplementedException();
    }
    public virtual void OnRunOutOfHealth(GameObject minion)
    {
        //if(currentState != dieState)
        //    currentState.ExitState();
        ChangeState(dieState);
        try
        {
            if (onRunOutOfHealthAction != null)
                onRunOutOfHealthAction.Invoke(gameObject);
            if(onDieAction!=null)
            {
                onDieAction.Invoke();
            }
        }
        catch(Exception ex)
        {
            //Debug.LogError(ex.StackTrace);
        }
    }

    public virtual void DisableOnDead()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnHit(bool isBlock, bool isNotKnock)
    {
        if(isBlock)
        {
            animator.SetTrigger(AnimationName.BLOCK);
            return;
        }

        if(isNotKnock)
        {
            return;
        }

        onHitAction.Invoke();
        if(immuneHit)
        {
            return;
        }
        if ((beHitState != null && currentState != null))
        {
            currentState.OnHit(isBlock);
        }else
        {
            animator.SetTrigger(AnimationName.HIT);
        }
    }

    protected virtual void EnterStateOnSpawn()
    {
        //  currentState.EnterState();
    }
    protected virtual void InitStates()
    {
        CreateStateFactory(ref idleState);
        CreateStateFactory(ref moveState);
        //CreateStateFactory(ref chaseState);
        CreateStateFactory(ref jumpState);
        CreateStateFactory(ref jumpFallState);
        CreateStateFactory(ref attackState);
        CreateStateFactory(ref beHitState);
        CreateStateFactory(ref dieState);
        CreateStateFactory(ref knockDownState);
        CreateStateFactory(ref freezeState);
        CreateStateFactory(ref skillState);
        CreateStateFactory(ref stuntState);
    }
    protected void CreateStateFactory(ref State stateToClone)
    {
        if (stateToClone != null)
        {
            stateToClone = Instantiate(stateToClone);
            stateToClone.InitState(this);

        }

    }
    public virtual void ChangeState(State newState)
    {     
       if(currentState!=dieState )
       {
            if(currentState != null)
                currentState.ExitState();

            if (newState == null)
                newState = idleState;
            currentState = newState;
            currentState.EnterState();
           
       }
    }

   

    protected virtual void OnInputAttack()
    {
        // currentState.OnInputAttack();
    }

    protected virtual void OnInputJump()
    {
        // currentState.OnInputJump();
    }

    protected virtual void OnInputMoveLeft()
    {
        // currentState.OnInputMoveLeft();
    }

    protected virtual void OnInputMoveRight()
    {
        // currentState.OnInputMoveRight();
    }

    protected virtual void OnInputStopMove()
    {
        //  currentState.OnInputStopMove();
    }

    protected virtual void OnInputSkill(int skillId)
    {
        //  currentState.OnTriggerSkill(characterSkills[skillId]);
    }

    protected virtual void OnInputDarts()
    {
        //  currentState.OnTriggerSkill(characterDartsSkill);
    }

    protected virtual void OnInputDash()
    {
        // throw new NotImplementedException();
    }

    public virtual void OnInputTool()
    {
        // currentState.OnInputTool();
    }

    public virtual void OnFinishCastingSkill(bool isSkill = false)
    {
        //  ChangeState(StateMachine.idle);
    }

    public virtual void UpdateState(float deltaTime)
    {
        // CheckForOpponentInTheSight();
        //  currentState.UpdateState(deltaTime);
    }

    public virtual void Attack()
    {
        // characterAttacks[0].BeTriggered();
    }

    public virtual void Reborn()
    {
        // ChangeState(StateMachine.idle);
    }

    protected virtual void CheckForOpponentInTheSight()
    {

    }
    public void GetUp()
    {
        currentState.OnGetUp();
    }
    
    public virtual void KnockDown()
    {

        
        if(immuneKnock)
        {
            return;
        }
        if (currentState != null)
        {
            //Debug.Log("here 2");
            currentState.OnKnockDown();
        }


    }
    public virtual void Freeze(float duration)
    {
        //if (currentState != freezeState && currentState!=knockDownState&& freezeState!=null)
        //{
        //    ChangeState(freezeState);
        //}
        //Debug.Log(gameObject);
        //Debug.Log(currentState.name);
        
        if(currentState != null)
        {
            currentState.OnFrezee(duration);
        }
        
    }

    public virtual void OnForceExitState()
    {
        if(currentState!= null)
        currentState.OnForceExitState();
    }

    public void HideGameObject(bool hideObj = true) {
        StartCoroutine(DelayHideObject(hideObj));
    }
    IEnumerator DelayHideObject(bool hideObj)
    {
        yield return new WaitForEndOfFrame();

        if (hideObj)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject, 2f);
        }
    }

}

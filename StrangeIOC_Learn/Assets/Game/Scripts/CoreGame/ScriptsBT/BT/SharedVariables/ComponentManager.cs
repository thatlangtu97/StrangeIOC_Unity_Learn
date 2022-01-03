using BehaviorDesigner.Runtime;
using Entitas.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ComponentManager : MonoBehaviour
{
    public Transform enemy;
    public StateMachineController stateMachine;
    public BehaviorTree BehaviorTree;
    public Rigidbody2D rgbody2D;
    public LayerMask layerMaskGround,layerMaskWall,layerEnemy;
    public ComponentProperties properties;
    public MeshRenderer meshRenderer;
    //[HideInInspector]
    public GameEntity entity;
    public EntityLink link;
    public bool hasCheckEnemyInSigh;
    public bool isFaceRight = false;
    public bool isAttack = false;
    public bool isOnGround;
    public bool isBufferAttack;
    [Range(0f,2f)]
    public float distanceCheckGround=0.1f;
    [Range(0f, 2f)]
    public float distanceCheckWall = 0.1f;
    [Range(0f, 2f)]
    public float distanceChecEnemy = 0.1f;
    public float timeScale = 1f;
    public float speedMove = 1f;
    public float maxSpeedMove = 2f;
    public int jumpCount;
    public int dashCount;
    public int attackAirCount;
    public int maxJump,maxDash, maxAttackAirCount;

    
    private void Awake()
    {
        // var components = GetComponentsInChildren<IAutoAdd<GameEntity>>();
        //foreach (var component in components)
        //{
        //    component.AddComponent(ref entity);
        //}
    }
    public void OnEnable()
    {
        entity = Contexts.sharedInstance.game.CreateEntity();
        link = gameObject.Link(entity);
        var component = GetComponent<IAutoAdd<GameEntity>>();
        component.AddComponent(ref entity);
        ComponentManagerUtils.AddComponent(this);
    }
    private void OnDisable()
    {
        DestroyEntity();
    }
    public void OnInputChangeFacing()
    {
        if (enemy.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            //speedMove = -maxSpeedMove;
        }
        else if (enemy.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            //speedMove = maxSpeedMove;
        }
    }
    
    
    public void ResetJumpCount()
    {
        jumpCount = 0;
    }
    public void ResetDashCount()
    {
        dashCount = 0;
    }
    public void ResetAttackAirCount()
    {
        attackAirCount = 0;
    }
    public bool CanJump
    {
        get { return jumpCount < maxJump; }
    }
    public bool CanDash
    {
        get { return dashCount < maxDash; }
    }
    public bool CanAttackAir
    {
        get { return attackAirCount < maxAttackAirCount; }
    }
    public bool checkGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distanceCheckGround, layerMaskGround);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool checkWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1,0)* transform.localScale.x, distanceCheckWall, layerMaskWall);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool checkEnemyForwark()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1, 0) * transform.localScale.x, distanceChecEnemy, layerEnemy);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
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
    public void DestroyEntity()
    {
        gameObject.Unlink();
        entity.Destroy();
        link = null;
    }
    /// <summary>
    /// UpdateWorldTransform() in Script 
    /// SkeletonAnimation.cs
    /// SkeletonMecanim.cs
    /// Skeleton.cs
    /// SkeletonGraphic.cs
    /// </summary>
    //public Spine.Unity.SkeletonMecanim mecanim;
    //[Header("Update Skeleton")]
    //public bool UpdateWorldTransform;
    //public void UpdateMecanim()
    //{
    //    if (UpdateWorldTransform)/* return;*/
    //    {
    //        mecanim.skeleton.UpdateCache();
    //        mecanim.skeleton.UpdateWorldTransform();
    //    }
    //}
}

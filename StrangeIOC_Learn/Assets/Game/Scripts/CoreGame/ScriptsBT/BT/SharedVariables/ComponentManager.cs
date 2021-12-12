using BehaviorDesigner.Runtime;
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
    public LayerMask layerMask;

    public bool hasCheckEnemyInSigh;
    public bool isFaceRight = false;
    public bool isAttack = false;
    public float timeScale=1f;
    public float speedMove = 1f;
    public float maxSpeedMove = 2f;
    public bool isBufferAttack;
    [Range(0f,2f)]
    public float distanceCheckGround=0.1f;
    public bool isOnGround;
    public int jumpCount;
    public int dashCount;
    public int maxJump,maxDash;

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
    /// <summary>
    /// /////////////
    /// </summary>
    /// <returns></returns>
    public void ResetJumpCount()
    {
        jumpCount = 0;
    }
    public void ResetDashCount()
    {
        dashCount = 0;
    }
    public bool CanJump
    {
        get { return jumpCount < maxJump; }
    }
    public bool CanDash
    {
        get { return dashCount < maxDash; }
    }
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

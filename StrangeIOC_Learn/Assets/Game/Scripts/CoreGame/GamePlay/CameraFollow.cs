using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Animator cameraAnim;
    public static CameraFollow instance;
    public float offSetX;
    public float offSetY;
    public float offSetZ;
    public bool hasTarget;
    public bool chasing;
    public int level;
    public Vector2 offScreenLimit;

    [SerializeField]
    Vector2 boxPoint;
    [SerializeField]
    bool isStop;

    [SerializeField]
    float speedOffset;

    [SerializeField]
    Vector2 posOffset;

    [SerializeField]
    float timeOffset;

    [SerializeField]
    float leftLimit;

    [SerializeField]
    float rightLimit;

    [SerializeField]
    float topLimit;

    [SerializeField]
    float bottomLimit;

    [SerializeField]
    Vector2 windowCamera;

    [SerializeField]
    float scaleOffset;
    private Vector3 velocity;

    [SerializeField]
    Vector3 right;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        offScreenLimit = new Vector2(Screen.width / 1920 * 5.6f, Screen.height / 1080 * 3);
        offSetZ = -10f;
        scaleOffset = .5f;
       
        //StartCoroutine(SetPlayer());
    }

    // Update is called once per frame
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere( player.transform.position+ new Vector3(0,posOffset.y,0) + player.transform.right.normalized * scaleOffset , 0.05f);
    //}
    void FixedUpdate()
    {
        if(hasTarget)
        {
            if (chasing)
            {
                Vector3 startPos = transform.position;
                right = player.transform.right.normalized * scaleOffset;
                Vector3 endPos = player.transform.position + right;
                
                endPos.x += posOffset.x;
                endPos.y += posOffset.y;
                endPos.z = offSetZ;

                transform.position = Vector3.SmoothDamp(startPos, endPos, ref velocity, timeOffset);
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                    Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
                    transform.position.z
                    );
            }
        }        
    }

    IEnumerator SetPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        //player = Contexts.sharedInstance.game.playerFlagEntity.stateMachineContainer.stateMachine.gameObject;
    }

    public bool checkOffScreen(Vector2 pos)
    {
        if (pos.x > transform.position.x + offScreenLimit.x || pos.x < transform.position.x - offScreenLimit.x || pos.y > transform.position.y + offScreenLimit.y)
        {
            return true;
        }
        else
            return false;
    }
    public void SetTarget(GameObject player)
    {
        this.player = player;
        hasTarget = true;
    }
}

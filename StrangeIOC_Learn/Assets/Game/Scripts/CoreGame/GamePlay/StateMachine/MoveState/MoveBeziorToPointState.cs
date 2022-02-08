using System.Collections;
using System.Collections.Generic;
using DesperateDevs.Logging;
using UnityEngine;
[CreateAssetMenu(fileName = "MoveBeziorToPointState", menuName = "CoreGame/State/MoveBeziorToPointState")]
public class MoveBeziorToPointState : State
{
    public Vector3 endPosition = Vector3.zero;
    public Vector3 startPosition = Vector3.zero;
    public Vector3 startRangeDistance = Vector3.zero;
    public Vector3 endRangeDistance = Vector3.zero;
    public Vector3 dirStartMove = Vector3.zero;
    public bool startMove = false;
    public float tParam = 0;
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        startPosition = controller.transform.position;
        FindEndPosition();
        startMove = true;
        tParam = 0;
        dirStartMove.x = Mathf.Abs(dirStartMove.x) * controller.componentManager.speedMove>=0? 1:-1 ;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        controller.componentManager.Rotate();
        if (startMove)
        {
            controller.componentManager.rgbody2D.position = CaculatePosition();
        }
        
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private void FindEndPosition()
    {
        if (controller.componentManager.enemy)
        {
            endPosition = controller.componentManager.enemy.position + RandomDistance();
        }
        else
        {
            endPosition = controller.transform.position + RandomDistance();
            endPosition = new Vector3(Mathf.Abs(endPosition.x)*controller.componentManager.speedMove,endPosition.y,endPosition.z);
        }
    }

    private Vector3 RandomDistance()
    {
        return new Vector3(    
            Random.Range(startRangeDistance.x, endRangeDistance.x),
            Random.Range(startRangeDistance.y, endRangeDistance.y),
            Random.Range(startRangeDistance.z, endRangeDistance.z)
        );
    }
    private Vector3 CaculatePosition()
    {            
        Vector3 temp = Mathf.Pow(1 - tParam, 3) * startPosition +
                                3 * Mathf.Pow(1 - tParam, 2) * tParam * dirStartMove +
                                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * dirStartMove +
                                Mathf.Pow(tParam, 3) * endPosition;
        tParam += Time.deltaTime;
        if (tParam > 1f)
        {
            startMove = false;
            controller.ChangeState(NameState.IdleState);
        }
        return temp;
    }
}

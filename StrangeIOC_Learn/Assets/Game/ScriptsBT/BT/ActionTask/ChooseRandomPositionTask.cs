using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRandomPositionTask : Action
{
    public SharedFloat minRandomX, maxRandomX, minRandomY, maxRandomY;
    public SharedVector2 storedValue;
    public override void OnStart()
    {

        var tempValue = new SharedVector2();
        int breakCount = 0;
        Vector2 newPos = Vector2.zero;
        do
        {
            breakCount++;
            if(breakCount>10)
            {
                break;
            }
            float directionX = Random.Range(0, 100) > 50 ? 1f : -1f;
            float directionY = Random.Range(0, 100) > 50 ? 1f : -1f;
            newPos = new Vector2(transform.position.x + directionX * Random.Range(minRandomX.Value, maxRandomX.Value), transform.position.y + directionY* Random.Range(minRandomY.Value, maxRandomY.Value));
        }
        while (newPos.x < LevelCreator.instance.map.leftAnchor.position.x+0.5f || newPos.x > LevelCreator.instance.map.rightAnchor.position.x - 0.5f);
            tempValue.Value = newPos;

        storedValue.SetValue(tempValue.Value);
       // transform.position = storedValue.Value;
        //Debug.Log(storedValue.Value + " is Destination");
    }
}

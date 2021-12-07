using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ChaseTask : Action
{
    public SharedComponentManager component;
   
    public Vector3 offset;
    public int randomXRange;
    public int randomYRange;
    Transform target;
    public float minDistance;
    public override void OnStart()
    {
        offset.x = Random.Range(-randomXRange, randomXRange);
        offset.y = Random.Range(0, randomYRange);
       
        target = component.Value.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint;
       
    }
    public override TaskStatus OnUpdate()
    {
        component.Value.entity.moveByDestination.destination = target.position + offset;
        /*
        if(Vector2.Distance(transform.position, target.position) < minDistance)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
        */
        
        if (transform.position == (target.position + offset))
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
        
    }
}
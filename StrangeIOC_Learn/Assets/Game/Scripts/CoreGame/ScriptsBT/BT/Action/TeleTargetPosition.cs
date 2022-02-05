using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class TeleTargetPosition : Action
    {
        public SharedComponentManager componentManager;
        public bool plantGround;
        public LayerMask layerCast;
        public Vector3 target;
        public LayerMask layerLimit;
        public float rangeTele;
        public float minX, maxX;
        public override void OnStart()
        {
            base.OnStart();
            FindLimitTele();

            if (plantGround)
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(componentManager.Value.stateMachine.transform.position, Vector2.down, 100f, layerCast);
                if (raycastHit2D.collider != null)
                {
                    float y = raycastHit2D.point.y;
                    float x = FindRandomPosition();
                    target = new Vector3(x,y);
                    componentManager.Value.stateMachine.transform.position = target;
                }
            }
            else
            {

            }
        }
        private void FindLimitTele()
        {
            RaycastHit2D raycastHitMin = Physics2D.Raycast(componentManager.Value.stateMachine.transform.position, Vector2.left, 1000f, layerLimit);
            RaycastHit2D raycastHitMax = Physics2D.Raycast(componentManager.Value.stateMachine.transform.position, Vector2.right, 1000f, layerLimit);
            if (raycastHitMin.collider != null)
            {
                minX = raycastHitMin.point.x;
            }
            if (raycastHitMax.collider != null)
            {
                maxX = raycastHitMax.point.x;
            }
        }
        private float FindRandomPosition()
        {
            float value = Random.Range(minX, maxX);
            return value;
        }
    }
}

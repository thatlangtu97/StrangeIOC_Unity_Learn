using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrajectoryMovement : ProjectileMovement
{
    [Header("Properties")]
    public TrailRenderer trail;
    public float timeDelay;
    public Transform enemyTransform;
    public Transform startTransform;
    float deltaTime = 0;
    float tParam;
    float countTimeDelay;
    public Vector3 directionStart = Vector3.zero;
    public Vector3 directionEnd = Vector3.zero;
    [Range(2f, 10f)]
    public float radiusCastEnemy;
    public LayerMask layerMaskEnemy;
    Vector3 endPosition = Vector3.zero;
    Vector3 startPosition = Vector3.zero;

    Vector3 dirStartMove, dirEndMove;
    Vector3 projectilePosition = Vector3.zero;
    [SerializeField]
    Collider2D[] cols;
    public void OnEnable()
    {
        countTimeDelay = timeDelay;
        tParam = 0;
        trail.enabled = false;
        FindEnemy();
    }
    public void FindEnemy()
    {
        cols = Physics2D.OverlapCircleAll(transform.position, radiusCastEnemy, layerMaskEnemy);

        if (cols != null)
        {
            foreach (var col in cols)
            {
                if (col != null)
                {
                    enemyTransform = col.transform;
                    break;
                }
            }
        }
    }
    public override void UpdatePosition()
    {
        startPosition = startTransform.position;
        if (enemyTransform != null)
            endPosition = enemyTransform.position;

        if (endPosition.x > startPosition.x)
        {
            dirStartMove = startPosition + directionStart;
            dirEndMove = startPosition + directionEnd;
        }
        else
        {
            dirStartMove = startPosition + new Vector3(directionStart.x * -1f, directionStart.y, directionStart.z);
            dirEndMove = startPosition + new Vector3(directionEnd.x * -1f, directionEnd.y, directionEnd.z);
        }


        transform.position = projectilePosition;
        if (countTimeDelay > 0)
        {
            countTimeDelay -= Time.fixedDeltaTime;
            deltaTime = 0;
            trail.enabled = false;
        }
        else
        {
            trail.enabled = true;
            deltaTime = Time.fixedDeltaTime;
        }
    }
    public override void CaculatePosition()
    {
        if (tParam <= 1f)
        {
            if (tParam >= .5f)
            {
                tParam += deltaTime;
            }
            else
            {
                tParam += deltaTime * 2f;
            }
            projectilePosition = Mathf.Pow(1 - tParam, 3) * startPosition +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * dirStartMove +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * dirEndMove +
                Mathf.Pow(tParam, 3) * endPosition;
        }
    }
    private void OnDisable()
    {
        //enemyTransform = null;
    }
    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(startPosition, radiusCastEnemy);
    }
    //private void OnDrawGizmos()
    //{
    //    Vector3 p0 = startTransform.position;
    //    Vector3 p1 = p0 + directionStart; 
    //    Vector3 p2 = p0 + directionEnd;
    //    Vector3 p3 = enemyTransform.position;
    //    for (float t = 0f; t < 1f; t += 0.05f)
    //    {
    //        Gizmos.color = Color.yellow;
    //        Vector3 gm = Mathf.Pow(1 - t, 3) * p0 +
    //            3 * Mathf.Pow(1 - t, 2) * t * p1 +
    //            3 * (1 - t) * Mathf.Pow(t, 2) * p2 +
    //            Mathf.Pow(t, 3) * p3;
    //        Vector3 gm2 = Mathf.Pow(1 - (t + 0.05f), 3) * p0 +
    //            3 * Mathf.Pow(1 - (t + 0.05f), 2) * (t + 0.05f) * p1 +
    //            3 * (1 - (t + 0.05f)) * Mathf.Pow((t + 0.05f), 2) * p2 +
    //            Mathf.Pow((t + 0.05f), 3) * p3;
    //        Gizmos.DrawLine(gm, gm2);
    //    }
    //        //Gizmos.color = Color.green;
    //        //Gizmos.DrawLine(startPosition, directionStart);
    //        //Gizmos.DrawLine(directionEnd, enemyTransform.position);

    //}
}

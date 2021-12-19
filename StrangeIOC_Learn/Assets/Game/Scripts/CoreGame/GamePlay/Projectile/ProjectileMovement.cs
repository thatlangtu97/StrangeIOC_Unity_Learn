using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public TrailRenderer trail;
    public float timeDelay;
    public Transform enemyTransform;
    public Transform startTransform;
    float deltaTime = 0;
    float tParam;
    float countTimeDelay;
    public Vector3 directionStart = Vector3.zero;
    public Vector3 directionEnd = Vector3.zero;
    Vector3 endPosition = Vector3.zero;
    Vector3 startPosition = Vector3.zero;

    Vector3 dirStartMove, dirEndMove;
    Vector3 projectilePosition = Vector3.zero;

    public void OnEnable()
    {
        //StartCoroutine(GoByTheRoute());
        countTimeDelay = timeDelay;
        tParam = 0;
        trail.enabled = false;
    }
    public void UpdatePoint()
    {
        startPosition = startTransform.position;
        endPosition = enemyTransform.position;
        dirStartMove = startPosition + directionStart ;
        dirEndMove = startPosition + directionEnd ;


        transform.position = projectilePosition;
        if (countTimeDelay > 0)
        {
            countTimeDelay-= Time.fixedDeltaTime;
            deltaTime = 0;
            trail.enabled = false;
        }
        else
        {
            trail.enabled = true;
            deltaTime = Time.fixedDeltaTime;
        } 
    }
    public void update()
    {
        
        if (tParam <= 1f)
        {
            //tParam += deltaTime;
            if (tParam >= .5f)
            {
                tParam += deltaTime;
            }
            else
            {
                tParam += deltaTime*2f;
            }
            projectilePosition = Mathf.Pow(1 - tParam, 3) * startPosition +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * dirStartMove +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * dirEndMove +
                Mathf.Pow(tParam, 3) * endPosition;
        }

    }
    private void OnDrawGizmos()
    {
        Vector3 p0 = startTransform.position;
        Vector3 p1 = p0 + directionStart; 
        Vector3 p2 = p0 + directionEnd;
        Vector3 p3 = enemyTransform.position;
        for (float t = 0f; t < 1f; t += 0.05f)
        {
            Gizmos.color = Color.yellow;
            Vector3 gm = Mathf.Pow(1 - t, 3) * p0 +
                3 * Mathf.Pow(1 - t, 2) * t * p1 +
                3 * (1 - t) * Mathf.Pow(t, 2) * p2 +
                Mathf.Pow(t, 3) * p3;
            Vector3 gm2 = Mathf.Pow(1 - (t + 0.05f), 3) * p0 +
                3 * Mathf.Pow(1 - (t + 0.05f), 2) * (t + 0.05f) * p1 +
                3 * (1 - (t + 0.05f)) * Mathf.Pow((t + 0.05f), 2) * p2 +
                Mathf.Pow((t + 0.05f), 3) * p3;
            Gizmos.DrawLine(gm, gm2);
            // Gizmos.DrawSphere(gm, 0.1f);
        }
            //Gizmos.color = Color.green;
            //Gizmos.DrawLine(startPosition, directionStart);
            //Gizmos.DrawLine(directionEnd, enemyTransform.position);

    }
}

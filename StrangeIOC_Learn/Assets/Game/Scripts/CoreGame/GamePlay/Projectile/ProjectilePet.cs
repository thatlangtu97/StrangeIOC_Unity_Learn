using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePet : MonoBehaviour
{
    public float timeDelay;
    public Transform enemyTransform;
    public Transform startTransform;
    public Transform directionTransform, directionTransform2;
    public Vector3 directionPosition;
    Vector3 startPosition;

    public float timeStep;
    public void OnEnable()
    {
        StartCoroutine(GoByTheRoute());
    }
    [Range(0f, 1f)]
    public float tParam; 
    public float speedModifier;
    private void OnDrawGizmos()
    {
        Vector3 p0 = startTransform.position;
        Vector3 p1 = directionTransform.position;
        Vector3 p2 = directionTransform2.position;
        Vector3 p3 = enemyTransform.position;
        for(float t=0f;t< 1f; t+=0.05f)
        {
            Vector3 gm = Mathf.Pow(1 - t, 3) * p0 +
                3 * Mathf.Pow(1 - t, 2) * t * p1 +
                3 * (1 - t) * Mathf.Pow(t, 2) * p2 +
                Mathf.Pow(t, 3) * p3;
            Gizmos.DrawSphere(gm, 0.1f);
        }
        Gizmos.DrawLine(startTransform.position, directionTransform.position);
        Gizmos.DrawLine(directionTransform2.position, enemyTransform.position);
    }
    IEnumerator GoByTheRoute()
    {
        yield return new WaitForSeconds(timeDelay);
        Vector3 p0 = startTransform.position;
        Vector3 p1 = directionTransform.position;
        Vector3 p2 = directionTransform2.position;
        Vector3 p3 = enemyTransform.position;
        tParam = 0;
        transform.position = startTransform.position;
        //float speed = speedModifier;
        while (true)
        {       

            if (tParam < 1.1f)
            {
                //tParam1 = Mathf.Clamp(tParam1 += speed, 0f, 1.1f);
                //if (tParam1 >= 1f)
                //{
                //    tParam1 = 1f;
                //    speed = 1f;
                tParam += Time.fixedDeltaTime;
                p3 = enemyTransform.position;
                Vector3 projectilePosition = Mathf.Pow(1 - tParam, 3) * p0 +
                    3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                    3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                    Mathf.Pow(tParam, 3) * p3;
                transform.position = projectilePosition;
                yield return new WaitForFixedUpdate();
            }
            else
            {
                break;
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePet : MonoBehaviour
{
    public float timeDelay;
    public Transform enemyTransform;
    public Transform startTransform;
    public Transform directionTransform, directionTransform2;
    public float timeStep;
    public bool useGimoz;
    public ProjectileMovement movement;
    public void OnEnable()
    {
        //StartCoroutine(GoByTheRoute());
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
        for (float t = 0f; t < 1f; t += 0.05f)
        {
            Gizmos.color = Color.yellow;
            Vector3 gm = Mathf.Pow(1 - t, 3) * p0 +
                3 * Mathf.Pow(1 - t, 2) * t * p1 +
                3 * (1 - t) * Mathf.Pow(t, 2) * p2 +
                Mathf.Pow(t, 3) * p3;
            Vector3 gm2 = Mathf.Pow(1 - (t+0.05f), 3) * p0 +
                3 * Mathf.Pow(1 - (t + 0.05f), 2) * (t + 0.05f) * p1 +
                3 * (1 - (t + 0.05f)) * Mathf.Pow((t + 0.05f), 2) * p2 +
                Mathf.Pow((t + 0.05f), 3) * p3;
            Gizmos.DrawLine(gm, gm2);
           // Gizmos.DrawSphere(gm, 0.1f);
        }
        //Gizmos.DrawLine(startTransform.position, directionTransform.position);
        //Gizmos.DrawLine(directionTransform2.position, enemyTransform.position);
        if (useGimoz)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(startTransform.position, directionTransform.position);
            Gizmos.DrawLine(directionTransform2.position, enemyTransform.position);
            startPosition = startTransform != null ? startTransform.position : Vector3.zero;
            directionStart = directionTransform != null ? directionTransform.position : Vector3.zero;
            directionEnd = directionTransform2 != null ? directionTransform2.position : Vector3.zero;
            endPosition = enemyTransform != null ? enemyTransform.position : Vector3.zero;
            //if (movement != null)
            //{
            //    movement.directionStart = directionStart;
            //    movement.directionEnd = directionEnd;
            //}
        }
    }
    //IEnumerator GoByTheRoute()
    //{
    //    yield return new WaitForSeconds(timeDelay);
    //    Vector3 p0 = startTransform.position;
    //    Vector3 p1 = directionTransform.position;
    //    Vector3 p2 = directionTransform2.position;
    //    Vector3 p3 = enemyTransform.position;
    //    tParam = 0;
    //    transform.position = startTransform.position;
    //    //float speed = speedModifier;
    //    while (true)
    //    {       

    //        if (tParam < 1.1f)
    //        {
    //            //tParam1 = Mathf.Clamp(tParam1 += speed, 0f, 1.1f);
    //            //if (tParam1 >= 1f)
    //            //{
    //            //    tParam1 = 1f;
    //            //    speed = 1f;
    //            tParam += Time.fixedDeltaTime;
    //            p3 = enemyTransform.position;
    //            Vector3 projectilePosition = Mathf.Pow(1 - tParam, 3) * p0 +
    //                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
    //                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
    //                Mathf.Pow(tParam, 3) * p3;
    //            transform.position = projectilePosition;
    //            yield return new WaitForFixedUpdate();
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }
    //}
    public Vector3 startPosition = Vector3.zero;
    public Vector3 directionStart = Vector3.zero;
    public Vector3 directionEnd = Vector3.zero;
    public Vector3 endPosition = Vector3.zero;
    Vector3 projectilePosition = Vector3.zero;
    float deltaTime = 0;
    public void UpdatePoint()
    {
        startPosition = startTransform.position;
        directionStart = directionTransform.position;
        directionEnd = directionTransform2.position;
        endPosition = enemyTransform.position;

        transform.position = projectilePosition;
        deltaTime = Time.fixedDeltaTime;
    }
    public void update()
    {
        if (tParam < 1f)
        {
            tParam += deltaTime;
            projectilePosition = Mathf.Pow(1 - tParam, 3) * startPosition +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * directionStart +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * directionEnd +
                Mathf.Pow(tParam, 3) * endPosition;
        }
    }
    public void OnDisable()
    {
        //Test
        tParam = 0;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DpsCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public float flyTime;
    public float damageTime;
    public float damageStep;
    public Collider2D collider;
    PooledObjectComponentManager componentManager;
    float prevDamageTime;
    bool isMove = true;
    bool isFly;
    float maxFlyTime;
    float maxDamageTime;

    void Start()
    {
        maxFlyTime = flyTime;
        maxDamageTime = damageTime;
    }

    private void OnEnable()
    {
        StartCoroutine(DelayGetEntity());
        collider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFly)
        {
            if (flyTime > 0)
            {
                flyTime -= Time.deltaTime;
                isMove = componentManager.entity.moveAndDestroy.isenable;
                if (flyTime <= 0)
                {
                    componentManager.entity.moveAndDestroy.isenable = false;
                    collider.enabled = false;
                    prevDamageTime = Time.time;
                    isMove = false;
                    StartCoroutine(delayReset(damageTime));
                }
            }
            if (Time.time - prevDamageTime > damageStep)
            {
                collider.enabled = true;
                StartCoroutine(delayDisable());
                prevDamageTime = Time.time;
            }
            if (!isMove)
            {
                if (damageTime > 0)
                {
                    damageTime -= Time.deltaTime;

                }
            }
        }
    }

    IEnumerator DelayGetEntity()
    {
        yield return new WaitForSeconds(0.1f);
        isFly = true;
        componentManager = GetComponent<PooledObjectComponentManager>();
        componentManager.entity.moveAndDestroy.timer = flyTime + damageTime;
        isMove = true;
    }

    IEnumerator delayDisable()
    {
        yield return new WaitForSeconds(0.05f);
        collider.enabled = false;
    }

    IEnumerator delayReset(float time)
    {
        yield return new WaitForSeconds(time);
        flyTime = maxFlyTime;
        damageTime = maxDamageTime;
        ObjectPool.Recycle(gameObject);
    }
}
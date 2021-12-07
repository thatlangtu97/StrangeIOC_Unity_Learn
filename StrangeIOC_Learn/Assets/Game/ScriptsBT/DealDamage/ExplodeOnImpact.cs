using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
    public GameObject explode;
    bool exploded;
    public int explodeRate;
    int rand;
    public void OnDisable()
    {
        exploded = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {    
        if(!exploded)
        {
            rand = Random.Range(0, 100);
            if (rand <= explodeRate)
            {
                ObjectPool.Spawn(explode, transform.position);
                exploded = true;
            }      
        }
      
    }
}

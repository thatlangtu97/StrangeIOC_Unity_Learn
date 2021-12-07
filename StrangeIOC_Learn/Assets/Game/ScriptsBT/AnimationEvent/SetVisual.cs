using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVisual : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInactiveVisual()
    {
        StartCoroutine(DelayInactive());
    }

    IEnumerator DelayInactive()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}

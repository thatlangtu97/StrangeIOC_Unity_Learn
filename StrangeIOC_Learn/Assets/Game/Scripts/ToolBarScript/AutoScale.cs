using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScale : MonoBehaviour
{
    public List<GameObject> listObject;
    public float scale;
    public bool up,down,upmove,downmove;
    private void OnDrawGizmos()
    {
        if (up)
        {
            foreach(GameObject temp in listObject)
            {
                temp.transform.localScale *= scale;
                up = false;
            }
        }
        if (down)
        {
            foreach (GameObject temp in listObject)
            {
                temp.transform.localScale /= scale;
                down = false;
            }
        }
        if (upmove)
        {
            foreach (GameObject temp in listObject)
            {
                temp.transform.localPosition = new Vector3(temp.transform.localPosition.x*scale, temp.transform.localPosition.y, temp.transform.localPosition.z);
                upmove = false;
            }
        }
        if (downmove)
        {
            foreach (GameObject temp in listObject)
            {
                temp.transform.localPosition = new Vector3(temp.transform.localPosition.x / scale, temp.transform.localPosition.y, temp.transform.localPosition.z);
                downmove = false;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChangeLayerMaskExtension
{
    // Start is called before the first frame update
    public static void ChangeLayerMask(this GameObject go, LayerMask layerMask)
    {
        int layerNumber = 0;
        int layer = layerMask.value;
        while (layer > 0)
        {
            layer = layer >> 1;
            layerNumber++;

        }
        go.layer = layerNumber - 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLayerWeight : MonoBehaviour
{
    public Animator animator;
    // call in animation event
    public void ActiveLayer(int layerIndex)
    {
        animator.SetLayerWeight(layerIndex, 1f);
    }
    public void DeActiveLayer(int layerIndex)
    {
        animator.SetLayerWeight(layerIndex, 0f);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFIllPanelInParent : MonoBehaviour
{
    [SerializeField]
    RectTransform rectTransform;
    public Vector2 anchorMin = Vector2.zero, anchorMax = Vector2.one;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        rectTransform.offsetMin = anchorMin;
        rectTransform.offsetMax = anchorMax;
        rectTransform.localScale = new Vector3(1, 1, 1);
    }
}

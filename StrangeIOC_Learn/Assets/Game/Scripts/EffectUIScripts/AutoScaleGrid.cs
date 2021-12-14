using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScaleGrid : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform rectTransform;
    public Vector2 sizeGrid;
    public Vector2 baseSizeGrid = new Vector2(960f,674f);
    //public Vector2 baseCellSize = Vector2.;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sizeGrid = new Vector2(rectTransform.rect.width , rectTransform.rect.height);
        rectTransform.localScale = new Vector2(Mathf.Clamp01(sizeGrid.x / baseSizeGrid.x), Mathf.Clamp01(sizeGrid.x / baseSizeGrid.x));
    }
}

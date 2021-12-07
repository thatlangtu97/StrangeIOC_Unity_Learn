using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoPlayOpenGacha : MonoBehaviour
{
    [Range(-1, 1)]
    public float RotationUV_Rotation_PosY_1;
    [Range(-1, 1)]
    public float _ShinyOnlyFX_Size_1;
    [Range(-1, 2)]
    public float _ThresholdSmooth_Value_1;
    [Range(0, 1)]
    public float SpriteFade;
    public Color _FillColor_Color_1;
    public Material material;
    void Start()
    {
        GetComponent<Image>().material = material;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        material.SetFloat("RotationUV_Rotation_PosY_1", RotationUV_Rotation_PosY_1);
        material.SetFloat("_ShinyOnlyFX_Size_1", _ShinyOnlyFX_Size_1);
        material.SetFloat("_ThresholdSmooth_Value_1", _ThresholdSmooth_Value_1);
        material.SetFloat("_SpriteFade", SpriteFade);
        material.SetColor("_FillColor_Color_1", _FillColor_Color_1);

    }
}

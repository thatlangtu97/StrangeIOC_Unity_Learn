//////////////////////////////////////////////////////////////
/// Shadero Sprite: Sprite Shader Editor - by VETASOFT 2018 //
/// Shader generate with Shadero 1.9.3                      //
/// http://u3d.as/V7t #AssetStore                           //
/// http://www.shadero.com #Docs                            //
//////////////////////////////////////////////////////////////

Shader "Shadero Customs/OpenChestShader"
{
Properties
{
[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
RotationUV_Rotation_1("RotationUV_Rotation_1", Range(-360, 360)) = 99
RotationUV_Rotation_PosX_1("RotationUV_Rotation_PosX_1", Range(-1, 2)) = 0.529
RotationUV_Rotation_PosY_1("RotationUV_Rotation_PosY_1", Range(-1, 2)) =0.5
RotationUV_Rotation_Speed_1("RotationUV_Rotation_Speed_1", Range(-8, 8)) =0
_ShinyOnlyFX_Pos_1("_ShinyOnlyFX_Pos_1", Range(-1, 1)) = 0
_ShinyOnlyFX_Size_1("_ShinyOnlyFX_Size_1", Range(-1, 1)) = -0.164
_ShinyOnlyFX_Smooth_1("_ShinyOnlyFX_Smooth_1", Range(0, 1)) = 0.321
_ShinyOnlyFX_Intensity_1("_ShinyOnlyFX_Intensity_1", Range(0, 4)) = 1.99
_ShinyOnlyFX_Speed_1("_ShinyOnlyFX_Speed_1", Range(0, 8)) = 0
_FillColor_Color_1("_FillColor_Color_1", COLOR) = (0.1839623,0.551285,1,1)
_Add_Fade_1("_Add_Fade_1", Range(0, 4)) = 1
_NewTex_1("NewTex_1(RGB)", 2D) = "white" { }
_FillColor_Color_2("_FillColor_Color_2", COLOR) = (1,1,1,1)
_TurnAlphaToBlack_Fade_2("_TurnAlphaToBlack_Fade_2", Range(0, 1)) = 0
_MaskRGBA_Fade_1("_MaskRGBA_Fade_1", Range(0, 1)) = 0
_ThresholdSmooth_Value_1("_ThresholdSmooth_Value_1", Range(-1, 2)) = -1
_ThresholdSmooth_Smooth_1("_ThresholdSmooth_Smooth_1", Range(0, 1)) = 0.176
_OperationBlendMask_Fade_1("_OperationBlendMask_Fade_1", Range(0, 1)) = 0
_SpriteFade("SpriteFade", Range(0, 1)) = 1.0

// required for UI.Mask
[HideInInspector]_StencilComp("Stencil Comparison", Float) = 8
[HideInInspector]_Stencil("Stencil ID", Float) = 0
[HideInInspector]_StencilOp("Stencil Operation", Float) = 0
[HideInInspector]_StencilWriteMask("Stencil Write Mask", Float) = 255
[HideInInspector]_StencilReadMask("Stencil Read Mask", Float) = 255
[HideInInspector]_ColorMask("Color Mask", Float) = 15

}

SubShader
{

Tags {"Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off

// required for UI.Mask
Stencil
{
Ref [_Stencil]
Comp [_StencilComp]
Pass [_StencilOp]
ReadMask [_StencilReadMask]
WriteMask [_StencilWriteMask]
}

Pass
{

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

struct appdata_t{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};

struct v2f
{
float2 texcoord  : TEXCOORD0;
float4 vertex   : SV_POSITION;
float4 color    : COLOR;
};

sampler2D _MainTex;
float _SpriteFade;
float RotationUV_Rotation_1;
float RotationUV_Rotation_PosX_1;
float RotationUV_Rotation_PosY_1;
float RotationUV_Rotation_Speed_1;
float _ShinyOnlyFX_Pos_1;
float _ShinyOnlyFX_Size_1;
float _ShinyOnlyFX_Smooth_1;
float _ShinyOnlyFX_Intensity_1;
float _ShinyOnlyFX_Speed_1;
float4 _FillColor_Color_1;
float _Add_Fade_1;
sampler2D _NewTex_1;
float4 _FillColor_Color_2;
float _TurnAlphaToBlack_Fade_2;
float _MaskRGBA_Fade_1;
float _ThresholdSmooth_Value_1;
float _ThresholdSmooth_Smooth_1;
float _OperationBlendMask_Fade_1;

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;
return OUT;
}


float2 RotationUV(float2 uv, float rot, float posx, float posy, float speed)
{
rot=rot+(_Time*speed*360);
uv = uv - float2(posx, posy);
float angle = rot * 0.01744444;
float sinX = sin(angle);
float cosX = cos(angle);
float2x2 rotationMatrix = float2x2(cosX, -sinX, sinX, cosX);
uv = mul(uv, rotationMatrix) + float2(posx, posy);
return uv;
}
float4 UniColor(float4 txt, float4 color)
{
txt.rgb = lerp(txt.rgb,color.rgb,color.a);
return txt;
}
float4 ThresholdSmooth(float4 txt, float value, float smooth)
{
float l = (txt.x + txt.y + txt.z) * 0.33;
txt.rgb = smoothstep(value, value + smooth, l);
return txt;
}
float4 OperationBlendMask(float4 origin, float4 overlay, float4 mask, float blend)
{
float4 o = origin; 
origin.rgb = overlay.a * overlay.rgb + origin.a * (1 - overlay.a) * origin.rgb;
origin.a = overlay.a + origin.a * (1 - overlay.a);
origin.a *= mask;
origin = lerp(o, origin,blend);
return origin;
}

float4 TurnAlphaToBlack(float4 txt,float fade)
{
float3 gs = lerp(txt.rgb,float3(0,0,0), 1-txt.a);
return lerp(txt,float4(gs, 1), fade);
}

float4 ShinyOnlyFX(float2 uv, float pos, float size, float smooth, float intensity, float speed)
{
pos = pos + 0.5+sin(_Time*20*speed)*0.5;
uv = uv - float2(pos, 0.5);
float a = atan2(uv.x, uv.y) + 1.4, r = 3.1415;
float d = cos(floor(0.5 + a / r) * r - a) * length(uv);
float dist = 1.0 - smoothstep(size, size + smooth, d);
return dist*intensity;
}
float4 frag (v2f i) : COLOR
{
float4 _MainTex_1 = tex2D(_MainTex, i.texcoord);
float2 RotationUV_1 = RotationUV(i.texcoord,RotationUV_Rotation_1,RotationUV_Rotation_PosX_1,RotationUV_Rotation_PosY_1,RotationUV_Rotation_Speed_1);
float4 _ShinyOnlyFX_1 = ShinyOnlyFX(RotationUV_1,_ShinyOnlyFX_Pos_1,_ShinyOnlyFX_Size_1,_ShinyOnlyFX_Smooth_1,_ShinyOnlyFX_Intensity_1,_ShinyOnlyFX_Speed_1);
float4 FillColor_1 = UniColor(_ShinyOnlyFX_1,_FillColor_Color_1);
_MainTex_1 = lerp(_MainTex_1,_MainTex_1*_MainTex_1.a + FillColor_1*FillColor_1.a,_Add_Fade_1 * _MainTex_1.a);
float4 NewTex_1 = tex2D(_NewTex_1, i.texcoord);
float4 FillColor_2 = UniColor(_MainTex_1,_FillColor_Color_2);
float4 TurnAlphaToBlack_2 = TurnAlphaToBlack(FillColor_2,_TurnAlphaToBlack_Fade_2);
NewTex_1.a = lerp(TurnAlphaToBlack_2.r, 1 - TurnAlphaToBlack_2.r ,_MaskRGBA_Fade_1);
float4 _ThresholdSmooth_1 = ThresholdSmooth(NewTex_1,_ThresholdSmooth_Value_1,_ThresholdSmooth_Smooth_1);
_MainTex_1.a = lerp(_ThresholdSmooth_1.r * _MainTex_1.a, (1 - _ThresholdSmooth_1.r) * _MainTex_1.a,0);
NewTex_1.a = lerp(TurnAlphaToBlack_2.r, 1 - TurnAlphaToBlack_2.r ,_MaskRGBA_Fade_1);
_MainTex_1.a = lerp(_ThresholdSmooth_1.r * _MainTex_1.a, (1 - _ThresholdSmooth_1.r) * _MainTex_1.a,0);
float4 OperationBlendMask_1 = OperationBlendMask(_MainTex_1, _MainTex_1, _MainTex_1, _OperationBlendMask_Fade_1); 
float4 FinalResult = OperationBlendMask_1;
FinalResult.rgb *= i.color.rgb;
FinalResult.a = FinalResult.a * _SpriteFade * i.color.a;
FinalResult.rgb *= FinalResult.a;
FinalResult.a = saturate(FinalResult.a);
return FinalResult;
}

ENDCG
}
}
Fallback "Sprites/Default"
}

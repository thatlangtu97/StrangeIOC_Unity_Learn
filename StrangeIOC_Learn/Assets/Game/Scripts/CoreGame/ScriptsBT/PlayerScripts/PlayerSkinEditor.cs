using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[CustomEditor(typeof(PlayerSkin))]
public class PlayerSkinEditor : Editor
{
    public GUIStyle boldFoldoutStyle;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (boldFoldoutStyle == null)
        {
            boldFoldoutStyle = new GUIStyle(EditorStyles.foldout);
            boldFoldoutStyle.fontStyle = FontStyle.Bold;
        }
        DrawBrickPathInspector();

    }
    void DrawBrickPathInspector()
    {
        if (GUILayout.Button("Modifier"))
        {
            
            PlayerSkin thisObj = (PlayerSkin)target;
            Skin mixSkin = new Skin("NewSkin");
            thisObj.runInEditMode = true;
            mixSkin.AddSkin(thisObj.skeletonMecanim.skeleton.Data.FindSkin(thisObj.skinWeaponName));
            mixSkin.AddSkin(thisObj.skeletonMecanim.skeleton.Data.FindSkin(thisObj.skinOufitName));
            thisObj.skeletonMecanim.skeleton.SetSkin(mixSkin);
            thisObj.skeletonMecanim.skeleton.SetSlotsToSetupPose();
            Debug.Log("setskin");
        }


    }
}
#endif
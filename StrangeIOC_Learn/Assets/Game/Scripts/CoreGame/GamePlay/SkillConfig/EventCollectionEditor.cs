//using Sirenix.OdinInspector;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//public class EventCollectionEditor : EditorWindow, IHasCustomMenu
//{
//#if UNITY_EDITOR
//    private string pathToFile;
//	[UnityEditor.Callbacks.OnOpenAsset()]
//	public static bool OnOpenAsset(int instanceID, int line)
//	{
//        string nameFile = Selection.activeObject.GetType().ToString();
//        Debug.Log(Selection.activeObject as SerializedScriptableObject);
//        EditorWindow editor = EditorWindow.GetWindow(typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow"));

//        editor = Instantiate(editor);
//        editor.Show();
//        editor.position = new Rect(Random.RandomRange(100, 500), 100, 640, 480);
//        var editorTitleContent = new GUIContent(editor.titleContent);
//        editorTitleContent.text = nameFile;
//        editor.titleContent = editorTitleContent;
//  //      SerializedScriptableObject temp = Selection.activeObject as SerializedScriptableObject;
//		//Debug.Log(temp);
//		//if (temp != null)
//		//{
//		//	var neweditor = Editor.CreateEditor(Selection.activeObject as SerializedScriptableObject);
//  //          neweditor.DrawDefaultInspector();
//		//	return true;
//		//}
//		return false;
//	}

//    public void AddItemsToMenu(GenericMenu menu)
//    {
       
//    }
//#endif
//}

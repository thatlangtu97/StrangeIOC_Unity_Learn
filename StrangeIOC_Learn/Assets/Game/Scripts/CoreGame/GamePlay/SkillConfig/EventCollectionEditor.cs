using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EventCollectionEditor : EditorWindow, IHasCustomMenu
{
	private string pathToFile;
	[UnityEditor.Callbacks.OnOpenAsset()]
	public static bool OnOpenAsset(int instanceID, int line)
	{
		string nameFile = Selection.activeObject.GetType().Name;
		EditorWindow editor = EditorWindow.GetWindow(typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow"));
			editor = Instantiate(editor);
			editor.Show();
			editor.position = new Rect(Random.RandomRange(100, 500), 100, 640, 480);
			var editorTitleContent = new GUIContent(editor.titleContent);
			editorTitleContent.text = nameFile;
			editor.titleContent = editorTitleContent;
		return false;
	}

    public void AddItemsToMenu(GenericMenu menu)
    {
       
    }
}

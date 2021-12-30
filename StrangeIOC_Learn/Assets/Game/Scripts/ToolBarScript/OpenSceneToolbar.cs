#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class OpenSceneToolbar : MonoBehaviour
{
    [MenuItem("Open Scene/UI Dev &1")]
    public static void OpenSplashScreen()
    {
        OpenScene("UIDev");
    }
    [MenuItem("Open Scene/FlashScene &2")]
    public static void OpenFlashScene()
    {
        OpenScene("FlashScene");
    }
    [MenuItem("Open Scene/HomeScene &3")]
    public static void OpenHomeSceneOld()
    {
        OpenScene("HomeScene");
    }
    [MenuItem("Open Scene/Main &4")]
    public static void Main()
    {
        OpenScene("Main");
    }
    [MenuItem("Open Scene/TestBt &5")]
    public static void OpenTestBt()
    {
        OpenScene("TestBt");
    }
    private static void OpenScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Game/Scenes/" + sceneName + ".unity");
        }
    }
}

#endif
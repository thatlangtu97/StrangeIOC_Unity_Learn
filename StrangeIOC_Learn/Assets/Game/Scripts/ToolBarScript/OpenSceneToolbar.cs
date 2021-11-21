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
    [MenuItem("Open Scene/_HomeScene &3")]
    public static void OpenHomeSceneOld()
    {
        OpenScene("_HomeScene");
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
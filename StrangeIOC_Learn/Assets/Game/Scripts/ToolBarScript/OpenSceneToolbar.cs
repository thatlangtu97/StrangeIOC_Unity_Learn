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
    public static void OpenFirstScene()
    {
        OpenScene("FlashScene");
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
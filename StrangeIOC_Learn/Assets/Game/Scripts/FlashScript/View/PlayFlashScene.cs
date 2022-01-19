using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayFlashScene : View
{
    public static PlayFlashScene instance;
    [Inject] public PopupManager popupManager { get; set; }
    public string nameScene;
    public Button StartGameBtn;
    public Animator animator;

    protected override void Start()
    {
        base.Start();
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public void StartGameClick()
    {
        
        StartCoroutine(DelayLoadScene());
    }
    IEnumerator DelayLoadScene()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadSceneAsync(nameScene);
    }
    public void Loading(string namescene, float timeDelay,System.Action action=null)
    {
        StartCoroutine(DelayLoadSceneWithLoading(namescene, timeDelay, action));
    }
    IEnumerator DelayLoadSceneWithLoading(string namescene , float timeDelay, System.Action action = null)
    {
        ShowLoading();
        try
        {
            if (action != null) action.Invoke();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
        yield return new WaitForSeconds(timeDelay);        
        SceneManager.LoadSceneAsync(namescene);
    }
    public void HideLoading()
    {
        animator.SetTrigger("HideLogo");
    }
    public void ShowLoading()
    {
        animator.SetTrigger("ShowLoading");
    }
}

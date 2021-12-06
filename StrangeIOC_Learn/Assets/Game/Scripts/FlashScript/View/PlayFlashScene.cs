using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayFlashScene : View
{
    public string nameScene;
    public float timeDelay;
    public Button StartGameBtn;
    public Animator animator;
    protected override void Start()
    {
        base.Start();
    }
    public void StartGameClick()
    {
        
        StartCoroutine(DelayLoadScene());
    }
    IEnumerator DelayLoadScene()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("HideLogo");
        SceneManager.LoadScene(nameScene);
    }
}

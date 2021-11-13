using EntrySystem;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayFlashScene : View
{
    public string nameScene;
    public float timeDelay;
    [Inject] public PopupManager popupManager { get; set; }
    [Inject] public GlobalData globalData { get; set; }
    void Start()
    {
        //yield return new WaitForSeconds(timeDelay);
        //SceneManager.LoadScene(nameScene);
        TestUtils temp = new TestUtils();
        temp.DebugPopup();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

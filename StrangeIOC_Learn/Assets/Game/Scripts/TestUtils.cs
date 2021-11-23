using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestUtils :View
{
    [Inject] public ShowPanelHomeSignal showPanelHomeSignal { get; set; }
    [Inject] public PopupManager popupManager { get; set; }
    public PanelKey panelKey;
    public PopupKey popupKey;
    protected override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //showPanelHomeSignal.Dispatch();
            popupManager.BackPanel();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            //showPanelHomeSignal.Dispatch();
            SceneManager.LoadScene("testSpawnObject");

        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            //showPanelHomeSignal.Dispatch();
            SceneManager.LoadScene("HomeScene");

        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            //showPanelHomeSignal.Dispatch();
            SceneManager.LoadScene("FlashScene");

        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter)){
            popupManager.SetPanelAfterLoadHomeScene(panelKey, popupKey);
        }
    }
}

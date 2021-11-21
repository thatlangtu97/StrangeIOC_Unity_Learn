using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUtils :View
{
    [Inject] public ShowPanelHomeSignal showPanelHomeSignal { get; set; }
    [Inject] public PopupManager popupManager { get; set; }
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
    }
}

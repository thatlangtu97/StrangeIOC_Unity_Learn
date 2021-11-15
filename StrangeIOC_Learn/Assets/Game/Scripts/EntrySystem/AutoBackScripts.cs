using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBackScripts : View
{
    [Inject] public PopupManager popupManager { get; set; }
    protected override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Back panel;
            popupManager.BackPanel();
        }
    }
}

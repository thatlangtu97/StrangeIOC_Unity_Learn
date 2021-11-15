using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBasePanel : View
{
    [Inject] public PopupManager popupManager { get; set; } 
    public UILayer uiLayer;
    //protected override void Awake()
    //{
    //    base.Awake();
    //    Debug.LogError(popupManager);
    //    popupManager.AddUILayer(uiLayer, transform);
    //}
    protected override void Start()
    {
        base.Start();
        Debug.Log(popupManager);
        popupManager.AddUILayer(uiLayer, transform);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBasePanel : MonoBehaviour
{
    [Inject()] public PopupManager popupManager { get; set; } 
    public UILayer uiLayer;
    private void Awake()
    {
        
    }
    void Start()
    {
        Debug.Log(popupManager);
        //popupManager.AddUILayer(uiLayer, transform);
    }

}

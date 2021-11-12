using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager 
{
    public PanelKey panelKey;
    public PopupManager()
    {
        Debug.Log("tao moi popup manager");
    }
    public void Start()
    {
        Debug.Log("start popup singleton");
    }
    public void SetPopupAfterLoadHomeScene(PanelKey key)
    {
        panelKey = key;
    }
    public void ResetPanelAfterLoadHomeScene()
    {
        panelKey = PanelKey.PanelHome;
    }
    public PanelKey GetPanelAfterLoadHomeScene()
    {
        return panelKey;
    }
    public void ResetPanelShowAfterLoadHomeScene()
    {
        panelKey = PanelKey.PanelHome;
    }
}
public enum PanelKey
{
    PanelHome,
}
public enum UILayer
{
    UI1,
    UI2,
}

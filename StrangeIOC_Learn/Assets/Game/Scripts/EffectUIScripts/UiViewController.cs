using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;

public class UiViewController : MonoBehaviour
{
    public UIView uiView;
    public bool isStarted;
    public Action action;
    void Start()
    {
        if (action != null)
        {
            action.Invoke();
        }
    }
    void Setup(bool checkShow)
    {
        if (checkShow)
            action = Show;
        else
            action = Hide;
    }
    public void Show()
    {
        if (action == null)
        {
            Setup(true);
        }
        else
        {
            uiView.Show();
        }
    }
    public void Hide()
    {
        if (action == null)
        {
            Setup(false);
        }
        else
        {
            uiView.Hide();
        }
    }
   
}

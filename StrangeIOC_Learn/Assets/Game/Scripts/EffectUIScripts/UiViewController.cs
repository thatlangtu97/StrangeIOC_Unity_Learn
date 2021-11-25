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
        //if (action != null && isStarted == false && gameObject.activeInHierarchy)
        //{
        //    uiView.DisableGameObjectWhenHidden = true;
        //    isStarted = true;
        //    action.Invoke();
        //}
        uiView.DisableGameObjectWhenHidden = true;
        isStarted = true;
        DelayInvokeAction();
        //action.Invoke();
    }
    void Setup(bool checkShow)
    {
        //uiView.Awake();
        //uiView.Start();
        if (checkShow)
            action = Show;
        else
            action = Hide;
        gameObject.SetActive(true);
    }
    public void Show()
    {
        if (!isStarted)
        {
            Setup(true);
        }
        else
        {
            uiView.Show();
            //UIView.ShowView("General", panelKey.ToString());
        }
        
    }
    public void Hide()
    {
        if (!isStarted)
        {
            Setup(false);
        }
        else
        {
            uiView.Hide();
            //UIView.HideView("General", panelKey.ToString());
        }

    }
    void DelayInvokeAction()
    {
        StartCoroutine(delayInvokeAction());
    }
    IEnumerator delayInvokeAction()
    {
        //yield return new WaitForSeconds(0.1f);
        yield return new WaitForEndOfFrame();
        if (action!=null)
            action.Invoke();
    }
}

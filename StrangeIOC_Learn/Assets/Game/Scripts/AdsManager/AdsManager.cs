using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager 
{
    public Action actionFinish;
    public void AddAction(Action action)
    {
        actionFinish = action;
    }
    public void GetReward()
    {
        actionFinish.Invoke();
        actionFinish = null;
    }
}


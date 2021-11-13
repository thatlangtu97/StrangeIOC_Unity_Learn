using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneContextView : ContextView
{
    void Awake()
    {
        context = new HomeSceneContext(this);
        context.Start();
    }
}

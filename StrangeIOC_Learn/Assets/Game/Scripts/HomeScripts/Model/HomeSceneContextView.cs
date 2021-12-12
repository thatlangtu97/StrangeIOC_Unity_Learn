using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneContextView : ContextView
{
    void Start()
    {
        context = new HomeSceneContext(this);
        context.Start();
        if (PlayFlashScene.instance != null)
        {
            PlayFlashScene.instance.HideLoading();
        }
    }
}

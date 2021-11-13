using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSceneContextView : ContextView
{
    // Start is called before the first frame update
    void Start()
    {
        context = new FlashSceneContext (this);
        context.Start();
        DontDestroyOnLoad(gameObject);
    }


}

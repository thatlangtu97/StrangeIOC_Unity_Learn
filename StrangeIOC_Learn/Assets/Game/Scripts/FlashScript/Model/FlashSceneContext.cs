using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSceneContext : MVCSContext
{
    private readonly FlashSceneContextView view;

    public FlashSceneContext(FlashSceneContextView view) : base(view, true)
    {
        this.view = view;
    }
    protected override void mapBindings()
    {
        base.mapBindings();
        //injectionBinder.Bind<PopupManager>().ToValue(new PopupManager()).ToSingleton().CrossContext();
        //mediationBinder.Bind<FlashSceneView>().To<FlashSceneMediator>();
    }
    // Remove Inject nếu k cần đến nữa
    public override void OnRemove()
    {
        base.OnRemove();
    }
    public override void Launch()
    {
        base.Launch();
        
    }

}

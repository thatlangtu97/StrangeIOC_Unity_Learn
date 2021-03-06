using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneContext : MVCSContext
{
    private readonly HomeSceneContextView view;

    public HomeSceneContext(HomeSceneContextView view) : base(view, true)
    {
        this.view = view;
    }
    protected override void mapBindings()
    {
        base.mapBindings();
        injectionBinder.Bind<PopupManager>().ToValue(new PopupManager()).ToSingleton();
        commandBinder.Bind<FinishSetupHomeSceneSignal>().To<FinishSetupHomeSceneCmd>();
        commandBinder.Bind<ShowPanelHomeSignal>().To<ShowPanelHomeCmd>();
    }
    // Remove Inject nếu k cần đến nữa
    public override void OnRemove()
    {
        base.OnRemove();
    }
    public override void Launch()
    {
        base.Launch();
        injectionBinder.GetInstance<FinishSetupHomeSceneSignal>().Dispatch();
    }
}

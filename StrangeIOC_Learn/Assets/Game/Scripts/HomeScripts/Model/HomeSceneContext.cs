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
        //commandBinder.Bind<>().To<>();
        //injectionBinder.Bind<PopupManager>().ToValue(new PopupManager()).ToSingleton();
        //commandBinder.Bind<InitHomeSceneSignal>().To<InitHomeSceneCmd>().To<FinishSetupHomeSceneCmd>().InSequence(); 
        commandBinder.Bind<InitHomeSceneSignal>().To<InitHomeSceneCmd>().To<FinishSetupHomeSceneCmd>().InSequence();

        //commandBinder.Bind<FinishSetupHomeSceneSignal>().To<FinishSetupHomeSceneCmd>();
        commandBinder.Bind<ShowPanelHomeSignal>().To<ShowPanelHomeCmd>();
        commandBinder.Bind<ShowPopupStaminaSignal>().To<ShowPopupStaminaCmd>();
        commandBinder.Bind<ShowPanelHeroSignal>().To<ShowPanelHeroCmd>();
        commandBinder.Bind<ShowPanelCraftSignal>().To<ShowPanelCraftCmd>();
        commandBinder.Bind<ShowPanelShopSignal>().To<ShowPanelShopCmd>();
        commandBinder.Bind<ShowPopupGachaSignal>().To<ShowPopupGachaCmd>();
        commandBinder.Bind<ShowPopupCraftSignal>().To<ShowPopupCraftCmd>();

    }
    // Remove Inject nếu k cần đến nữa
    public override void OnRemove()
    {
        base.OnRemove();
    }
    public override void Launch()
    {
        base.Launch();
        injectionBinder.GetInstance<PopupManager>().ResetPanelAfterLoadHomeScene();
        injectionBinder.GetInstance<InitHomeSceneSignal>().Dispatch();
        //injectionBinder.GetInstance<FinishSetupHomeSceneSignal>().Dispatch();
    }
}

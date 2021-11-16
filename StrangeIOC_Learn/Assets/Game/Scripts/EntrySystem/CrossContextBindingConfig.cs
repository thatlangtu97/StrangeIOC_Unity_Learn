using strange.extensions.command.api;
using strange.extensions.injector.api;
using strange.extensions.mediation.api;
namespace EntrySystem
{
    public class CrossContextBindingConfig
    {
        public CrossContextBindingConfig()
        {
        }
        public void MapBindings(ICrossContextInjectionBinder injectionBinder, ICommandBinder commandBinder,
            IMediationBinder mediationBinder)
        {
            //commandBinder.Bind<>().To<>();
            commandBinder.Bind<ShowPanelHomeSignal>().To<ShowPanelHomeCmd>();
            commandBinder.Bind<ShowPopupStaminaSignal>().To<ShowPopupStaminaCmd>();
            commandBinder.Bind<ShowPanelHeroSignal>().To<ShowPanelHeroCmd>();
        }
    }
}

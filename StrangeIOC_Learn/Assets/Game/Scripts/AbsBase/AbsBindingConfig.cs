using strange.extensions.command.api;
using strange.extensions.injector.api;
using strange.extensions.mediation.api;
namespace EntrySystem.Model
{
	public abstract class AbsBindingConfig
	{
		public abstract void MapBindings(ICrossContextInjectionBinder injectionBinder, ICommandBinder commandBinder, IMediationBinder mediationBinder);
	}
}

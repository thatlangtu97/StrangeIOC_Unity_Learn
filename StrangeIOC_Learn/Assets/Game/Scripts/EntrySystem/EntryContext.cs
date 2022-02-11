using strange.extensions.context.impl;
using UnityEngine;

namespace EntrySystem
{
	public class EntryContext : MVCSContext
	{
		public EntryContext(MonoBehaviour view, bool autoMapping) : base(view, autoMapping)
		{
		}

		protected override void mapBindings() {
			base.mapBindings();

			new CrossContextBindingConfig().MapBindings(injectionBinder, commandBinder, mediationBinder);
			injectionBinder.Bind<GlobalData>().ToValue(new GlobalData()).ToSingleton().CrossContext();
			injectionBinder.Bind<EquipmentLogic>().ToValue(new EquipmentLogic()).ToSingleton().CrossContext();
			injectionBinder.Bind<PopupManager>().ToValue(new PopupManager()).ToSingleton().CrossContext();
			injectionBinder.Bind<EntryContext>().ToValue(this).ToSingleton().CrossContext();
			injectionBinder.Bind<AdsManager>().ToValue(new AdsManager()).ToSingleton().CrossContext();



			commandBinder.Bind<SetupDatamanagerSignal>().To<SetupDataManagerCmd>();
		}
		public override void Launch()
		{
			injectionBinder.GetInstance<SetupDatamanagerSignal>().Dispatch();
		}
	}
}
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

			//new CrossContextBindingConfig().MapBindings(injectionBinder, commandBinder, mediationBinder);
			injectionBinder.Bind<GlobalData>().ToValue(new GlobalData(this)).ToSingleton().CrossContext();
			injectionBinder.Bind<PopupManager>().ToValue(new PopupManager()).ToSingleton().CrossContext();
			injectionBinder.Bind<EntryContext>().ToValue(this).ToSingleton().CrossContext();
			
			
		}
		public override void Launch()
		{
			Debug.Log("value popupmanager : "+ injectionBinder.GetBinding<PopupManager>().value);
			Debug.Log("value GlobalData : " + injectionBinder.GetBinding<GlobalData>().value);

		}
	}
}
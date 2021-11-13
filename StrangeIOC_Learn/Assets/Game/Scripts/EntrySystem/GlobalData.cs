using System.Collections.Generic;
using strange.extensions.context.impl;

namespace EntrySystem
{
	public class GlobalData
	{
		private PanelKey panelAfterLoadMainScene;
		private List<MVCSContext> listContext =  new List<MVCSContext>();
		public GlobalData()
		{

		}
		public GlobalData(MVCSContext mvcsContext)
		{
			ResetPopupShowAfterLoadMainScene();
			listContext.Add(mvcsContext);
		}
		public void SetPopupShowAfterLoadMainScene(PanelKey popupKey)
		{
			panelAfterLoadMainScene = popupKey;
		}
		public PanelKey GetPopupShowAfterLoadMainScene()
		{
			return panelAfterLoadMainScene;
		}
		public void ResetPopupShowAfterLoadMainScene()
		{
			panelAfterLoadMainScene = PanelKey.PanelHome;
		}
		public void AddLastestContext(MVCSContext mvcsContext)
		{
			listContext.Add(mvcsContext);
		}
		public MVCSContext GetLastestContext()
		{
			return listContext[listContext.Count - 1];
		}
		public void RemoveLastestContext(MVCSContext mvcsContext)
		{
			listContext.Remove(mvcsContext);
		}
	}

}
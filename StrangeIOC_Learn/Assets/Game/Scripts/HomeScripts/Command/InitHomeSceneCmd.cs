using strange.extensions.command.impl;
using UnityEngine;

public class InitHomeSceneCmd : Command
{
	[Inject] public PopupManager popupManager { get; set; }
    public override void Execute()
    {
		GetResourcePath = GameResourcePath.PANEL_HERO;
		PanelHeroView panelHeroView = GetInstance<PanelHeroView>();
		GetResourcePath = GameResourcePath.PANEL_HOME;
		PanelHomeView panelHomeView = GetInstance<PanelHomeView>();
		GetResourcePath = GameResourcePath.PANEL_CRAFT;
		PanelCraftView panelCraftView = GetInstance<PanelCraftView>();
		GetResourcePath = GameResourcePath.PANEL_SHOP;
		PanelShopView panelShopView = GetInstance<PanelShopView>();

	}
	public T GetInstance<T>() where T : Component
	{
		bool isInit = injectionBinder.GetBinding<T>(GetInjectName()) == null ||
					  injectionBinder.GetInstance<T>(GetInjectName()) == null;
		if (isInit)
		{
			GameObject q = Instantiate();
			//q.transform.localScale = new Vector3(1, 1, 1);
			if (injectionBinder.GetBinding<T>(GetInjectName()) != null)
			{
				injectionBinder.Unbind<T>(GetInjectName());
			}
			injectionBinder.Bind<T>()
				.ToValue(q.GetComponent<T>())
				.ToName(GetInjectName());
		}

		return injectionBinder.GetInstance<T>(GetInjectName());
	}

	public virtual string GetInjectName()
	{
		return "";
	}
	public GameObject Instantiate()
	{
		GameObject o = PrefabUtils.LoadPrefab(GetResourcePath);
		GameObject spawned = null;
		PanelKey panelKey = o.GetComponent<AbsPanelView>().panelKey;
		if (!popupManager.CheckContainPanel(panelKey))
		{
			spawned = GameObject.Instantiate(o/*, popupManager.GetUILayer(uiLayer)*/) as GameObject;

			popupManager.AddPanel(panelKey, spawned);
		}
		else
		{
			if (popupManager.GetPanelByPanelKey(panelKey) == null)
			{
				spawned = GameObject.Instantiate(o) as GameObject;
				popupManager.AddPanel(panelKey, spawned);
			}
			else
			{
				spawned = popupManager.GetPanelByPanelKey(panelKey);

			}
		}

		return spawned;
	}

	public string GetResourcePath { get; set; }
}

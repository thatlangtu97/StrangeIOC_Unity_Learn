using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public abstract class AbsPanelView : View
{
	[Inject]
	public PopupManager popupManager { get; set; }
	public UILayer uILayer;
	public PanelKey panelKey;
	AutoFIllPanelInParent autoFIllPanelInParent;
	public UiViewController UiViewController;

	protected override void Start()
	{
		base.Start();
		transform.parent = popupManager.GetUILayer(uILayer);
		autoFIllPanelInParent = GetComponent<AutoFIllPanelInParent>();
		autoFIllPanelInParent.AutoFill();
	}
	public void ShowPanelByCmd() 
	{
		base.Start();
		//this.gameObject.SetActive(true);
		//NotifyShowPanel();
		popupManager.ShowPanel(panelKey);
		
	}
	public void ShowPanel()
    {
		UiViewController.Show();
	}
	public void HidePanel()
    {
		UiViewController.Hide();
	}

	public void NotifyShowPanel()
	{
		
	}
}

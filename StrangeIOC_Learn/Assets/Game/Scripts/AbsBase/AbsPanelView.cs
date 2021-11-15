using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public abstract class AbsPanelView : View
{
	[Inject]
	public PopupManager popupManager { get; set; }
	public UILayer uILayer;
	AutoFIllPanelInParent autoFIllPanelInParent;
	protected override void Start()
	{
		base.Start();
		transform.parent = popupManager.GetUILayer(uILayer);
		autoFIllPanelInParent = GetComponent<AutoFIllPanelInParent>();
		autoFIllPanelInParent.AutoFill();
	}
	public void ShowPanel() 
	{
		this.gameObject.SetActive(true);
		NotifyShowPopup();

	}
	
	protected override void OnEnable()
	{
	}
	public void NotifyShowPopup()
	{
		
	}
}

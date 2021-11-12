using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public abstract class AbsPanelView : View
{
	[Inject]
	public PopupManager popupManager { get; set; }
	public UILayer uILayer;
	protected override void Awake()
	{
		base.Awake();
	}
	public void ShowPanel() 
	{
		NotifyShowPopup();

	}
	protected override void OnEnable()
	{
	}
	public void NotifyShowPopup()
	{
		//PopupManager.ShowPopup(this, AddToListShow());
	}
}

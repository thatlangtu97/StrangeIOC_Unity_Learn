using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsPopupView : View
{
	[Inject]
	public PopupManager popupManager { get; set; }
	public UILayer uILayer;
	public PopupKey popupKey;
	AutoFIllPanelInParent autoFIllPanelInParent;
	protected override void Start()
	{
		base.Start();
		transform.parent = popupManager.GetUILayer(uILayer);
		autoFIllPanelInParent = GetComponent<AutoFIllPanelInParent>();
		autoFIllPanelInParent.AutoFill();
	}
	public void ShowPopup()
	{
		this.gameObject.SetActive(true);
		NotifyShowPopup();
		popupManager.ShowPopup(popupKey);
	}

	protected override void OnEnable()
	{
	}
	public void NotifyShowPopup()
	{

	}
}

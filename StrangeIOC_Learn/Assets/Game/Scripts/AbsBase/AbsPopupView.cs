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
	public UiViewController UiViewController;
	protected override void Start()
	{
		base.Start();
		if (uILayer != UILayer.NODE)
		{
			transform.parent = popupManager.GetUILayer(uILayer);
			autoFIllPanelInParent = GetComponent<AutoFIllPanelInParent>();
			autoFIllPanelInParent.AutoFill();
		}
	}
	public virtual void ShowPopupByCmd()
	{
		base.CopyStart();
		NotifyShowPopup();
		popupManager.ShowPopup(popupKey);
	}
	public virtual void ShowPopup()
	{
		UiViewController.Show();
	}
	public void HidePopup()
	{
		UiViewController.Hide();
	}
	public void NotifyShowPopup()
	{

	}
}
public class ParameterPopup
{

}

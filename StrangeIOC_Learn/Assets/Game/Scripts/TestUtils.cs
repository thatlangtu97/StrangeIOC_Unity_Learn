using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUtils
{
    [Inject] public PopupManager popupManager { get; set; }
    public void DebugPopup()
    {
        Debug.Log(popupManager);
    }
}

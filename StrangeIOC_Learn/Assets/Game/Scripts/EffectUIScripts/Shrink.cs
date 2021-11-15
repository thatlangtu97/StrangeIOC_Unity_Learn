using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shrink : MonoBehaviour
{
    public EventTrigger eventrigger;
    public Vector3 shrinkScale = new Vector3(.9f,.9f,1f) , expandScale = Vector3.one;
    void Start()
    {
        transform.localScale = expandScale;
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnShrink()
    {
        transform.localScale = shrinkScale;
        
    }
    public void OnExpand()
    {
        transform.localScale = expandScale;

    }
    void Setup()
    {
        if (eventrigger == null)
        {
            eventrigger = gameObject.AddComponent<EventTrigger>();
        }
        if (eventrigger != null)
        {
            EventTrigger trigger = GetComponentInParent<EventTrigger>();
            EventTrigger.Entry entryPointerDown = new EventTrigger.Entry();
            EventTrigger.Entry entryPointerUp = new EventTrigger.Entry();
            entryPointerDown.eventID = EventTriggerType.PointerDown;
            entryPointerDown.callback.AddListener((eventData) => OnShrink() );
            entryPointerUp.eventID = EventTriggerType.PointerUp;
            entryPointerUp.callback.AddListener((eventData) => OnExpand());
            eventrigger.triggers.Add(entryPointerDown);
            eventrigger.triggers.Add(entryPointerUp);
        }
    }
}

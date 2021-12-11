using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Unity.Collections;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    //public GameObject gameinitObj;

    //public int deviceId;
    [SerializeField]
    public Image BackGround, PointJoystick;
    public Vector3 posStart, posEnd, ForceVector;
    [Range(1.5f, 4f)]
    public float space =2.5f;
    public ComponentManager componentManager;
    IEnumerator delayActive(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(true);
    }
    void Start()
    {
        posStart = Vector3.zero;
        BackGround = transform.GetComponent<Image>();
        PointJoystick = transform.GetChild(0).GetComponent<Image>();

    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        Vector2 sizeDelta;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(BackGround.rectTransform
                                                                    , eventData.position
                                                                    , eventData.pressEventCamera
                                                                    , out pos))
        {
            sizeDelta = BackGround.rectTransform.sizeDelta;
            pos.x = (pos.x / sizeDelta.x);
            pos.y = (pos.y / sizeDelta.y);
            posEnd = new Vector3(pos.x * 2 , pos.y * 2);
            posEnd = (posEnd.magnitude >= 1f) ? posEnd.normalized : posEnd;
            ForceVector = new Vector3(posEnd.x, posEnd.y,0f);
            PointJoystick.rectTransform.anchoredPosition = new Vector3((posEnd.x *sizeDelta.x) / space, (posEnd.y * sizeDelta.y) / space);
            OnMove();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        if (componentManager != null)
        {
            componentManager.stateMachine.currentState.OnInputMove();
            //componentManager.stateMachine.ChangeState(componentManager.stateMachine.moveState);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        posEnd = Vector3.zero;
        ForceVector = Vector3.zero;
        PointJoystick.rectTransform.anchoredPosition = posEnd;
        OnStop();
    }
    void OnMove()
    {
        if (componentManager != null)
        {
            if (ForceVector.x > 0)
            {
                componentManager.speedMove = componentManager.maxSpeedMove;
            }
            if (ForceVector.x < 0)
            {
                componentManager.speedMove = -componentManager.maxSpeedMove;
            }
        }
    }
    void OnStop()
    {
        if (componentManager != null)
        {
            componentManager.speedMove = 0f;
        }
    }

}

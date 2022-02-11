using strange.extensions.mediation.impl;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
class SlideHeroView : View
{
    [Inject] public GlobalData global { get; set; }
    [Inject] public OnViewHeroSignal OnViewHeroSignal { get; set; }
    
    public Button nextHero,previousHero;
    List<int> tempList = new List<int>();
    protected override void Awake()
    {
        base.Awake();
        nextHero.onClick.AddListener(SelectNextHero);
        previousHero.onClick.AddListener(SelectPreviousHero);
        foreach (HeroConfig config in ScriptableObjectData.HeroConfigCollection.heroes)
        {
            tempList.Add(config.id);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        CheckButton();
    }

    public void SelectNextHero()
    {
        for (int i = 0; i < tempList.Count; i++) {
            if (global.CurrentIdHero == tempList[i])
            {
                if(i== tempList.Count - 1)
                {
                    global.CurrentIdHero = tempList[0];
                    break;
                }
                else
                {
                    global.CurrentIdHero = tempList[i+1];
                    break;
                }
            }
        }
        OnViewHeroSignal.Dispatch();
        CheckButton();
        Debug.Log($"current hero {global.CurrentIdHero}");
    }
    public void SelectPreviousHero()
    {
        for (int i = 0; i < tempList.Count; i++)
        {
            if (global.CurrentIdHero == tempList[i])
            {
                if (i == 0)
                {
                    global.CurrentIdHero = tempList[tempList.Count - 1];
                    break;
                }
                else
                {

                    global.CurrentIdHero = tempList[i-1];
                    break;
                }
            }
        }
        OnViewHeroSignal.Dispatch();
        CheckButton();
        Debug.Log($"current hero {global.CurrentIdHero}");
    }

    public void CheckButton()
    {
        if (global.CurrentIdHero == tempList[tempList.Count - 1])
        {
            nextHero.gameObject.SetActive(false);
            previousHero.gameObject.SetActive(true);
            return;
        }

        if (global.CurrentIdHero == tempList[0])
        {
            nextHero.gameObject.SetActive(true);
            previousHero.gameObject.SetActive(false);
            return;
        }
        
        nextHero.gameObject.SetActive(true);
        previousHero.gameObject.SetActive(true);
    }
}

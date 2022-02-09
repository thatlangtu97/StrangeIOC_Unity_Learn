using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

public class ComponentProperties : MonoBehaviour
{
    public int Heal=100;
    public List<Immune> baseImmunes = new List<Immune>();
    [ShowInInspector]
    List<Immune> currentImunes= new List<Immune>();

    private void Awake()
    {
        currentImunes = baseImmunes.Clone();
    }

    public void AddImunes(List<Immune> immunesAdd)
    {
        List<Immune> tempImmune = baseImmunes.Clone();
        foreach (Immune immuneItem in immunesAdd)
        {
            if(baseImmunes.Contains(immuneItem))
                continue;
            tempImmune.Add(immuneItem);
        }

        currentImunes = tempImmune;
    }

    public void RemoveImmunes(List<Immune> immunesRemove)
    {
//        List<Immune> tempImmune = currentImunes;
        foreach (Immune immuneItem in immunesRemove)
        {
            if(baseImmunes.Contains(immuneItem))
                continue;
            if(currentImunes.Contains(immuneItem))
                currentImunes.Remove(immuneItem);
        }

//        currentImunes = tempImmune;
    }

    public bool HasImmune(Immune immune)
    {
        if (currentImunes.Contains(immune))
        {
            return true;
        }

        return false;
    }

}
public enum Immune{
    HIT,
    KNOCK,
    BLOCK,
}

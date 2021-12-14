using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public interface IAutoAdd<T>
{
    void AddComponent(ref T e);
}

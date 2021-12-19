using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateEventTriger : MonoBehaviour
{
    public void AttackStart()
    {
        Debug.Log("AttackStart");
    }
}
public enum NameStateEvent
{
    AttackStart,

}
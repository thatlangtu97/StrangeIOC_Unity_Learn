using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCompleteEmitOnAnimationEnd : MonoBehaviour
{
    public ComponentManager component;
    //public PlayerStateMachine stateMachine;
    public void AttackComplete()
    {
        //Debug.Log("complete attack");
        if (component.entity.isEnabled == true)
        {
            component.entity.isAttackDone = true;
            CleanUpBufferManager.instance.AddReactiveComponent(() => { if (component.entity.isEnabled) { component.entity.isAttackComplete = true; } }, () => { if (component.entity.isEnabled) { component.entity.isAttackComplete = false; component.entity.isSkillComplete = false; } });
        }        
        //Debug.Log("done");
    }
    public void SkillComplete()
    {
        //Debug.Log("complete skill");
        CleanUpBufferManager.instance.AddReactiveComponent(() => { if (component.entity.isEnabled) {  component.entity.isSkillComplete = true;  } }, () => { if (component.entity.isEnabled) { component.entity.isSkillComplete = false; } });
    }

    public void RollComplete()
    {
        //Debug.Log("Roll comp[lete");
        CleanUpBufferManager.instance.AddReactiveComponent(() => { if (component.entity.isEnabled) { component.entity.isRollComplete = true; } }, () => { if (component.entity.isEnabled) { component.entity.isRollComplete = false; } });
    }

    public void AnimationEnd()
    {
        CleanUpBufferManager.instance.AddReactiveComponent(() => { if (component.entity.isEnabled) {component.entity.isAnimationEnd = true; } }, () => { if (component.entity.isEnabled) { component.entity.isAnimationEnd = false; } });
    }

}

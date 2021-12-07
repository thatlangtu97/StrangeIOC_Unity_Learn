using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class AnimationChainConfig : BaseSkill
{
    public List<AnimationChain> listAnimation;


    public override TaskStatus OnUpdate()
    {
        // base.CastSkill(entity);
        List<AnimationChain> listAnimationChainClone = new List<AnimationChain>();
        for (int i = 0; i < listAnimation.Count; i++)
        {
            AnimationChain chain = new AnimationChain();
            chain.animationName = listAnimation[i].animationName;
            chain.parameter = listAnimation[i].parameter;
            chain.delayTime = listAnimation[i].delayTime;
            chain.animationType = listAnimation[i].animationType;
            chain.isTriggered = false;
            listAnimationChainClone.Add(chain);
        }
        if (entity.hasAnimationChain)
        {

            entity.ReplaceAnimationChain(listAnimationChainClone, 0);
        }
        else
        {

            entity.AddAnimationChain(listAnimationChainClone, 0);
        }
        return TaskStatus.Success;
    }
}

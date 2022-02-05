using UnityEngine;
[System.Serializable]
public class AttackConfig 
{
    public string NameTrigger,NameTriggerAir;
    public float durationAnimation;
    public Vector2 velocity;
    public float durationVelocity;
    public AnimationCurve curveX, curveY;
    //public EventConfig eventConfig;
}

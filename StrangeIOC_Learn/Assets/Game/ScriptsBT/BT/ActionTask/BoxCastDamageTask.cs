using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

public class BoxCastDamageTask : Action
{
    public Vector2 boxSize;
    public Vector2 direction;
    public AttackInfo atkInfo;
    public SharedComponentManager component;
    public override void OnStart()
    {
        var box = Physics2D.BoxCastAll(transform.position, boxSize, 0f, transform.right*direction);
    }
}

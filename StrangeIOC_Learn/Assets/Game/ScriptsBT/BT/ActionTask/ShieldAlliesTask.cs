using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAlliesTask : Action
{
    // Start is called before the first frame update
    public LayerMask whatIsAllies;
    public float radius;
    public SharedComponentManager componentManager;
    public SharedTransform shieldLinkPoint;
    public GameObject shieldPrefab;
    public float shieldDuration;
    public override void OnStart()
    {
        Vector2 castPoint = shieldLinkPoint.Value.position;
        var cols = Physics2D.OverlapCircleAll(castPoint, radius, whatIsAllies);
        foreach (var col in cols)
        {
            Detectable detect = col.GetComponent<Detectable>();
            if (!detect.immuneToEffects)
            {


                GameEntity ally = detect.componentManager.entity;
                if (ally != null)
                {
                    if (ally.hasImmune)
                    {
                        ally.ReplaceImmune(Faction.ENEMY, shieldDuration);
                    }
                    else
                    {
                        ally.AddImmune(Faction.ENEMY, shieldDuration);
                    }
                    GameObject link = ObjectPool.Spawn(GameSceneConfig.instance.shieldLinkPrefab, componentManager.Value.transform);
                    LightningBoltScript boltScript = link.GetComponent<LightningBoltScript>();
                    boltScript.StartObject = componentManager.Value.entity.centerPoint.centerPoint.gameObject;
                    boltScript.EndObject = ally.centerPoint.centerPoint.gameObject;
                    ObjectPool.Recycle(link, shieldDuration);
                    EffectRequestManager.RequestEffect(shieldPrefab, ally.centerPoint.centerPoint, Vector2.zero, shieldDuration);
                }
            }
        }

    }
}

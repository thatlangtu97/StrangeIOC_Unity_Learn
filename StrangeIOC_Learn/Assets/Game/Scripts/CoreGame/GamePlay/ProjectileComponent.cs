using System.Collections;
using System.Collections.Generic;
using Entitas.Unity;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    public Collider2D colliderProjectile;
    public GameEntity entity;
    public EntityLink link;
    public void OnEnable()
    {
        if (entity == null)
        {
            entity = Contexts.sharedInstance.game.CreateEntity();
            link = gameObject.Link(entity);
            var component = GetComponent<IAutoAdd<GameEntity>>();
            component.AddComponent(ref entity);
            ComponentManagerUtils.AddComponent(this);
            colliderProjectile.enabled = true;
        }
    }
}

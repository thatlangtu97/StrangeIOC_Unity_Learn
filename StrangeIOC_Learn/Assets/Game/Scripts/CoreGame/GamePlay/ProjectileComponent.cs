using System;
using System.Collections;
using System.Collections.Generic;
using Entitas.Unity;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    public Collider2D colliderProjectile;
    public GameEntity entity;
    public EntityLink link;
    public ProjectileMovement projectileMovement;
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
    public void OnDisable()
    {
        if (entity != null)
        {
            gameObject.Unlink();
            entity.Destroy();
            entity = null;
            link = null;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}

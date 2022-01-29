using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTakeDamage : MonoBehaviour
{
    public static TestTakeDamage instance;
    public GameEntity entity;
    public GameEntity entityTakeDamage;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddReactiveComponent();
        }
    }
    public void AddReactiveComponent()
    {
        //GameEntity takeDamage;
        //takeDamage = Contexts.sharedInstance.game.CreateEntity();
        //takeDamage.AddTakeDamage( entity, entityTakeDamage, 10,null);
        //takeDamage.Destroy();
    }
    public void AddEntity(GameEntity e)
    {
        if (entity == null)
        {
            entity = e;
        }
        else
        {
            entityTakeDamage = e;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using TMPro;


public class DamageTextSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    GameEntity targetEnemy;
    public DamageTextSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }
    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDamageText;
    }
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.DamageText);
    }
    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity myEntity in entities)
        {
            GameObject text = ObjectPool.Spawn(Resources.Load<GameObject>("DamageTextPrefab"));
            TextMeshPro textmesh = text.GetComponent<TextMeshPro>();
            textmesh.text = myEntity.damageText.value;
            text.transform.position = myEntity.damageText.position;
            ObjectPool.instance.Recycle(text.gameObject,1f);
            myEntity.Destroy();
        }
    }

}

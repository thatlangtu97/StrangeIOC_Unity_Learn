using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Entitas;
using TMPro;


public class DamageTextSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    GameEntity targetEnemy;
    private GameObject textprefab;
    public DamageTextSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
        textprefab = Resources.Load<GameObject>("DamageTextPrefab");
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
            GameObject text = ObjectPool.Spawn(textprefab);
            TextMeshPro textmesh = text.GetComponent<TextMeshPro>();
            textmesh.text = myEntity.damageText.value;
            textmesh.color = DamageTextManager.GetColor(myEntity.damageText.damageTextType);
            text.transform.position = myEntity.damageText.position;
            text.transform.DOMove(text.transform.position + new Vector3(0f,.3f,0f),.4f);
            ObjectPool.instance.Recycle(text.gameObject,.5f);
            myEntity.Destroy();
        }
    }

}

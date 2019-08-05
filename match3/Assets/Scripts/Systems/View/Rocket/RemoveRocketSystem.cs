using System;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using Entitas.Unity;
using UnityEngine;
using Object = UnityEngine.Object;

public class RemoveRocketSystem: ReactiveSystem<GameEntity>
{
    private GameContext gameContext;
    public RemoveRocketSystem(GameContext game) : base(game)
    {
        gameContext = game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Asset.Removed(),
            GameMatcher.RemoveViewRocket.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasRemoveViewRocket;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            destroyViewRocket(e.view);
            e.RemoveView();
        }
    }
    void destroyViewRocket(ViewComponent viewComponent) {
        var gameObject = viewComponent.gameObject;
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        var color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.material.DOColor(color, 0.3f);
        
//        gameObject.Unlink();
//        Object.Destroy(gameObject);
        gameObject.transform
            .DOScale(Vector3.one * 1.9f, 0.3f)
            .OnComplete(() => {
                gameObject.Unlink();
                Object.Destroy(gameObject);
            });
    }
}
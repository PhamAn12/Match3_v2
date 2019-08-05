using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class DestroyBlockRocketSystem : ReactiveSystem<GameEntity>
{
    public DestroyBlockRocketSystem(GameContext context) : base(context)
    {
    }
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.RemoveViewRocket);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.Destroy();
        }
    }
}
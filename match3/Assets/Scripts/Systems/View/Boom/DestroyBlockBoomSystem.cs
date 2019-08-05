using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class DestroyBlockBoomSystem : ReactiveSystem<GameEntity>
{
    public DestroyBlockBoomSystem(GameContext context) : base(context)
    {
    }
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.RemoveViewBoom);
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
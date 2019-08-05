using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class DestroyEntitySystem : ReactiveSystem<GameEntity>
{
    public DestroyEntitySystem(GameContext context) : base(context)
    {
    }
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.TypeMechanicsDestroy)
         );
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.Destroy();
        }
    }
}
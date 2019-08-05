using System;
using System.Collections.Generic;
using Entitas;

public class ReplaceViewBoomSystem: ReactiveSystem<GameEntity>
{
    private GameContext gameContext;
    public ReplaceViewBoomSystem(GameContext game) : base(game)
    {
        gameContext = game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Boom);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.ReplaceAsset("Prefabs/Boom");
        }
    }
}

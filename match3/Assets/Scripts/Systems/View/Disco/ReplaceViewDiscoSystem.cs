using System;
using System.Collections.Generic;
using Entitas;

public class ReplaceViewDiscoSystem : ReactiveSystem<GameEntity>
{
    private GameContext gameContext;
    public ReplaceViewDiscoSystem(GameContext game) : base(game)
    {
        gameContext = game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Disco);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            if (e.asset.name.Equals("Prefabs/Block_1"))
            {
                e.AddTypeDisco("Blue");
                e.ReplaceAsset("Prefabs/DiscoBlue");
            }
            else if (e.asset.name.Equals("Prefabs/Block_2"))
            {
                e.AddTypeDisco("Green");
                e.ReplaceAsset("Prefabs/DiscoGreen");
            }
            else if (e.asset.name.Equals("Prefabs/Piece0"))
            {
                e.AddTypeDisco("Red");
                e.ReplaceAsset("Prefabs/Discored");
            }
            else if (e.asset.name.Equals("Prefabs/Piece3"))
            {
                e.AddTypeDisco("Yellow");
                e.ReplaceAsset("Prefabs/DiscoYellow");
            }
        }
    }
}
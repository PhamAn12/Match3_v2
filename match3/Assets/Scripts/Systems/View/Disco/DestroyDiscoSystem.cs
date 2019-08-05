using System.Collections.Generic;
using Entitas;

public class DestroyDiscoSystem: ReactiveSystem<GameEntity>
{
    private GameContext gameContext;
    public DestroyDiscoSystem(GameContext game) : base(game)
    {
        gameContext = game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.RemoveViewDisco);
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

using System.Collections.Generic;
using Entitas;

public class DestroyBoardGameSystem : ReactiveSystem<GameEntity>
{
    public DestroyBoardGameSystem(GameContext context) : base(context)
    {
    }
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BoadGame);
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
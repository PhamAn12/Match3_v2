using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RocketCombineDiscoSystem : ReactiveSystem<GameEntity>
{
    private GameContext gameContext;
    private IGroup<GameEntity> blockOnBoard;
    public RocketCombineDiscoSystem(GameContext game) : base(game)
    {
        gameContext = game;
        blockOnBoard = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.BoadGameElement)
        );
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.TapOnRocket);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var allBlock = blockOnBoard.GetEntities();
        foreach (var e in entities)
        {
            var posX = e.position.value.x;
            var posY = e.position.value.y;
            var flag = 0; // disco
            GameEntity tempBlock = new GameEntity();
            foreach (var block in allBlock)
            {
                if (block.position.value.y == posY + 1.5f && block.position.value.x == posX
                    || block.position.value.y == posY - 1.5f && block.position.value.x == posX
                    || block.position.value.y == posY && block.position.value.x == posX + 1.5f
                    || block.position.value.y == posY && block.position.value.x == posX - 1.5f)
                {
                    if (block.isDisco)
                    {
                        flag = 1;
                        tempBlock = block;
                    }
                }
            }
            //Debug.Log("FLaggggggggggg : " + flag);
            if (flag == 1)
            {
                e.isBlockRocketMechanic = true;
                tempBlock.isTapOnDisco = true;
                e.isRemoveViewDisco = true;
            }
        }
    }
}

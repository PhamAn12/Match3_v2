using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


// Create board game by using RandomBoardElement
public class CreateBoardSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    readonly GameContext gameContext;
    public CreateBoardSystem(GameContext Game) : base(Game)
    {
        gameContext = Game;
    }

    public void Initialize()
    {
        var gameBoard = gameContext.CreateGameBoard().boadGame;
        
        for (var r = 0; r < gameBoard.row; r++)
        {
            for (var c = 0; c < gameBoard.columns; c++)
            {
                if (Random.value > 0.95f)
                    gameContext.CreateRandomBlock(c, r);
                else
                    gameContext.CreateRandomPiece(c, r);
            }

            
        }
    }

    protected override void Execute(List<GameEntity> entities)
    {
        
        
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasBoadGame;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BoadGame);
    }

}
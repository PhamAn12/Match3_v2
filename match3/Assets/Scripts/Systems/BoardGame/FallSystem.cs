using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Entitas;
using UnityEngine;

public class FallSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext gameContext;
    private IGroup<GameEntity> allBlock;
    public FallSystem(GameContext context) : base(context)
    {
        gameContext = context;
        allBlock = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.View,GameMatcher.Position));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BoadGameElement.Removed());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameController.Instance.StartCoroutine(DelaySomething());
    }
    
    void MoveDown(GameEntity entity, Vector2 position)
    {
        var nextRowPos = CheckEmptyPosition.GetNextEmptyRow(gameContext, position);
        //Debug.Log("NRP " + nextRowPos + " Y " + position.y);
        if (nextRowPos != position.y)
        {
        //Debug.Log("new Pos " + nextRowPos + " Y " + position.y + " x " + position.x);
            entity.ReplacePosition(new Vector2(position.x, nextRowPos));
        }
    }

    IEnumerator DelaySomething()
    {
        yield return new WaitForSeconds(0);
        var gameBoard = gameContext.CreateGameBoard().boadGame;
        
        //Debug.Log(gameBoard.columns + "va" + gameBoard.row);
        for (var c = 0*1.5f; c < gameBoard.columns*1.5f; c += 1.5f) {
            for (var r = 0*1.5f; r < gameBoard.row*1.5f; r += 1.5f) {
                var position = new Vector2(c, r);

                //Debug.Log(position.x + " and " + position.y);
                var movables = gameContext.GetEntitiesWithPosition(position)
                    .Where(e => e.isDownable)
                    .ToArray();
                
                foreach (var e in movables) {
                    //Debug.Log("new Pos" + e);
                    MoveDown(e, position);
                    
                }
            }
        }

        var allBlocks = allBlock.GetEntities();
        foreach (var block in allBlocks)
        {
            //block.isOverlay = true;
        }
    }
}
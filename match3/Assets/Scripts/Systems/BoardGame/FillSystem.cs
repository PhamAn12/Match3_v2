using System;
using System.Collections;
using System.Collections.Generic;

using DefaultNamespace;
using Entitas;
using UnityEngine;

public sealed class FillSystem : ReactiveSystem<GameEntity> {

    readonly GameContext gameContext;

    public FillSystem(GameContext Game) : base(Game) {
        gameContext = Game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.BoadGameElement.Removed());
    }

    protected override bool Filter(GameEntity entity) {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameController.Instance.StartCoroutine(DelaySomething(0.2f));
        
    }

    IEnumerator DelaySomething(float time)
    {
        yield return new WaitForSeconds(time);
        var gameBoard = gameContext.CreateGameBoard().boadGame;
        //Debug.Log("gameboard columns:" + gameBoard.columns );
        for (var c = 0*1.5f; c < gameBoard.columns * 1.5f; c = c + 1.5f) {
            var position = new Vector2(c, gameBoard.row*1.5f);
            var nextRowPos = CheckEmptyPosition.GetNextEmptyRow(gameContext, position);
            //Debug.Log("position : " + position + " nextRowpos : " + nextRowPos + " row : " + gameBoard.row*1.5f);
            
            while(nextRowPos != gameBoard.row * 1.5f) {
                //Debug.Log("c truoc : " + c  + " nextpos truoc : " + nextRowPos );
                //_context.CreateRandomPiece(c/1.5f, nextRowPos/1.5f);
                GameEntity ge = gameContext.CreateRandomPiece(c / 1.5f, nextRowPos / 1.5f);
                ge.isMovable = true;
                ge.isOverlay = true;
                //ge.isMove = true;
                nextRowPos = CheckEmptyPosition.GetNextEmptyRow(gameContext, position);
                //Debug.Log("c sau : " + c  + " nextpos sau : " + nextRowPos );
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Entitas;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MoveSystem : ReactiveSystem<GameEntity>,IInitializeSystem,ICleanupSystem
{
    private Text labelMove;
    private GameContext gameContext;
    //private int move = 100;
    private int numMove = Int32.Parse(PlayerPrefs.GetString("NumOfMoves"));
    //private int numMove = 100;
    IGroup<GameEntity> moveGroup;
    public MoveSystem(GameContext Game) : base(Game)
    {
        gameContext = Game;
        moveGroup = gameContext.GetGroup(GameMatcher.MoveNum);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Move.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        numMove --;
        UpdateMove(numMove);
    }

    public void Initialize()
    {
        
        UpdateMove(numMove);
    }

    void UpdateMove(int move)
    {
        var moveEntiy = gameContext.CreateEntity();
        
        moveEntiy.ReplaceMoveNum(move);
//        Debug.Log(moveEntiy.moveNum.value);
        labelMove = GameObject.Find("Canvas/Panel/NumOfMove").GetComponent<Text>();
        labelMove.text = "Move : " + move;

    }

    public void Cleanup()
    {
        foreach(var e in moveGroup.GetEntities())
        {
            e.Destroy();
        }
    }

    private IEnumerator DelaySomething(float time)
    {
        yield return new WaitForSeconds(time);
        
    }
}
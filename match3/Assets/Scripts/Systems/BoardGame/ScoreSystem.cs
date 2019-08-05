using System.Collections.Generic;
using DefaultNamespace;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : ReactiveSystem<GameEntity>,IInitializeSystem,ICleanupSystem
{
    readonly GameContext gameContext;
    int count = 0;
    private Text label;
    IGroup<GameEntity> scoreGroup;
    public ScoreSystem(GameContext Game) : base(Game) {
        gameContext = Game;
        scoreGroup = gameContext.GetGroup(GameMatcher.Score);
    }

    public void Initialize()
    {
        
        updateScore(0);
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
        
        var ban = entities.Count;
        
        
        count += ban;
        //Debug.Log(count);
        updateScore(count);
    }
    void updateScore(int score)
    {
        var scoreEntity = gameContext.CreateEntity();
        scoreEntity.ReplaceScore(score);
//        Debug.Log("score value : " + scoreEntity.score.value);
        label = GameObject.Find("Canvas/Panel/Text").GetComponent<Text>();
        
        label.text = "Score : " + score; 
        
    }

    public void Cleanup()
    {
        foreach(var e in scoreGroup.GetEntities())
        {
            e.Destroy();
        }
    }
}

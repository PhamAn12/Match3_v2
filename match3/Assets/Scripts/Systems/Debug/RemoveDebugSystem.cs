using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class RemoveDebugSystem: ReactiveSystem<GameEntity>
{
    private GameContext gameContext;
    private Button btn;
    public RemoveDebugSystem(GameContext game) : base(game)
    {
        gameContext = game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Debug);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        btn = GameObject.Find("Canvas/CancelButton").GetComponent<Button>();
        foreach (var e in entities)
        {
            btn.onClick.AddListener((() => RemoveDebugComponent(e)));
        }
    }

    void RemoveDebugComponent(GameEntity gameEntity)
    {
        if (gameEntity.hasView)
        {
            gameEntity.isDebug = false;
        }
    }
}